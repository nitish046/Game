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

    private GameObject player;

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
        player = member.player;
    }

    public override void UpdateState()
    {
        if(member_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            member.stateMachine.ChangeState(member.stateMachine.activated_state);
        }
        member.transform.LookAt(player.transform);
    }

    public override void ExitState()
    {
        member_animator.applyRootMotion = false;
        member_animator.ResetTrigger("isAttacking");
        member_animator.SetTrigger("isIdle");
    }
}
