using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaccoonAnimationController : MonoBehaviour
{
    Animator player_animator;

    private void Start()
    {
        player_animator = transform.GetChild(0).GetComponent<Animator>();
    }
}
