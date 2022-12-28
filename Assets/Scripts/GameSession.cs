using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameSession : MonoBehaviour
{
    //config params
    [Range(0.1f, 10f)] [SerializeField] float gameSpeed = 1f;
    [SerializeField] int pointsPerBlockDestroyed = 83;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI highScore;
    [SerializeField] TextMeshProUGUI lives;
    [SerializeField] bool isAutoPlayEnabled;
    [SerializeField] int livesCount = 3;
    //state variables
    [SerializeField] int currentScore = 0;

    private void Awake()
    {
        int gameStatusCount = FindObjectsOfType<GameSession>().Length;
        if (gameStatusCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }

        else //if(FindObjectOfType<SceneLoader>().)
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        scoreText.text = currentScore.ToString();
        highScore.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
        lives.text = livesCount.ToString();
         //sceneLoader = FindObjectOfType<SceneLoader>();
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = gameSpeed;

        //scoreText.text = currentScore.ToString();
    }

    public void AddToScore()
    {
        currentScore += pointsPerBlockDestroyed;
        scoreText.text = currentScore.ToString();

        if (currentScore > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", currentScore);
            highScore.text = currentScore.ToString();
        }
    }

    public void SetToZero()
    {
        currentScore = 0;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    public bool IsAutoPlayEnabled()
    {
        return isAutoPlayEnabled;
    }

    public void DecreaseLives()
    {
        livesCount--;
        lives.text = livesCount.ToString();
    }

    public int RemainingLives()
    {
        return livesCount;
    }

    public void Reset()
    {
        PlayerPrefs.DeleteAll();
    }
}
