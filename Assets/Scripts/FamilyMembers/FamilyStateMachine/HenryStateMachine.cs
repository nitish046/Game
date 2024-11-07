using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HenryStateMachine : MonoBehaviour
{
    FamilyBaseState current_state;
    public FamilyBaseState previous_state;

    public FamilyPatrolState patrol_state = new FamilyPatrolState();
    public FamilyActivatedState activated_state = new FamilyActivatedState();
    public FamilyFreezeState freeze_state = new FamilyFreezeState();

    public Material FreezeColor;
    public AudioSource splash;
    public GameObject player;

    //remove these later
    public float movement_speed = 5f;
    public float rotation_speed = 90f;
    public float waypoint_wait_time = 2f;

    private void Start()
    {
        current_state = patrol_state;

        current_state.EnterState(this);
    }

    private void Update()
    {
        current_state?.UpdateState(this);
    }

    public void ChangeState(FamilyBaseState state)
    {
        if(current_state != state)
        {
            current_state.ExitState(this);
            previous_state = current_state;
            current_state = state;
            Debug.Log("Changed to state: " + state);
            state.EnterState(this);
        }
    }
}
