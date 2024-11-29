using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class BryanAttackState : FamilyBaseState
{
    private readonly BryanController member;
    private Animator member_animator;
    private NavMeshAgent nav_mesh_member;

    public BryanAttackState(BryanController family_member, Animator animator, NavMeshAgent nav_mesh_agent)
    {
        member = family_member;
        member_animator = animator;
        nav_mesh_member = nav_mesh_agent;
    }

    public override void EnterState()
    {
        member_animator.applyRootMotion = true;
        member_animator.SetTrigger("isAttacking");
        Debug.Log("ATTACKED");
        //member.stateMachine.ChangeState(member.stateMachine.activated_state);
    }

    public override void UpdateState()
    {

    }

    public override void ExitState()
    {
        member_animator.applyRootMotion = false;
    }
}
