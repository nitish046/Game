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
    if (other.CompareTag("Henry")) //If this is triggered
    {
      print("here");
      ActivateTrap(other.gameObject);
      Destroy(gameObject); // Destroy the trap after activation
    }
  }

  public override void ActivateTrap(GameObject enemy)
  {
    FamilyMember familyMemberScript = enemy.GetComponent<FamilyMember>();
    if (familyMemberScript != null)
    {
      familyMemberScript.Freeze(effectDuration, true); // Pass true to indicate it's a trap freeze
    }
  }
}