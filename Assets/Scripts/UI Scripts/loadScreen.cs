using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class loadScreen : MonoBehaviour
{

    public void PlayGame()
    {
        SceneManager.LoadScene("inHouse");
        
    }

    public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }
}
