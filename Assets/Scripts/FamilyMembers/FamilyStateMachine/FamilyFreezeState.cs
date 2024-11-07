using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FamilyFreezeState : FamilyBaseState
{
    private Animator animator;
    private SkinnedMeshRenderer skinned_mesh_renderer;
    private Material MainColor;

    public float effect_duration;
    public bool is_trap_slip;


    public override void EnterState(HenryStateMachine henry)
    {
        animator = henry.transform.GetChild(0).GetComponent<Animator>();
        skinned_mesh_renderer = henry.transform.GetChild(0).GetChild(0).GetComponent<SkinnedMeshRenderer>();
        MainColor = skinned_mesh_renderer.material;
        Debug.Log(MainColor);
        Freeze(henry, effect_duration, is_trap_slip);
    }

    public override void UpdateState(HenryStateMachine henry)
    {

    }

    public override void ExitState(HenryStateMachine henry)
    {

    }

    public void Freeze(HenryStateMachine henry, float freezeDuration, bool slip)
    {
        UnityEngine.Debug.Log("Entering Henry Freeze");
        float duration = freezeDuration; // Set the freeze duration based on the trap

        // Only change color if it's not a trap freeze
        if (!slip)
        {
            skinned_mesh_renderer.material = henry.FreezeColor; // Change to FreezeColor
        }

        if (henry.splash != null && henry. splash.clip != null)
        {
            henry.splash.Play();
        }
        else
        {
            UnityEngine.Debug.LogWarning("Splash AudioSource or AudioClip is not assigned.");
        }

        // If it's a trap freeze, make Henry fall and pause animation
        if (slip)
        {
            animator.enabled = false; // Pause all animations
            henry.transform.rotation = Quaternion.Euler(90f, henry.transform.rotation.eulerAngles.y, henry.transform.rotation.eulerAngles.z); // Rotate Henry to appear as if he has fallen down
            UnityEngine.Debug.Log("Henry has been frozen and fallen to the ground.");
        }
        else
        {
            UnityEngine.Debug.Log("Henry has been frozen by another method.");
        }

        henry.StartCoroutine(delay(henry, slip, duration)); // Pass the freeze type to the delay
    }

    IEnumerator delay(HenryStateMachine henry, bool slip, float duration)
    {
        yield return new WaitForSeconds(duration);

        skinned_mesh_renderer.material = MainColor; // Reset color

        if (slip)
        {
            animator.enabled = true; // Resume animations
            henry.transform.rotation = Quaternion.Euler(0f,henry.transform.rotation.eulerAngles.y, henry.transform.rotation.eulerAngles.z); // Reset rotation to stand Henry back up
                                                                                                                           //Debug.Log("Henry has unfrozen and is standing up.");
        }
        henry.ChangeState(henry.previous_state);
        //Debug.Log("Henry has unfrozen.");
    }
}
