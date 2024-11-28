using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BryanNeutralState : FamilyBaseState
{
    private readonly BryanController member;
    private Animator member_animator;

    public BryanNeutralState(BryanController family_member, Animator animator)
    {
        member = family_member;
        member_animator = animator;
    }

    public override void EnterState()
    {
        throw new System.NotImplementedException();
    }

    public override void ExitState()
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateState()
    {
        throw new System.NotImplementedException();
    }
}
