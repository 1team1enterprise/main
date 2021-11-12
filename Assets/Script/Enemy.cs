using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // 컴포넌트 할당
    private Rigidbody2D rigid;

    public GameObject enemy_bullet;
    public Transform player;
    public Transform firepoint;

    // 변수
    public enum Type { Melee, Range };
    public Type type;

    public float moveSpeed;
    public int health;
    public int meleeDamage;
    public float attackRange;
    public float attackDelay;

    private float timer = 0f;

    // 벡터
    private Vector2 movement;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (type == Type.Melee)
        {
            AI_m();
        }
        else if (type == Type.Range)
        {
            AI_r();
        }
    }

    void AI_m()
    {
        Vector2 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        direction.Normalize();
        movement = direction;

        if (Vector2.Distance(transform.position, player.position) > attackRange - 0.2f)
            Move(movement);
    }

    void AI_r()
    {
        Vector2 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        direction.Normalize();
        movement = direction;

        if (Vector2.Distance(transform.position, player.position) > (attackRange - 2f))
            Move(movement);
        if (Vector2.Distance(transform.position, player.position) <= attackRange)
            Attack_r();
            
    }

    void Attack_r()
    {
        if (timer > attackDelay)
        {
            Instantiate(enemy_bullet, firepoint.position, firepoint.rotation);
            timer = 0f;
        }
    }

    void Move(Vector2 direction)
    {
        rigid.position += direction * moveSpeed * Time.deltaTime;
    }

    void OnHit(int dmg)
    {
        health -= dmg;

        if(health <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player_bullet")
        {
            Bullet bullet = other.gameObject.GetComponent<Bullet>();
            OnHit(bullet.dmg);

            other.gameObject.SetActive(false);
        }
    }
}
