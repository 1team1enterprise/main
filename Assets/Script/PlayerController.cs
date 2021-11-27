using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public List<PlayerController> playerList;

    // ÄÄÆ÷³ÍÆ® ÇÒ´ç
    private Rigidbody2D myrigid;

    public Transform firePos;

    // º¯¼ö
    public int curHealth;
    public int maxHealth;

    public float moveSpeed;

    public int attackDamage;
    public float attackDelay;

    private float attackTimer = 0f;

    // º¤ÅÍ
    Vector2 direction;

    void Awake()
    {
        curHealth = maxHealth;
        instance = this;
        myrigid = GetComponent<Rigidbody2D>();
        playerList.Add(this);
    }

    void Update()
    {
        attackTimer += Time.deltaTime;
        Move();
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
        {
            if (attackTimer > attackDelay)
            {
                Fire();
                attackTimer = 0f;
            }
        }
    }

    void Move()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = mousePos - (Vector2)transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);

        myrigid.velocity = new Vector2(moveSpeed * Input.GetAxisRaw("Horizontal"), moveSpeed * Input.GetAxisRaw("Vertical"));
    }

    void Fire()
    {
        var bullet = ObjectPooler.SpawnFromPool<Bullet>("Player_Bullet", firePos.position, firePos.rotation);
        bullet.dmg = attackDamage;
        bullet.Shoot();
    }

    public void OnHit(int dmg)
    {
        curHealth -= dmg;
        if (curHealth <= 0)
        {
            curHealth = 0;
            playerList.Remove(this);
            gameObject.SetActive(false);
        }
        UIManager.instance.HandleHp();
    }
}