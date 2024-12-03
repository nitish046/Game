using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class CPSmokeBomb : Trap
{
  [SerializeField] private AudioSource audioSource;
  private Transform bombTransform;
  [SerializeField] private float expansionTime;

  private void Awake()
  {
    // Initialize the audioSource component
    audioSource = GetComponent<AudioSource>();
    bombTransform = GetComponent<Transform>();
  }


  public override void ActivateTrap(GameObject enemy)
  {
    StartCoroutine(SmokeCloudGrows());
  }

  private IEnumerator SmokeCloudGrows() // temporary smoke cloud function until actual smoke animation is possible
  {
    int iterations = 100;
    float rate = expansionTime / (float)iterations;
    WaitForSeconds waitForIt = new WaitForSeconds(rate);
    float increment = 5.0f * rate;
    for (int i = 0; i < iterations; i++)
    {
      bombTransform.localScale += new Vector3(increment, increment, increment);
      //Debug.Log(i + ", " + bombTransform.localScale);
      yield return waitForIt;
    }
    StartCoroutine(SmokeCloudFades());
  }

  private IEnumerator SmokeCloudFades() // temporary smoke cloud function until actual smoke animation is possible
  {
    int iterations = 100;
    float rate = effectDuration / (float)iterations;
    WaitForSeconds waitForIt = new WaitForSeconds(rate);

    Renderer bombRenderer = bombTransform.GetComponent<Renderer>();
    Color bombColor = bombRenderer.material.color;
    float bombAlphaValue = bombColor.a;
    float increment = bombAlphaValue / iterations;

    for (int i = 0; i < iterations; i++)
    {
      bombAlphaValue -= increment;
      bombRenderer.material.color = new Color(bombRenderer.material.color.r, bombRenderer.material.color.g, bombRenderer.material.color.b, bombAlphaValue);
      // Debug.Log(bombTransform.GetComponent<Renderer>().material.color);
      yield return waitForIt;
    }
    Destroy(this.gameObject);
  }

  // Method to play the trap sound
  private void PlayTrapSound()
  {
    if (audioSource != null)
    {
      Debug.Log("Playing smoke bomb sound effect");  // Debug message to check if it's being triggered
      audioSource.Play();  // Play the assigned sound
    }
    else
    {
      Debug.LogWarning("AudioSource is not assigned on the trap prefab.");
    }
  }


}