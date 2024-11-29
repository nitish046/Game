using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeManager : MonoBehaviour
{
    public Animator fade;
    public float fade_time = 1f;

    private void Awake()
    {
        //fade = GetComponentInChildren<Animator>();
    }

    public void fadeToNextLevel()
    {
        StartCoroutine(loadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void PlayFadeIn()
    {
        fade.ResetTrigger("fadeOutTrigger");
        fade.SetTrigger("fadeInTrigger");
    }

    public void PlayFadeOut()
    {
        fade.ResetTrigger("fadeInTrigger");
        fade.SetTrigger("fadeOutTrigger");
    }

    IEnumerator loadLevel(int levelIndex)
    {
        PlayFadeIn();

        yield return new WaitForSeconds(fade_time);

        SceneManager.LoadScene(levelIndex);

    }
}
