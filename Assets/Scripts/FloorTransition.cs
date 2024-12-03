using System.Collections;
using UnityEngine;

public class FloorTransition : MonoBehaviour
{
    public FadeManager fadeManager;
    public Transform teleport_transform;
    public HouseMusic houseMusic; // Reference to the HouseMusic script
    public bool isBryanRoom = true; // Set this to true for Bryan's room transition

    private bool hasTriggered = false; // Prevent multiple triggers

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasTriggered)
        {
            Debug.Log("OnTriggerEnter: Player entered trigger. isBryanRoom: " + isBryanRoom);
            hasTriggered = true; // Prevent further triggers
            StartCoroutine(TeleportWithFade(other));
        }
    }

    private IEnumerator TeleportWithFade(Collider other)
    {
        Debug.Log("TeleportWithFade started. isBryanRoom: " + isBryanRoom);
        fadeManager.PlayFadeOut();

        yield return new WaitForSeconds(1f);

        Vector3 teleport_position = teleport_transform.position;
        Quaternion teleport_rotation = teleport_transform.rotation;
        other.transform.position = teleport_position;
        other.transform.rotation = teleport_rotation;
        Physics.SyncTransforms();

        // Notify HouseMusic about the music change
        if (houseMusic != null)
        {
            if (isBryanRoom)
            {
                Debug.Log("Entering Bryan's Room");
                houseMusic.OnEnterBryansRoom(); // Use the new method
            }
            else
            {
                Debug.Log("Exiting Bryan's Room");
                houseMusic.OnExitBryansRoom(); // Use the new method
            }
        }
        else
        {
            Debug.LogWarning("HouseMusic script is not assigned.");
        }

        yield return new WaitForSeconds(1f);

        fadeManager.PlayFadeIn();

        // Allow triggering again after fade completes
        yield return new WaitForSeconds(1f);
        hasTriggered = false;
    }
}
