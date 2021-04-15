using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform player;
    public float spawnRadius = 3f;
    public float spawnTime = 2f;
    public GameObject enemy;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        if(player != null)
        {
            Vector2 spawnPosition = player.position;
            spawnPosition += Random.insideUnitCircle.normalized * spawnRadius;
            Instantiate(enemy, spawnPosition, Quaternion.identity);
        }
        yield return new WaitForSeconds(spawnTime);
        StartCoroutine(SpawnEnemy());
    }
}
