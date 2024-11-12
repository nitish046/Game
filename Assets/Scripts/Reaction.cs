using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reaction : MonoBehaviour
{
  public float effectDuration = 5f;

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }

  private void OnCollisionEnter(Collision collision)
  {
    if (collision.gameObject.tag == "Terrain")
    {
      Destroy(gameObject);
    }
    if (collision.gameObject.tag == "Henry")
    {
      // Call Freeze with effectDuration and isTrapFreeze = false (since this isn't a trap)
      //collision.gameObject.GetComponent<HenryController>().Freeze(effectDuration, false);
      FamilyStateMachine state_machine = collision.gameObject.GetComponent<HenryController>().stateMachine;
      state_machine.freeze_state.effect_duration = effectDuration;
      state_machine.freeze_state.is_trap_slip = false;
      state_machine.ChangeState(state_machine.freeze_state);
      Destroy(gameObject); // Destroy the object (like a tomato) after hitting Henry
    }
  }
}

