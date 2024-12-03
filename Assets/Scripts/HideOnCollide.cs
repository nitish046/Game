using MaskedMischiefNamespace;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideOnCollide : MonoBehaviour
{
		public event EventHandler onRaccoonFirstTimeOnTrash;

		
		private bool can_hide = false;
		private bool already_hiding = false;
		private bool not_entered = true;
		private Renderer[] player_renderers;
		private PlayerRunner movement_script;
		private GameObject player;

		
		[SerializeField] private GameInput game_input;
		
		public int hiding_layer = 8;
		private int original_layer;

		private void Start()
		{
				game_input.on_interact_action += GameInput_OnInteractAction;
		}

		private void GameInput_OnInteractAction(object sender, System.EventArgs e)
		{
				//Debug.Log( " can_hide: " + can_hide + " already_hiding: " + already_hiding);
				if ((can_hide && !already_hiding) && player_renderers != null)
				{
						foreach(Renderer renderer in player_renderers)
						{
								renderer.enabled = false;
						}
						if(movement_script != null)
						{
								movement_script.enabled = false;
						}
						ChangeLayer(hiding_layer);
						already_hiding = true;
						
				}
				else if(already_hiding && player_renderers != null)
				{
						foreach (Renderer renderer in player_renderers)
						{
								renderer.enabled = true;
						}

						if (movement_script != null)
						{
								movement_script.enabled = true;
						}
						already_hiding = false;
						ChangeLayer(original_layer);
				}
		}

		void OnTriggerEnter(Collider other)
		{
				if(not_entered)
				{
						onRaccoonFirstTimeOnTrash?.Invoke(this, System.EventArgs.Empty);
						not_entered = false;
				}
				can_hide = true;
				if(other.CompareTag("Player"))
				{
					player_renderers = other.gameObject.GetComponentsInChildren<Renderer>();
					movement_script = other.gameObject.GetComponentInParent<PlayerRunner>();
					original_layer = other.gameObject.layer;
					player = other.gameObject;
				}
		}


	 void OnTriggerExit(Collider other)
	 {
				can_hide = false;
				already_hiding = false;
				player_renderers = null;
				movement_script = null;
				//Debug.Log("left collison");
	 }

    private void ChangeLayer(int layer)
    {
		Debug.Log("Change Layer");
        if (player != null)
        {
                player.gameObject.layer = layer;
				Debug.Log(player.gameObject.layer);   
        }
    }
}
