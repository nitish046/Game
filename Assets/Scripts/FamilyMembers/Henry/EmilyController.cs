using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class EmilyController : FamilyMember
{

    public float charge_cooldown = 3f;
    public float follow_distance = 5f;

    public TMP_Text lose_text;
    public float duration = 5f;
    [SerializeField] private HideOnCollide collision_occur;
    protected override void Start()
    {
        base.Start();

        FamilyPatrolState patrol = new FamilyPatrolState(this, animator, nav_mesh_agent);
        EmilyActivatedState activated = new EmilyActivatedState(this, animator, nav_mesh_agent);
        FamilyFreezeState freeze = new FamilyFreezeState(this, animator, nav_mesh_agent);
        FamilySearchState search = new FamilySearchState(this, animator, nav_mesh_agent);
        stateMachine = new FamilyStateMachine(patrol, activated, freeze, search);


        waypoint_array = getWaypointArray("Patrol");

        stateMachine.current_state = patrol;
        stateMachine.current_state.EnterState();
    }

    protected Vector3[] getWaypointArray(string type)
    {
        return base.getWaypointArray("emily", type);
    }
}
