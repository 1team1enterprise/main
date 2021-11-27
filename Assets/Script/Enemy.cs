using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum Type { Range, Melee };
    public Type enemyType;

    // 컴포넌트 할당
    public Transform attackPos;

    private Transform attackTarget;

    private Rigidbody2D rigid;

    // 변수
    public int curHealth;
    public int maxHealth;

    public float moveSpeed;

    public float detectRange;

    public int attackDamage;
    public int bodyDamage;
    public float attackRange;
    public float attackRangeLimit;
    public float attackDelay;

    private float attackTimer = 0f;

    // 벡터
    private Vector2 movement;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        curHealth = maxHealth;
    }

    void Update()
    {
        AI();
    }

    void AI()
    {
        StartCoroutine(SearchTarget());
        Look();
        if (attackTarget != null)
        {
            attackTimer += Time.deltaTime;
            if (Vector2.Distance(transform.position, attackTarget.position) >= attackRangeLimit)
            {
                Move(movement);
            }
            if (enemyType == Type.Range)
            {
                if (Vector2.Distance(transform.position, attackTarget.position) <= attackRange)
                {
                    if (attackTimer >= attackDelay)
                    {
                        RangeAttack();
                        attackTimer = 0;
                    }
                }
            }
        }
        else
        {
            Move(movement);
        }
    }

    void Look()
    {
        if (attackTarget != null)
        {
            Vector2 direction = attackTarget.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle - 90);
            direction.Normalize();
            movement = direction;
        }
        else
        {
            Vector2 direction = new Vector3(0, 0, transform.position.z) - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle - 90);
            direction.Normalize();
            movement = direction;
        }
    }

    void Move(Vector2 direction)
    {
        rigid.position += direction * moveSpeed * Time.deltaTime;
    }

    void RangeAttack()
    {
        var bullet = ObjectPooler.SpawnFromPool<Bullet>("Enemy_Bullet", attackPos.position, attackPos.rotation);
        bullet.dmg = attackDamage;
        bullet.Shoot();
    }

    void OnDisable()
    {
        ObjectPooler.ReturnToPool(gameObject);    // 한 객체에 한번만 
        CancelInvoke();    // Monobehaviour에 Invoke가 있다면 
    }

    public void OnHit(int dmg)
    {
        curHealth -= dmg;

        if (curHealth <= 0)
        {
            EnemySpawner.instance.enemyList.Remove(this);
            gameObject.SetActive(false);
            attackTarget = null;
            curHealth = maxHealth;
        }
    }

    private IEnumerator SearchTarget()
    {
        while (true)
        {
            float closeDisSqr = Mathf.Infinity;
            for (int i = 0; i < PlayerController.instance.playerList.Count; ++i)
            {
                float distance = Vector3.Distance(PlayerController.instance.playerList[i].transform.position, transform.position);
                if (distance <= detectRange && distance <= closeDisSqr)
                {
                    closeDisSqr = distance;
                    attackTarget = PlayerController.instance.playerList[i].transform;
                }
                yield return null;
            }
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            if (attackTimer >= attackDelay)
            {
                if (enemyType == Type.Melee)
                {
                    player.OnHit(attackDamage);
                    attackTimer = 0;
                }
                else if (enemyType == Type.Range)
                {
                    player.OnHit(bodyDamage);
                    attackTimer = 0;
                }
            }
        }
    }
}
