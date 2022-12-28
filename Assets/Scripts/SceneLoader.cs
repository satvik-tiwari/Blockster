using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
   // bool flag = false;

    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
             SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void LoadStartScene()
    {

        SceneManager.LoadScene(0); // We are loading scene 0
        //FindObjectOfType<GameSession>().Destroy();
        //  flag = true;

    }
    
    public void QuitApplication()
    {
        Application.Quit();
    }

  
}
