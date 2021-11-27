using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public enum Type { PlayerBullet, EnemyBullet };
    public Type bulletType;
    public int dmg;

    [SerializeField] private float speed = 10f;

    public void Shoot()
    {
        Invoke(nameof(DestroyBullet), 3f);
    }

    private void DestroyBullet() => gameObject.SetActive(false);

    void OnDisable()
    {
        ObjectPooler.ReturnToPool(gameObject);    // 한 객체에 한번만 
        CancelInvoke();    // Monobehaviour에 Invoke가 있다면 
    }

    void Update()
    {
        transform.Translate(0f, speed * Time.deltaTime, 0f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        switch (bulletType)
        {
            case Type.EnemyBullet :
                if (other.gameObject.CompareTag("Player"))
                {
                    PlayerController player = other.gameObject.GetComponent<PlayerController>();
                    player.OnHit(dmg);
                    DestroyBullet();
                }
                break;
            case Type.PlayerBullet :
                if (other.gameObject.CompareTag("Enemy"))
                {
                    Enemy enemy = other.gameObject.GetComponent<Enemy>();
                    enemy.OnHit(dmg);
                    DestroyBullet();
                }
                break;
        }
    }
}
