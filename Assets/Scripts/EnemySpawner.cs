using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;

    public Vector2 gridSize = new Vector2(64, 64);

    public float spawnX = 0;

    public float spawnYMin = -128;
    public float spawnYMax = 128;

    /// Spawns a new enemy in a random position, returns the spawned enemy GameObject
    public GameObject Spawn()
    {
        float x = Mathf.Round(spawnX / gridSize.x) * gridSize.x;
        float y = UnityEngine.Random.Range(spawnYMin, spawnYMax);
        y = Mathf.Round(y / gridSize.y) * gridSize.y;
        int enemyPrefabInd = Random.Range(0, enemyPrefabs.Length);

        return Instantiate(enemyPrefabs[enemyPrefabInd], new Vector3(x, y, 0.0f), Quaternion.identity);
    }
}