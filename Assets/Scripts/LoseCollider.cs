using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseCollider : MonoBehaviour
{
    //Cached References
    GameSession gameSession;

    private void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        gameSession.DecreaseLives();
        if (gameSession.RemainingLives() == 0)
        {
            SceneManager.LoadScene("Game Over");
            FindObjectOfType<GameSession>().Destroy();
        }

        else
        {
            FindObjectOfType<Ball>().ResetBall();
        }
    }
}
