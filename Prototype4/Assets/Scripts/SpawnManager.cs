using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject powerupPrefab;
    private PlayerController playerController;

    public float spawnRange = 9;
    private int nextWave = 1;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        int enemiesAlive = FindObjectsOfType<Enemy>().Length;

        if (enemiesAlive == 0 && playerController.isGameOver != true)
        {
            SpawnWave();
            SpawnPowerup();
        }
    }

    void SpawnWave()
    {
        for (int i = 0; i < nextWave; i++)
        {
            SpawnEnemy();
        }
        nextWave++;
    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
    }

    void SpawnPowerup()
    {
        Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);
    }

    Vector3 GenerateSpawnPosition()
    {
        float spawnX = Random.Range(-spawnRange, spawnRange);
        float spawnZ = Random.Range(-spawnRange, spawnRange);
        return new Vector3(spawnX, 0, spawnZ);
    }
}
