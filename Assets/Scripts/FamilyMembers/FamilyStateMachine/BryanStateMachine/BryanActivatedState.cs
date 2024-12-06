using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BryanActivatedState : FamilyBaseState
{
  private readonly BryanController memberBryan;
  private Animator member_animator;
  private NavMeshAgent nav_mesh_member;

  private Vector3 guard_position;
  private float distance_to_player;
  private float attack_range;

  private bool done_walking = false;

  private GameObject player;

  public BryanActivatedState(BryanController family_member, Animator animator, NavMeshAgent nav_mesh_agent)
  {
    memberBryan = family_member;
    member_animator = animator;
    nav_mesh_member = nav_mesh_agent;
  }

  public override void EnterState()
  {
    memberBryan.katana.GetComponent<Renderer>().enabled = true;
    guard_position = memberBryan.guardPosition.transform.position;
    attack_range = memberBryan.attack_range;
    player = memberBryan.player;
    nav_mesh_member.SetDestination(guard_position);
    member_animator.SetTrigger("isWalking");
  }

  public override void UpdateState()
  {
    distance_to_player = Vector3.Distance(memberBryan.transform.position, player.transform.position);
    if (nav_mesh_member.remainingDistance <= 0.2f)
    {
      if (!done_walking && nav_mesh_member.hasPath)
      {
        member_animator.ResetTrigger("isWalking");
        member_animator.SetTrigger("isIdle");
        done_walking = true;
        memberBryan.transform.LookAt(player.transform.position);
      }
      else
      {
        Debug.Log("can see player? " + memberBryan.fieldOfView.canSeePlayer);
        if (memberBryan.fieldOfView.canSeePlayer)
        {
          memberBryan.playerLastTransform = player.transform;
          Debug.Log("can see player");
        }
        else { Debug.Log("cannot see player"); }
        InAttackRange();
        memberBryan.transform.LookAt(memberBryan.playerLastTransform);
      }
    }
  }

  public override void ExitState()
  {
    member_animator.ResetTrigger("isIdle");
    nav_mesh_member.ResetPath();
  }

  private void InAttackRange()
  {
    if (distance_to_player <= attack_range)
    {
      memberBryan.stateMachine.ChangeState(memberBryan.stateMachine.attack_state);
    }
  }
}
