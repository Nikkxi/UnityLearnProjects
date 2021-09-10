using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<GameObject> enemyPrefabs;
    private GameObject player;

    public GameObject titleScreen;
    public GameObject gameScreen;
    public GameObject gameOverScreen;

    public TextMeshProUGUI timerText;

    public float spawnRate = 3.0f;
    public float spawnRange = 19.0f;

    public bool isGameActive;
    private float timer = 60.0f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(isGameActive == true)
        {
            UpdateTimer();
        }
    }

    public void StartGame(int difficulty)
    {
        spawnRate /= difficulty;

        timerText.text = "Timer: " + Mathf.Round(timer);
        isGameActive = true;

        StartCoroutine(SpawnTarget());

        titleScreen.SetActive(false);
        gameScreen.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GameOver()
    {
        isGameActive = false;
        gameOverScreen.SetActive(true);
    }

    void UpdateTimer()
    {
        timer -= Time.deltaTime;
        timerText.text = "Timer: " + Mathf.Round(timer);

        if(timer <= 0.0f)
        {
            GameOver();
        }
    }

    IEnumerator SpawnTarget()
    {
        while(isGameActive == true)
        {
            yield return new WaitForSeconds(spawnRate);

            Vector3 spawnPos = GetRandomSpawnPoint();
            
            Vector3 directionToPlayer = (player.transform.position - spawnPos).normalized;
            Quaternion rotation = Quaternion.LookRotation(directionToPlayer, transform.up);

            int index = Random.Range(0, enemyPrefabs.Count);
            Instantiate(enemyPrefabs[index], spawnPos, rotation);
        }
    }

    Vector3 GetRandomSpawnPoint()
    {
        int side = Random.Range(0, 4);
        
        Vector3 randomPos;
        float x = Random.Range(-spawnRange, spawnRange);
        float z = Random.Range(-spawnRange, spawnRange);

        switch (side)
        {
            case 0: // top
                randomPos = new Vector3(x, 0, spawnRange);
                break;
            case 1: // bottom
                randomPos = new Vector3(x, 0, -spawnRange);
                break;
            case 2: // right
                randomPos = new Vector3(spawnRange, 0, z);
                break;
            case 3: // left
                randomPos = new Vector3(-spawnRange, 0, z);
                break;
            default:
                Debug.Log("Random Spawn Point somehow ended up not working.");
                break;
        }

        return new Vector3(x, 0, z);
    }
}
