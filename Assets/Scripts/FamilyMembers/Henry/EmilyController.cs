using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class EmilyController : FamilyMember
{
    public float charge_cooldown = 3f;
    public float follow_distance = 5f;

    public TMP_Text lose_text;
    public float duration = 5f;
    [SerializeField] private HideOnCollide collision_occur;

    public HouseMusic houseMusic; // Reference to the HouseMusic script

    protected override void Start()
    {
        base.Start();

        FamilyPatrolState patrol = new FamilyPatrolState(this, animator, nav_mesh_agent);
        EmilyActivatedState activated = new EmilyActivatedState(this, animator, nav_mesh_agent);
        FamilyFreezeState freeze = new FamilyFreezeState(this, animator, nav_mesh_agent);
        FamilySearchState search = new FamilySearchState(this, animator, nav_mesh_agent);
        EmilyChargeState charge = new EmilyChargeState(this, animator, nav_mesh_agent);
        stateMachine = new EmilyStateMachine(patrol, activated, freeze, search, charge);

        waypoint_array = getWaypointArray("Patrol");

        stateMachine.current_state = patrol;
        stateMachine.current_state.EnterState();
    }

    protected Vector3[] getWaypointArray(string type)
    {
        return base.getWaypointArray(patrol_path_name, type);
    }

    // Call this method when Emily is activated
    public void OnActivated()
    {
        if (houseMusic != null)
        {
            houseMusic.SetFamilyMemberActivated(true);
        }
        else
        {
            Debug.LogWarning("HouseMusic is not assigned in EmilyController.");
        }
    }

    // Call this method when Emily stops chasing the player
    public void OnDeactivated()
    {
        if (houseMusic != null)
        {
            houseMusic.SetFamilyMemberActivated(false);
        }
        else
        {
            Debug.LogWarning("HouseMusic is not assigned in EmilyController.");
        }
    }
}
