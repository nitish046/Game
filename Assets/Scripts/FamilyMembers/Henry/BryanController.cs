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

  public FieldOfView fieldOfView;
  [SerializeField] protected float viewRadius;
  [Range(0, 360)][SerializeField] protected float viewAngle = 120;
  [Range(0, 360)][SerializeField] protected float periferalAngle = 190;
  [SerializeField] protected LayerMask targetMask;
  [SerializeField] protected LayerMask obstructionMask;
  [SerializeField] protected LayerMask interactableObstructionMask;
  public Transform playerLastTransform;

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

    fieldOfView = gameObject.AddComponent<FieldOfView>();
    fieldOfView.makeFOV(player, this.gameObject, viewRadius, viewAngle, periferalAngle, targetMask, obstructionMask, interactableObstructionMask);
    StartCoroutine(fieldOfView.FOVRoutine());
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
        Debug.Log("BryanController: Calling SetBryanActivated(true)");
        houseMusic.SetBryanActivated(true);
      }
      else
      {
        Debug.LogWarning("HouseMusic is not assigned in BryanController.");
      }
    }
  }
}
