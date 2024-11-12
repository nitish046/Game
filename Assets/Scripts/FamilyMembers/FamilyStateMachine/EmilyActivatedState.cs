using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EmilyActivatedState : FamilyBaseState
{
    private GameObject player;

    private float charge_timer;
    private float charge_cooldown;

    private float distance_to_player;
    private float follow_distance;
    private float charge_stop_distance = .5f;

    private float normal_speed;
    private float charge_speed;

    private bool is_charging = false;

    private readonly EmilyController member;
    private Animator member_animator;
    private NavMeshAgent nav_mesh_member;

    public EmilyActivatedState(EmilyController family_member, Animator animator, NavMeshAgent nav_mesh_agent)
    {
        member = family_member;
        member_animator = animator;
        nav_mesh_member = nav_mesh_agent;
    }

    public override void EnterState()
    {
        charge_cooldown = member.charge_cooldown;
        follow_distance = member.follow_distance;
        normal_speed = nav_mesh_member.speed;
        charge_speed = normal_speed * 5;
        is_charging = false;
        player = member.player;
    }

    public override void UpdateState()
    {
        if(!is_charging)
        {
            charge_timer += Time.deltaTime;
        }
        member.transform.LookAt(player.transform);
        distance_to_player = Vector3.Distance(member.transform.position, player.transform.position);
        KeepInRange();
        
        if (is_charging && distance_to_player <= charge_stop_distance)
        {
            EndCharge();
        }

        if (!is_charging && charge_timer > charge_cooldown)
        {
            Charge();
        }
        
    }

    public override void ExitState()
    {
        nav_mesh_member.speed = normal_speed;
        nav_mesh_member.ResetPath();
    }

    public void Charge()
    {
        nav_mesh_member.speed = charge_speed; 
        nav_mesh_member.destination = player.transform.position;
        is_charging = true;
        charge_timer = 0;
        distance_to_player = Vector3.Distance(member.transform.position, player.transform.position);

    }

    public void EndCharge()
    {
        nav_mesh_member.velocity = Vector3.zero;
        nav_mesh_member.speed = normal_speed;
        is_charging = false;
        if (distance_to_player <= charge_stop_distance)
        {
            player.GetComponent<LifeTracker>().LoseLife();

        }
    }

    private void KeepInRange()
    {
        if(!is_charging)
        {
            if (distance_to_player > follow_distance)
            {
                nav_mesh_member.SetDestination(player.transform.position);
            }
            else
            {
                nav_mesh_member.ResetPath();
            }
        }
    }
}
