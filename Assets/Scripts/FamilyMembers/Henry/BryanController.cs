using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BryanController : MonoBehaviour
{
    public BryanStateMachine stateMachine;
    public NavMeshAgent nav_mesh_agent;
    protected Animator animator;

    [SerializeField] public GameObject player;
    public GameObject katana;
    public GameObject guardPosition;
    public float attack_range = 2;

    public HouseMusic houseMusic; // Reference to the HouseMusic script

    private void Start()
    {
        nav_mesh_agent = GetComponent<NavMeshAgent>();
        animator = transform.GetChild(0).GetComponent<Animator>();

        BryanNeutralState neutral = new BryanNeutralState(this, animator);
        BryanActivatedState activated = new BryanActivatedState(this, animator, nav_mesh_agent);
        BryanAttackState attack = new BryanAttackState(this, animator, nav_mesh_agent);

        stateMachine = new BryanStateMachine(neutral, activated, attack);

        stateMachine.current_state = neutral;
        stateMachine.current_state.EnterState();
    }

    private void Update()
    {
        stateMachine.UpdateCurrentState();
    }

    public void RespondToSteal()
    {
        if (stateMachine.current_state == stateMachine.neutral_state)
        {
            stateMachine.ChangeState(stateMachine.activated_state);

            if (houseMusic != null)
            {
                houseMusic.SetBryanActivated(true);
            }
            else
            {
                Debug.LogWarning("HouseMusic is not assigned in BryanController.");
            }
        }
    }
}
