using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelInteractable : Interactable
{
    public FadeManager fadeManager;

    protected override void Interact()
    {
        StartCoroutine(loadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator loadLevel(int levelIndex)
    {
        fadeManager.PlayFadeIn();

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(levelIndex);

    }
}
