using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TutorialMusic : MonoBehaviour
{
    public AudioSource tutorialMusic;
    public AudioSource intenseMusic;
    public GameObject hasStackCollectibles; // Reference to the GameObject with StackCollectables component
    protected StackCollectables stackCollectables;
    private bool hasEnoughFood = false;

    private void Start()
    {
        // Initialize stackCollectables
        stackCollectables = hasStackCollectibles.GetComponent<StackCollectables>();
        if (stackCollectables == null)
        {
            Debug.LogError("StackCollectables component not found on hasStackCollectibles GameObject.");
            return;
        }

        Debug.Log("Start Tutorial Music");
        PlayTutorialMusic();
        StartCoroutine(checkDeclareEnoughFood());
    }

    // Method to play tutorial music
    public void PlayTutorialMusic()
    {
        if (intenseMusic.isPlaying)
        {
            intenseMusic.Stop();
        }
        tutorialMusic.Play();
    }

    // Method to play intense music
    public void PlayIntenseMusic()
    {
        if (tutorialMusic.isPlaying)
        {
            tutorialMusic.Stop();
        }
        intenseMusic.Play();
    }

    private IEnumerator checkDeclareEnoughFood()
    {
        Debug.Log("In Function");
        float timeBetweenChecks = 0.2f;
        WaitForSeconds wait = new WaitForSeconds(timeBetweenChecks);
        while (true)
        {
            Debug.Log("CHECKING IF ENOUGH FOOD!");
            if (hasEnoughFood)
            {
                Debug.Log("Exiting");
                break;
            }
            else if (stackCollectables.current_num_points >= stackCollectables.num_points_to_win)
            {
                hasEnoughFood = true;
                PlayIntenseMusic();
            }
            yield return wait;
        }
    }
}