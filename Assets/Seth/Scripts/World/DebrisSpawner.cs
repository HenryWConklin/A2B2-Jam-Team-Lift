using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DebrisSpawner : MonoBehaviour
{
    public GameObject debrisObj;
    public List<Transform> spawnPositions;
    public bool spawnDebris = true;
    public PlayerBase player;


    private void Awake()
    {
        InitializeSpawnPositions();
        player = GameObject.FindObjectOfType<PlayerBase>();
        StartCoroutine(SpawnDebris_Co());
    }

    private void InitializeSpawnPositions()
    {
        for (int i = 0; i < transform.childCount; i++) spawnPositions.Add(transform.GetChild(i).transform);
    }

    public IEnumerator SpawnDebris_Co()
    {
        while (spawnDebris)
        {
            SpawnDebris();
            yield return new WaitForSeconds(1f);
        }
    }


    public void SpawnDebris()
    {
        int random = UnityEngine.Random.Range(0, spawnPositions.Count);
        GameObject newDebrisObj = Instantiate(debrisObj, spawnPositions[random].position, Quaternion.identity);
        newDebrisObj.GetComponent<Debris>().Init(GetDebrisDirection());
    }

    public Vector2 GetDebrisDirection()
    {
        //var position = player.transform.position;
        Vector2 playerPos = player.transform.position;
        Vector2 randomLocation = new Vector2(playerPos.x + UnityEngine.Random.Range(playerPos.x - 6f, playerPos.x + 6f),
            playerPos.y + UnityEngine.Random.Range(playerPos.y - 3f, playerPos.y + 3f));

        return randomLocation;


    }
    
    
}
