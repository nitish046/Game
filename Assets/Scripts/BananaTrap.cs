using UnityEngine;

public abstract class Trap : MonoBehaviour
{
		// Duration for which the enemy will be immobilized
		public float effectDuration = 5f;

		// Abstract method to activate the trap
		public abstract void ActivateTrap(GameObject enemy);
}

public class BananaTrap : Trap
{
		private void OnTriggerEnter(Collider other)
		{
				if (other.CompareTag("Henry"))
				{
						print("here");
						ActivateTrap(other.gameObject);
						Destroy(gameObject); // Destroy the trap after activation
				}
		}

		public override void ActivateTrap(GameObject enemy)
		{
				HenryController henryScript = enemy.GetComponent<HenryController>();
				if (henryScript != null)
				{
						henryScript.Freeze(effectDuration, true); // Pass true to indicate it's a trap freeze
				}
		}
}