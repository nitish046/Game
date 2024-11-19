using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class FamilyStateMachine
{
    public FamilyBaseState current_state;
    public FamilyBaseState previous_state;

    public FamilyPatrolState patrol_state;
    public FamilyBaseState activated_state;
    public FamilyFreezeState freeze_state;
    public FamilySearchState search_state;

    //private Dictionary<Type, List>

    public FamilyStateMachine(FamilyPatrolState patrol, FamilyBaseState activated, FamilyFreezeState freeze, FamilySearchState search)
    {
        patrol_state = patrol;
        activated_state = activated;
        freeze_state = freeze;
        search_state = search;
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

    private class transition
    {
        public Func<bool> Condition { get; }
        public FamilyBaseState To { get; }

        public transition(FamilyBaseState to, Func<bool> condition)
        {
            To = to;
            Condition = condition;
        }
    }
}