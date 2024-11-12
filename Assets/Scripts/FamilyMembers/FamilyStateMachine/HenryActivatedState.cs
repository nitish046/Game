using UnityEngine;
using UnityEngine.AI;

public class HenryActivatedState : FamilyBaseState
{
    //remove all eventually
    private float rate_of_fire;
    private float shot_timer;

    private GameObject player;
    private Transform hammer_origin;
    private GameObject[] Throwable_object_array;

    private float distance_to_player;
    private float follow_distance;

    private readonly HenryController member;
    private Animator member_animator;
    private NavMeshAgent nav_mesh_member;

    public HenryActivatedState(HenryController family_member, Animator animator, NavMeshAgent nav_mesh_agent)
    {
        member = family_member;
        member_animator = animator;
        nav_mesh_member = nav_mesh_agent;
    }

    public override void EnterState()
    {
        rate_of_fire = member.rate_of_fire;
        follow_distance = member.follow_distance;

        player = member.player;

        hammer_origin = member.hammer_origin;
        Throwable_object_array = member.Throwable_object_array;
    }

    public override void UpdateState()
    {
        shot_timer += Time.deltaTime;
        member.transform.LookAt(player.transform);
        distance_to_player = Vector3.Distance(member.transform.position, player.transform.position);
        KeepInRange();
        if (shot_timer > rate_of_fire)
        {
            shoot();
            shot_timer = 0;
        }
    }
    public override void ExitState()
    {
        nav_mesh_member.ResetPath();
    }

    private void KeepInRange()
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

    public void shoot()
    {
        GameObject tool_to_throw = Throwable_object_array[Random.Range(0, Throwable_object_array.Length)];

        GameObject thrown_object = GameObject.Instantiate(tool_to_throw, hammer_origin.position, hammer_origin.rotation);
        Rigidbody object_rigid_body = thrown_object.GetComponent<Rigidbody>();
        object_rigid_body.velocity = 10f * (hammer_origin.forward);
        object_rigid_body.angularVelocity = 20f * Vector3.one;
        //Debug.Log("shoot");
    }
}