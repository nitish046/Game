using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTransition : MonoBehaviour
{
    public FadeManager fadeManager;
    //private Transform teleport_transform;
    public Transform teleport_transform;

    private void Awake()
    {
        //teleport_transform = GetComponentInChildren<Transform>();
    }

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(TeleportWithFade(other));
    }

    private IEnumerator TeleportWithFade(Collider other)
    {
        fadeManager.PlayFadeOut();

        yield return new WaitForSeconds(1f);

        Vector3 teleport_position = teleport_transform.position;
        Quaternion teleport_rotation = teleport_transform.rotation;
        other.transform.position = teleport_position;
        other.transform.rotation = teleport_rotation;
        Physics.SyncTransforms();

        yield return new WaitForSeconds(1f);

        fadeManager.PlayFadeIn();
    }
}
