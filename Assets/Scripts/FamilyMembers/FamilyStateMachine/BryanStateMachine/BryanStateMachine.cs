using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BryanStateMachine
{
    public FamilyBaseState current_state;
    public FamilyBaseState previous_state;

    public BryanNeutralState neutral_state;
    public BryanActivatedState activated_state;
    public BryanAttackState attack_state;



    public BryanStateMachine(BryanNeutralState neutral, BryanActivatedState activated, BryanAttackState attack)
    {
        neutral_state = neutral;
        activated_state = activated;
        attack_state = attack;
    }

    public void UpdateCurrentState()
    {
        current_state?.UpdateState();
    }


    public void ChangeState(FamilyBaseState state)
    {
        if (current_state != state)
        {
            current_state.ExitState();
            previous_state = current_state;
            current_state = state;
            Debug.Log("Changed to state: " + state);
            state.EnterState();
        }
    }
}