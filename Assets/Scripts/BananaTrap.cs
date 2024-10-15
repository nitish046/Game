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
            ActivateTrap(other.gameObject);
            Destroy(gameObject); // Destroy the trap after activation
        }
    }

    public override void ActivateTrap(GameObject enemy)
    {
        // Access the enemy's script to immobilize it
        HenryController henryScript = enemy.GetComponent<HenryController>();
        if (henryScript != null)
        {
            henryScript.Freeze();
        }
    }
}
