using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Setup")]
    [SerializeField] private GameObject enemyPrefab;

    [Header("Square Area")]
    [SerializeField] private Vector3 bottomLeftCorner;
    [SerializeField] private Vector3 topRightCorner;

    public List<GameObject> SpawnEnemies(int count)
    {
        List<GameObject> spawnedEnemies = new List<GameObject>();

        for (int i = 0; i < count; i++)
        {
            float randomX = Random.Range(bottomLeftCorner.x, topRightCorner.x);
            float randomZ = Random.Range(bottomLeftCorner.z, topRightCorner.z);
            float y = bottomLeftCorner.y;

            Vector3 spawnPos = new Vector3(randomX, y, randomZ);
            GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
            spawnedEnemies.Add(enemy);
        }

        return spawnedEnemies;
    }
}