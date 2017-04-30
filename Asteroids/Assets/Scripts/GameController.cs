using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public GameObject powerup;

    public Vector3 spawnValues;

    public GUIText scoreText;
    public GUIText restartText;
    public GUIText gameOverText;

    private bool gameOver;
    private bool restart;

    public int hazardCount;
    private int score;

    public float spawnWait;
    public float startWait;
    public float waveWait;

    void Start()
    {
        gameOver = false;

        restart = false;

        restartText.text = string.Empty;

        gameOverText.text = string.Empty;

        score = 0;

        UpdateScoreText();

        StartCoroutine(SpawnWaves());
    }

    void Update()
    {
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("Main");
            }
        }
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);

        var waveCount = 1;

        while (true)
        {
            if ((waveCount % 2) == 0)
            {
                hazardCount ++;

                spawnWait -= 0.05f;
            }            

            for (int i = 0; i < hazardCount; i++)
            {
                var hazard = hazards[Random.Range(0, hazards.Length)];

                var spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                
                var spawnRotation = Quaternion.identity;

                Instantiate(hazard, spawnPosition, spawnRotation);

                yield return new WaitForSeconds(spawnWait);
            }

            if (gameOver)
            {
                restartText.text = "Press 'R' for Restart";

                restart = true;

                break;
            }

            var powerupSpawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), -5.13f, spawnValues.z);

            var powerupSpawnRotation = Quaternion.identity;

            Instantiate(powerup, powerupSpawnPosition, powerupSpawnRotation);

            waveCount++;

            yield return new WaitForSeconds(waveWait);
        }
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;

        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        scoreText.text = string.Format("Score: {0}", score);
    }

    public void GameOver()
    {
        gameOverText.text = "Game Over";

        gameOver = true;
    }
}
