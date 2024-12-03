using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
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

    public HouseMusic houseMusic; // Reference to the HouseMusic script

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

    protected Vector3[] getWaypointArray(string type)
    {
        return base.getWaypointArray(patrol_path_name, type);
    }

    // Call this method when Henry is activated
    public void OnActivated()
    {
        if (houseMusic != null)
        {
            houseMusic.SetFamilyMemberActivated(true);
        }
        else
        {
            Debug.LogWarning("HouseMusic is not assigned in HenryController.");
        }
    }

    // Call this method when Henry stops chasing the player
    public void OnDeactivated()
    {
        if (houseMusic != null)
        {
            houseMusic.SetFamilyMemberActivated(false);
        }
        else
        {
            Debug.LogWarning("HouseMusic is not assigned in HenryController.");
        }
    }
}
