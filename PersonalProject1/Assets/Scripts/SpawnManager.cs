using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    private GameObject player;
    private PlayerController playerController;

    public float spawnRange = 19.0f;
    private float spawnTimer = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();

        //InvokeRepeating("SpawnEnemy", 2.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if(playerController.isGameOver == false)
        {
            spawnTimer -= Time.deltaTime;

            if(spawnTimer <= 0)
            {
                SpawnEnemy();
                spawnTimer = 1.5f;
            }
        }
    }

    void SpawnEnemy()
    {
        enemyPrefab.transform.position = GetRandomSpawnPosition();

        enemyPrefab.transform.LookAt(player.transform);

        Instantiate(enemyPrefab);
    }

    Vector3 GetRandomSpawnPosition()
    {
        int side = Random.Range(0, 4);
        float x;
        float z;

        switch (side)
        {
            case 0: // top
                x = Random.Range(-spawnRange, spawnRange);
                z = spawnRange;
                break;
            case 1: // bottom
                x = Random.Range(-spawnRange, spawnRange);
                z = -spawnRange;
                break;
            case 2: // right
                x = spawnRange;
                z = Random.Range(-spawnRange, spawnRange);
                break;
            case 3: // left
                x = -spawnRange;
                z = Random.Range(-spawnRange, spawnRange);
                break;
            default: // random spawn - should never reach this condition
                x = Random.Range(-spawnRange, spawnRange);
                z = Random.Range(-spawnRange, spawnRange);
                break;
        }

        return new Vector3(x, enemyPrefab.transform.position.y, z);
    }
}
