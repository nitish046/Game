using UnityEngine;
using UnityEngine.AI;

public class FamilyActivatedState : FamilyBaseState
{
    private NavMeshAgent agent;
    private float shot_rate = 3f;
    private float shot_timer;
    private GameObject player;
    private GameObject hammer;
    private Transform hammer_origin;
    public override void EnterState(HenryStateMachine henry)
    {
        agent = henry.GetComponent<NavMeshAgent>();
        player = henry.GetComponent<HenryController>().player;
        hammer_origin = henry.transform.GetChild(1);
        hammer = henry.GetComponent<HenryController>().hammer;
        Debug.Log("hello");
    }

    public override void UpdateState(HenryStateMachine henry)
    {
        shot_timer += Time.deltaTime;
        henry.transform.LookAt(player.transform);
        if(shot_timer > shot_rate)
        {
            shoot();
            shot_timer = 0;
        }
    }

    public override void ExitState(HenryStateMachine henry)
    {

    }

    public void shoot()
    {

        hammer = Object.Instantiate(hammer, hammer_origin.position, hammer_origin.rotation);
        hammer.GetComponent<Rigidbody>().velocity = 40f * (hammer_origin.forward);
        Debug.Log("shoot");
    }
}
