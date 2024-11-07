using UnityEngine;

public class FamilyActivatedState : FamilyBaseState
{
    public override void EnterState(HenryStateMachine henry)
    {
        Debug.Log("hello");
    }

    public override void UpdateState(HenryStateMachine henry)
    {

    }

    public override void ExitState(HenryStateMachine henry)
    {

    }
}
