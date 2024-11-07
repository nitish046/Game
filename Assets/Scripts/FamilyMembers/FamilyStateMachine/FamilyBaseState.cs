using UnityEditor;
using UnityEngine;

public abstract class FamilyBaseState 
{
    public abstract void EnterState(HenryStateMachine henry);

    public abstract void UpdateState(HenryStateMachine henry);

    public abstract void ExitState(HenryStateMachine henry);
}
