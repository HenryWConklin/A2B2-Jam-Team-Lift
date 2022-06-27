using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SR_TetronimoBlockSpawner : MonoBehaviour
{
    public List<Transform> spawnPositions = new List<Transform>();
    public GameObject block;

    void Start()
    {
        InvokeRepeating("SpawnBlock", 0f, 1f);
    }

    private void SpawnBlock()
    {
        if (!GameManager.Instance.gameStarted)
        {
            return;
        }
        int randomPosition = Random.Range(0, spawnPositions.Count);
        GameObject tempBlock = Instantiate(block, spawnPositions[randomPosition].position, Quaternion.identity);
    }
}