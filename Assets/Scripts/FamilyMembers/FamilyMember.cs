using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;


public abstract class FamilyMember : MonoBehaviour
{
    //need to remake as public
    //public abstract float MovementSpeed { get; }
    //public abstract float RotationSpeed { get; }

    [SerializeField] float distance;

    [SerializeField] protected Transform path;
    public Vector3[] waypoint_array;
    [SerializeField] protected float waypoint_size = .4f;
    [SerializeField] public float waypoint_wait_time = 2f;

    public AudioSource splash;

    [SerializeField] public GameObject player;

    protected Animator animator;
    [SerializeField] protected GameObject win_lose_controller;


    public FieldOfView fieldOfView;
    [SerializeField] protected float viewRadius;
    [Range(0, 360)][SerializeField] protected float viewAngle = 120;
    [Range(0, 360)][SerializeField] protected float periferalAngle = 190;
    [SerializeField] protected LayerMask targetMask;
    [SerializeField] protected LayerMask obstructionMask;
    [SerializeField] protected LayerMask interactableObstructionMask;

    public bool hasSeenPlayer = false;
    public float patrolDuration = 10;
    public float secondsSinceSeenPlayer = 0;
    protected Coroutine timerCoroutine;

    public bool is_charging = false;

    public GameObject patrolPointOperator;

    public Material MainColor, FreezeColor;

    protected Vector3 player_last_seen_position;

    public FamilyStateMachine stateMachine;
    public NavMeshAgent nav_mesh_agent;

    protected virtual void Start()
    {
        animator = transform.GetChild(0).GetComponent<Animator>();
        nav_mesh_agent = GetComponent<NavMeshAgent>();
        fieldOfView = gameObject.AddComponent<FieldOfView>();
        fieldOfView.makeFOV(player, this.gameObject, viewRadius, viewAngle, periferalAngle, targetMask, obstructionMask, interactableObstructionMask);
        StartCoroutine(fieldOfView.FOVRoutine());
    }

    protected void Update()
    {
        distance = Vector3.Distance(transform.position, player.transform.position);
        if (stateMachine.current_state != stateMachine.freeze_state)
        {
            CheckForRaccoon();
        }
        stateMachine.UpdateCurrentState();
    }

    protected void CheckForRaccoon()
    {
        if(is_charging)
        {
            return;
        }

        if (fieldOfView.canSeePlayer)
        {
            hasSeenPlayer = true;
            player_last_seen_position = player.transform.position;
            SeesRaccoon();
        }
        else if (hasSeenPlayer)
        {
            stateMachine.search_state.search_location = player_last_seen_position;
            if (stateMachine.current_state != stateMachine.freeze_state)
            {
                stateMachine.ChangeState(stateMachine.search_state);
                hasSeenPlayer = false;
                // Go on Patrol, start timer with secondsSinceSeenPlayer
                //timerCoroutine = StartCoroutine(seePlayerTimer());
            }
            else
            {
                stateMachine.previous_state = stateMachine.search_state;
            }
        }
    }
    /*
    protected IEnumerator seePlayerTimer()
    {
        isTimerCoroutineRunning = true;
        WaitForSeconds wait = new WaitForSeconds(1.0f);
        while (!fieldOfView.canSeePlayer)
        {
            yield return wait;
            if (stateMachine.current_state != stateMachine.freeze_state)
            {
                secondsSinceSeenPlayer++;
            }
            if (secondsSinceSeenPlayer >= 5)
            {
                stateMachine.ChangeState(stateMachine.patrol_state);
                //call to implement normal behavior
                isTimerCoroutineRunning = false;
                secondsSinceSeenPlayer = 0;
                yield break;
            }
        }
        isTimerCoroutineRunning = false;
        secondsSinceSeenPlayer = 0;
    }
    */

    protected virtual void SeesRaccoon()
    {
        if (stateMachine.current_state != stateMachine.freeze_state && (stateMachine.current_state.GetType() != typeof(EmilyChargeState)))
        {
            stateMachine.ChangeState(stateMachine.activated_state);
        }
    }

    public void RespondToSound(DetectableSound sound)
    {
        stateMachine.search_state.search_location = sound.sound_position;
        if(stateMachine.current_state == stateMachine.search_state || stateMachine.current_state == stateMachine.patrol_state)
        {
            stateMachine.ChangeState(stateMachine.search_state);
        }
        print("Hear Song");
    }

    protected virtual Vector3[] getWaypointArray(string name, string type)
    {
        UnityEngine.Debug.Log("FamilyMember getWaypointArray type " + name + " " + type);
        UnityEngine.Debug.Log(patrolPointOperator.GetComponent<FamilyPatrolPoints>());
        Vector3[] waypoint_array = patrolPointOperator.GetComponent<FamilyPatrolPoints>().findPatrolPoints(name, type);

        return waypoint_array;
    }
}


