using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideOnCollide : MonoBehaviour
{
  void OnTriggerEnter(Collider other)
  {
    other.gameObject.SetActive(false);
    while (!Input.GetKeyDown(KeyCode.Space))
    {
      Debug.Log("hidden");
    }
    other.gameObject.transform.position = new Vector3(-8.08f, 1.45f, -3.919f);
    other.gameObject.SetActive(true);
  }
}
