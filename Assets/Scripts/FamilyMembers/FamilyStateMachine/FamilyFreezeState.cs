using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FamilyFreezeState : FamilyBaseState
{
    private SkinnedMeshRenderer skinned_mesh_renderer;
    private Material MainColor;

    public float effect_duration;
    public bool is_trap_slip;

    public Vector3 pre_fall_position;
    public float pre_fall_y;

    private readonly FamilyMember member;
    private Animator member_animator;
    private NavMeshAgent nav_mesh_member;

    public FamilyFreezeState(FamilyMember family_member, Animator animator, NavMeshAgent nav_mesh_agent)
    {
        member = family_member;
        member_animator = animator;
        nav_mesh_member = nav_mesh_agent;
    }

    public override void EnterState()
    {
        member_animator.enabled = false;
        skinned_mesh_renderer = member.transform.GetChild(0).GetChild(0).GetComponent<SkinnedMeshRenderer>();
        MainColor = skinned_mesh_renderer.material;
        pre_fall_position = member.transform.GetChild(0).GetComponent<Transform>().position;
        pre_fall_y = pre_fall_position.y;
        Freeze(effect_duration, is_trap_slip);
    }

    public override void UpdateState()
    {

    }

    public override void ExitState()
    {
        member_animator.enabled = true;
        nav_mesh_member.enabled = true;
        member_animator.ResetTrigger("isFalling");
        Debug.Log("Exiting Freeze state");
    }

    public void Freeze(float freezeDuration, bool slip)
    {
        UnityEngine.Debug.Log("Entering Henry Freeze");
        float duration = freezeDuration; // Set the freeze duration based on the trap
        nav_mesh_member.enabled = false;
        // Only change color if it's not a trap freeze
        if (!slip)
        {
            skinned_mesh_renderer.material = member.FreezeColor; // Change to FreezeColor
        }

        if (member.splash != null && member.splash.clip != null)
        {
            member.splash.Play();
        }
        else
        {
            UnityEngine.Debug.LogWarning("Splash AudioSource or AudioClip is not assigned.");
        }

        // If it's a trap freeze, make Henry fall and pause animation
        if (slip)
        {
            nav_mesh_member.enabled = false;
            member_animator.enabled = true;
            member_animator.applyRootMotion = true;
            pre_fall_position = member.transform.GetChild(0).GetComponent<Transform>().position;
            member_animator.ResetTrigger("isWalking");
            member_animator.ResetTrigger("isIdle");
            member_animator.SetTrigger("isFalling");
            //member.transform.rotation = Quaternion.Euler(90f, member.transform.rotation.eulerAngles.y, member.transform.rotation.eulerAngles.z); // Rotate Henry to appear as if he has fallen down
            UnityEngine.Debug.Log("Henry has been frozen and fallen to the ground.");
        }
        else
        {
            UnityEngine.Debug.Log("Henry has been frozen by another method.");
        }

        member.StartCoroutine(delay(slip, duration)); // Pass the freeze type to the delay
    }

    IEnumerator delay(bool slip, float duration)
    {
        yield return new WaitForSeconds(duration);

        skinned_mesh_renderer.material = MainColor; // Reset color

        if (slip)
        {
            member_animator.applyRootMotion = false;
            member_animator.enabled = true; // Resume animations

            // Reset rotation and position
            //member.transform.GetChild(0).rotation = Quaternion.Euler(0f, member.transform.rotation.eulerAngles.y, member.transform.rotation.eulerAngles.z);


            nav_mesh_member.enabled = false; // Disable NavMeshAgent temporarily
            member.transform.GetChild(0).position = pre_fall_position;
            member_animator.ResetTrigger("isFalling");
            member_animator.SetTrigger("isIdle");

            yield return new WaitForSeconds(0.1f); // Add a slight delay to ensure position update takes effect
            nav_mesh_member.enabled = true; // Re-enable NavMeshAgent
        }
        
        member.stateMachine.ChangeState(member.stateMachine.previous_state);
    }

}
