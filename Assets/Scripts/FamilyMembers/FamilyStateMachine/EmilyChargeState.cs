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
    private Vector3 charge_direction;
    private bool was_frozen;

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
        member.is_charging = true;
        player = member.player;
        normal_speed = nav_mesh_member.speed;
        charge_speed = normal_speed * 3;

        nav_mesh_member.speed = charge_speed;
        if(!was_frozen)
        {
            charge_direction = (player.transform.position - member.transform.position).normalized;
        }
        was_frozen = true;
        nav_mesh_member.isStopped = true;
    }

    public override void UpdateState()
    {
        nav_mesh_member.Move(charge_direction * charge_speed * Time.deltaTime);

        RaycastHit hit;
        Debug.DrawRay(member.transform.position + Vector3.up, charge_direction, Color.green, 1f);
        if (Physics.Raycast(member.transform.position + Vector3.up, charge_direction, out hit, 1f))
        {
            nav_mesh_member.velocity = Vector3.zero;
            HandleCollision(hit.collider);  
        }
    }

    private void HandleCollision(Collider collider)
    {
        member.stateMachine.ChangeState(member.stateMachine.activated_state);

        if (collider.CompareTag("Player"))
        {
            player.GetComponent<LifeTracker>().LoseLife();
        }

        //Debug.Log("Emily collided with " + collider.gameObject.name);
        member.is_charging = true;
        was_frozen = false;
    }


    public override void ExitState()
    {
        nav_mesh_member.velocity = Vector3.zero;
        nav_mesh_member.speed = normal_speed;
        nav_mesh_member.ResetPath();
    }

}
