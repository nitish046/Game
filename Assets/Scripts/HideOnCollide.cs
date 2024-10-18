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
		private Player movement_script;

		
		[SerializeField] private GameInput game_input;
		

		private void Start()
		{
				game_input.on_interact_action += GameInput_OnInteractAction;
		}

		private void GameInput_OnInteractAction(object sender, System.EventArgs e)
		{
				Debug.Log( " can_hide: " + can_hide + " already_hiding: " + already_hiding);
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
				player_renderers = other.gameObject.GetComponentsInChildren<Renderer>();
				movement_script = other.gameObject.GetComponentInParent<Player>();
		}


	 void OnTriggerExit(Collider other)
	 {
				can_hide = false;
				already_hiding = false;
				player_renderers = null;
				movement_script = null;
				Debug.Log("left collison");
	 }
}
