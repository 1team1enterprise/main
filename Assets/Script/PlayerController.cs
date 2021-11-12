using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // 컴포넌트 할당
    private Rigidbody2D myrigid;

    public GameObject player_bullet;
    public Transform firepoint;
    public Slider hpbar;

    // 변수
    public int health;
    public int maxhealth;
    public float moveSpeed;
    public float attackDelay;
    public float meleeAttackDelay;

    private float attackTimer = 0f;
    private float meleeAttackTimer = 0f;

    // 벡터
    Vector2 direction;

    private void Awake()
    {
        myrigid = GetComponent<Rigidbody2D>();

        hpbar.value = (float)health / (float)maxhealth;
    }

    private void Start()
    {
        health = maxhealth;
        HandleHp();
    }

    void Update()
    {
        attackTimer += Time.deltaTime;
        meleeAttackTimer += Time.deltaTime;

        Move();
        Fire();
        HandleHp();
    }

    void Move()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = mousePos - (Vector2)transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        myrigid.position += new Vector2(x, y) * moveSpeed * Time.deltaTime;
        // 나중위치 = 현재위치 + 방향 * 속력(속도 * 시간)
    }

    void Fire()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
        {
            if (attackTimer > attackDelay)
            {
                Instantiate(player_bullet, firepoint.position, firepoint.rotation);
                attackTimer = 0f;
            }
        }
    }

    void OnHit(int dmg)
    {
        health -= dmg;
        if (health <= 0)
        {
            health = 0;
            hpbar.value = 0;
            gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy_bullet")
        {
            Bullet bullet = other.gameObject.GetComponent<Bullet>();
            OnHit(bullet.dmg);
            other.gameObject.SetActive(false);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (meleeAttackTimer > meleeAttackDelay && other.gameObject.tag == "Enemy")
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            OnHit(enemy.meleeDamage);
            meleeAttackTimer = 0;
        }
    }

    void HandleHp()
    {
        hpbar.value = Mathf.Lerp(hpbar.value, (float)health / (float)maxhealth, Time.deltaTime * 10);
    }
}