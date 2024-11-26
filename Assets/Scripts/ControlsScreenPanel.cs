using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlsScreenPanel : MonoBehaviour
{
  [SerializeField] private Image controlsScreen;
  [SerializeField] PlayerInputActions input;
  // Start is called before the first frame update
  void Start()
  {
    controlsScreen.enabled = false;
  }

  // Update is called once per frame
  void Update()
  {

  }
}
