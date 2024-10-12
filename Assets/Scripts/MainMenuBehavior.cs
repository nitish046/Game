using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuBehavior : MonoBehaviour
{
    public void playButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void quitButton()
    { 
        Application.Quit();  
    }

    public void resetButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
