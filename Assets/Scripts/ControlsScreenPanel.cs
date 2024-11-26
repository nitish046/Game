using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class ControlsScreenPanel : MonoBehaviour
{
  [SerializeField] private Image controlsScreen;
  private PlayerInputActions playerInput;
  private InputAction viewControlsPanel;

  private float enablePanel;
  // private GameInput input;

  private void Awake()
  {
    playerInput = new PlayerInputActions();
  }

  private void OnEnable()
  {
    viewControlsPanel = playerInput.Player.ViewControlsScreen;
    viewControlsPanel.Enable();
  }

  private void OnDisable()
  {
    viewControlsPanel.Disable();
  }

  // Start is called before the first frame update
  void Start()
  {
    controlsScreen.enabled = false;
  }

  // Update is called once per frame
  void Update()
  {
    enablePanel = viewControlsPanel.ReadValue<float>();
    if (enablePanel == 1.0f)
    {
      Debug.Log("Enabled");
      controlsScreen.enabled = true;
    }
    else
    {
      Debug.Log("Disabled");
      controlsScreen.enabled = false;
    }
  }
}
