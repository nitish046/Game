using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideOnCollide : MonoBehaviour
{
    public event EventHandler onRaccoonFirstTimeOnTrash;

    
    private bool canHide = false;
    private bool alreadyHiding = false;
    private int timeEntered = 0;
    private Renderer[] playerRenderers;
    private Player movementScript;
    [SerializeField] private GameInput gameInput;
    

    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        Debug.Log( " canHide: " + canHide + " alreadyHiding: " + alreadyHiding);
        if ((canHide && !alreadyHiding) && playerRenderers != null)
        {
            foreach(Renderer renderer in playerRenderers)
            {
                renderer.enabled = false;
            }
            if(movementScript != null)
            {
                movementScript.enabled = false;
            }
            alreadyHiding = true;
            
        }
        else if(alreadyHiding && playerRenderers != null)
        {
            foreach (Renderer renderer in playerRenderers)
            {
                renderer.enabled = true;
            }

            if (movementScript != null)
            {
                movementScript.enabled = true;
            }
            alreadyHiding = false;

        }
    }

    void OnTriggerEnter(Collider other)
    {
        timeEntered++;
        if(timeEntered == 1)
        {
            onRaccoonFirstTimeOnTrash?.Invoke(this, System.EventArgs.Empty);
        }
        canHide = true;
        playerRenderers = other.gameObject.GetComponentsInChildren<Renderer>();
        movementScript = other.gameObject.GetComponentInParent<Player>();
    }


   void OnTriggerExit(Collider other)
   {
        canHide = false;
        alreadyHiding = false;
        playerRenderers = null;
        movementScript = null;
        Debug.Log("left collison");
   }
}
