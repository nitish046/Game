using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class HenryController : FamilyMember
{
    public GameObject[] Throwable_object_array;
    public float rate_of_fire = 3f;
    public float follow_distance = 5f;
    public Transform hammer_origin;
    public TMP_Text lose_text;
    public float duration = 5f;
    [SerializeField] private HideOnCollide collision_occur;
    protected override void Start()
    {
        base.Start();

        FamilyPatrolState patrol = new FamilyPatrolState(this, animator, nav_mesh_agent);
        HenryActivatedState activated = new HenryActivatedState(this, animator, nav_mesh_agent);
        FamilyFreezeState freeze = new FamilyFreezeState(this, animator, nav_mesh_agent);
        FamilySearchState search = new FamilySearchState(this, animator, nav_mesh_agent);
        stateMachine = new FamilyStateMachine(patrol, activated, freeze, search);

        hammer_origin = transform.GetChild(1);

        waypoint_array = getWaypointArray("Patrol");

        stateMachine.current_state = patrol;
        stateMachine.current_state.EnterState();
    }

    private void collisionOccur_onRaccoonFirstTimeOnTrash(object sender, System.EventArgs e)
    {
        // UnityEngine.Debug.Log("collisionOccur_onRaccoonFirstTimeOnTrash");
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        //stateMachine.ChangeState(stateMachine.patrol_state);
        //patrolCoroutine = StartCoroutine(Patrol(getWaypointArray("Patrol")));
    }
    private void HenryStartInHouse()
    {
        waypoint_array = getWaypointArray("Patrol");
        transform.position = waypoint_array[0];
    }

    protected Vector3[] getWaypointArray(string type)
    {
        // UnityEngine.Debug.Log("Henry getWaypointArray type");
        return base.getWaypointArray(patrol_path_name, type);
    }

}