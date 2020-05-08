using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;

    public void pauseGame()
    {
        Time.timeScale = 0f;
        Time.fixedDeltaTime = 0f;
        GameIsPaused = true;
        AudioListener.pause = false;
        Debug.Log("Time Stop");
    }

    public void resumeGame()
    {

                Time.timeScale = 1f;
                Time.fixedDeltaTime = 1f;
                GameIsPaused = false;
                AudioListener.pause = true;
            Debug.Log("Resume");
        
    }
}
