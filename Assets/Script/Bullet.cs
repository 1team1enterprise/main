using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int dmg;
    
    private float speed = 6f;

    void Awake()
    {
        Destroy(gameObject, 3f);
    }

    void Update()
    {
        transform.Translate(0f, speed * Time.deltaTime, 0f);
    }
}
