using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EmilyChargeState : FamilyBaseState
{
    private GameObject player;

    private float normal_speed;
    private float charge_speed;

    private float distance_to_player;
    private float charge_stop_distance = .5f;

    private readonly EmilyController member;
    private Animator member_animator;
    private NavMeshAgent nav_mesh_member;

    public EmilyChargeState(EmilyController family_member, Animator animator, NavMeshAgent nav_mesh_agent)
    {
        member = family_member;
        member_animator = animator;
        nav_mesh_member = nav_mesh_agent;
    }

    public override void EnterState()
    {
        player = member.player;
        normal_speed = nav_mesh_member.speed;
        charge_speed = normal_speed * 5;
        nav_mesh_member.destination = player.transform.position;
    }

    public override void UpdateState()
    {
        nav_mesh_member.speed = charge_speed;
        distance_to_player = Vector3.Distance(member.transform.position, player.transform.position);
        if(nav_mesh_member.remainingDistance < charge_stop_distance)
        {
            member.stateMachine.ChangeState(member.stateMachine.activated_state);
        }
    }

    public override void ExitState()
    {
        nav_mesh_member.velocity = Vector3.zero;
        nav_mesh_member.speed = normal_speed;
        if (distance_to_player <= charge_stop_distance + .1f)
        {
            player.GetComponent<LifeTracker>().LoseLife();

        }
        nav_mesh_member.ResetPath();
    }

}
