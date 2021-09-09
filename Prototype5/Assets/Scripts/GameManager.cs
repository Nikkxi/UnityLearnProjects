using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    public GameObject titleScreen;
    public GameObject gameScreen;
    public GameObject gameOverScreen;
    public TextMeshProUGUI scoreText;

    public float spawnRate = 2.0f;
    private int score;

    private bool isGameActive;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);

            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void TriggerGameOver()
    {
        isGameActive = false;
        gameOverScreen.SetActive(true);
    }

    public bool IsGameActive()
    {
        return isGameActive;
    }

    public void RestartGame()
    {
        gameOverScreen.SetActive(false);
        score = 0;
        scoreText.text = "Score: " + score;
        isGameActive = true;
        StartCoroutine(SpawnTarget());
    }

    public void ReturnToTitleScreen()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(int difficulty)
    {
        spawnRate /= difficulty;

        score = 0;
        scoreText.text = "Score: " + score;
        isGameActive = true;
        StartCoroutine(SpawnTarget());

        titleScreen.SetActive(false);
        gameScreen.SetActive(true);
    }
}
