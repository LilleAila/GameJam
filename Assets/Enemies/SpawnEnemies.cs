using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    public Enemy[] enemies;
    public Transform playerTransform;
    public float spawnSpeed = 2.5f;

    private void Start()
    {
        InvokeRepeating("SpawnEnemiesInWorld", spawnSpeed, spawnSpeed);
    }

    private void SpawnEnemiesInWorld()
    {
        if (!LightingManager.night) return;

        for (int i = 0; i < enemies.Length; i++)
        {
            for (int b = 0; b < Mathf.FloorToInt(Random.Range(1, 5)); b++)
            {
                if (Random.Range(0, enemies[i].spawnChance) == 1)
                {
                    Vector3 spawn = new Vector3(
                            playerTransform.position.x + Random.Range(-enemies[i].spawnRadius, enemies[i].spawnRadius),
                            40,
                            playerTransform.position.z + Random.Range(-enemies[i].spawnRadius, enemies[i].spawnRadius)
                        );

                    Instantiate(enemies[i].enemyPrefab, spawn, Quaternion.identity);
                }
            }
        }
    }
}

[System.Serializable]
public class Enemy {
    [Range(1, 10)] public int spawnChance = 1;
    public float spawnRadius = 25;
    public GameObject enemyPrefab;
}