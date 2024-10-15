using System.Collections;
using System.Collections.Generic;
//using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class LifeTracker : MonoBehaviour
{
  public int lives;

  [SerializeField]
  public UnityEngine.UI.Image[] hearts;
  public Sprite heartImage;

  public GameObject loseScreen;
  public GameObject mainScreen;

  public Button restart_button;
  public Button quit_button;

  void Update()
  {
    if (Input.GetKeyDown(KeyCode.Backspace))
    {
      LoseLife();
    }

    for (int i = 0; i < hearts.Length; i++)
    {
      if (i < lives)
      {
        hearts[i].enabled = true;
      }
      else
      {
        hearts[i].enabled = false;
      }
    }
  }

  void LoseLife()
  {
    if (lives >= 0)
    {
      lives--;
      if (lives == 0)
      {
        LoseGame();
      }
    }
  }

  void LoseGame()
  {
    mainScreen.SetActive(false);
    loseScreen.SetActive(true);
    restart_button.gameObject.SetActive(true);
    quit_button.gameObject.SetActive(true);
  }

}
