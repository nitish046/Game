using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinLoseControl : MonoBehaviour
{
  private int game_status = 0;

  public GameObject mainScreen;
  public GameObject winScreen;
  public GameObject loseScreen;

  public Button restart_button;
  public Button quit_button;
  public Button continue_button;

  private void Start()
  {
    mainScreen.SetActive(true);
    winScreen.SetActive(false);
    loseScreen.SetActive(false);
  }

  public void WinGame()
  {
    if (game_status != 0)
    {
      return;
    }
    game_status = 2;
    mainScreen.SetActive(false);
    winScreen.SetActive(true);
    if (SceneManager.GetActiveScene().buildIndex == 1)
    {
      continue_button.gameObject.SetActive(true);
    }
    restart_button.gameObject.SetActive(true);
    quit_button.gameObject.SetActive(true);
  }
  public void LoseGame()
  {
    if (game_status != 0)
    {
      return;
    }
    game_status = 3;
    restart_button.gameObject.SetActive(true);
    quit_button.gameObject.SetActive(true);
    mainScreen.SetActive(false);
    loseScreen.SetActive(true);
  }
}
