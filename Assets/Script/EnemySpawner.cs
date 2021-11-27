using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner instance;

    public List<Enemy> enemyList;

    public Transform[] spawnPos;

    public int spawnEnemyLimit;
    public float spawnDelay;

    private float spawnTime;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        spawnTime += Time.deltaTime;
        if (spawnTime >= spawnDelay)
        {
            Spawn();
            spawnTime = 0;
        }
    }

    void Spawn()
    {
        for (int i = 0; i < spawnPos.Length; i++)
        {
            if (enemyList.Count < spawnEnemyLimit)
            {
                Debug.Log("1 : " + enemyList.Count);
                int randomSpawn = Random.Range(0, 2);
                if (randomSpawn == 0)
                {
                    Enemy enemyM = ObjectPooler.SpawnFromPool<Enemy>("MeleeEnemy", spawnPos[i].position, spawnPos[i].rotation);
                    enemyList.Add(enemyM);
                }
                else
                {
                    Enemy enemyR = ObjectPooler.SpawnFromPool<Enemy>("RangeEnemy", spawnPos[i].position, spawnPos[i].rotation);
                    enemyList.Add(enemyR);
                }
            }
        }
    }
}
