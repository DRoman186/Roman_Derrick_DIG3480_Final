using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;
    public AudioClip gameover01;
    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;
    AudioSource audioSource;

    public Text scoreText;
    public Text restartText;
    public Text gameOverText;
    public Text gameWonText;

    public bool gameOver;
    public bool restart;
    public bool gameWon;
    private int score;
    private BGScroller BGScroller;

    void Start()
    {
        gameOver = false;
        restart = false;
        gameWon = false;
        gameWonText.text = "";
        restartText.text = "";
        gameOverText.text = "";
        audioSource = GetComponent<AudioSource>();
        score = 0;
        UpdateScore();
        StartCoroutine(SpawnWaves());
    }

    void Update()
    {
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
        if (Input.GetKey("escape"))
            Application.Quit();
    }


    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                restartText.text = "Press 'P' for Restart";
                restart = true;
                break;
            }

            if (gameWon)
            {
                restartText.text = "Press 'P for Restart";
                restart = true;
                hazardCount = 0;
                break;
            }
        }
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        if (score <= 100)
        {
            scoreText.text = "Points: " + score;
        }
        else
        {
            scoreText.text = "Points: 100";
        }
        if (score >= 100)
        {
            score = 100;
            if (score == 100 && !gameWon)
            {
                gameWonText.text = "You Win! Game Created by Derrick Roman";
                gameWon = true;
                audioSource.Stop();
                audioSource.clip = musicClipOne;
                audioSource.Play();
                audioSource.loop = false;
            }
        }
    }

    public void GameOver()
    {
        gameOverText.text = "Game Over!";
        gameOver = true;
        audioSource.PlayOneShot(gameover01, 5f);
        audioSource.clip = musicClipTwo;
        audioSource.Play();
        audioSource.loop = false;
    }
}