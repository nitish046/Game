using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HenryController : MonoBehaviour
{

	[SerializeField] Transform path;
	[SerializeField] float movement_speed = 5f;
	[SerializeField] float rotation_speed = 90f;


	[SerializeField] float waypoint_size = .4f;
	[SerializeField] float waypoint_wait_time = 2f;
	[SerializeField] float distance;
	[SerializeField] GameObject player;
	[SerializeField] SkinnedMeshRenderer skinnedMeshRenderer;



	public Button restart_button;
	public Button quit_button;
	public TMP_Text lose_text;
	public Material MainColor, FreezeColor;
	public float duration = 5f;
	public bool allow;
	public AudioSource splash;
	[SerializeField] private HideOnCollide collision_occur;

	public GameObject loseScreen;
	public GameObject mainScreen;

	private Animator henry_animator;

	private void Start()
	{
		collision_occur.onRaccoonFirstTimeOnTrash += collisionOccur_onRaccoonFirstTimeOnTrash;
		allow = true;

		henry_animator = transform.GetChild(0).GetComponent<Animator>();
	}


	private void collisionOccur_onRaccoonFirstTimeOnTrash(object sender, System.EventArgs e)
	{
		transform.position = new Vector3(transform.position.x, 0, transform.position.z);
		StartCoroutine(patrol(getWaypointArray()));
		walkingTransition(true);
	}

	private void Update()
	{
		distance = Vector3.Distance(transform.position, player.transform.position);
		if (distance <= 4 && allow)	// Loss condition is now active
		{
			// lose_text.text = "You were Caught! You Lose!";
			// lose_text.gameObject.SetActive(true);
			// restart_button.gameObject.SetActive(true);
			// quit_button.gameObject.SetActive(true);
			// mainScreen.SetActive(false);
			// loseScreen.SetActive(true);
		}
	}

	public void Freeze(float freezeDuration, bool isTrapFreeze)
	{
		duration = freezeDuration; // Set the freeze duration based on the trap
		skinnedMeshRenderer.material = FreezeColor; // Change to FreezeColor
		allow = false; // Stop movement

		if (splash != null && splash.clip != null)
		{
			splash.Play();
		}
		else
		{
			//Debug.LogWarning("Splash AudioSource or AudioClip is not assigned.");
		}

		// If it's a trap freeze, rotate Henry to make it look like he fell down and pause animation
		if (isTrapFreeze)
		{
			henry_animator.enabled = false; // Pause all animations
			transform.rotation = Quaternion.Euler(90f, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z); // Rotate Henry to appear as if he has fallen down
																																																											//Debug.Log("Henry has been frozen and fallen to the ground.");
		}
		else
		{
			//Debug.Log("Henry has been frozen by another method (e.g., tomato).");
		}

		StartCoroutine(delay(isTrapFreeze)); // Pass the freeze type to the delay
	}

	IEnumerator delay(bool isTrapFreeze)
	{
		yield return new WaitForSeconds(duration);

		skinnedMeshRenderer.material = MainColor; // Reset color
		allow = true;

		if (isTrapFreeze)
		{
			henry_animator.enabled = true; // Resume animations
			transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z); // Reset rotation to stand Henry back up
																																																										 //Debug.Log("Henry has unfrozen and is standing up.");
		}

		//Debug.Log("Henry has unfrozen.");
	}

	IEnumerator patrol(Vector3[] waypoints)
	{
		int waypoint_index = 0;
		Vector3 waypoint_target = waypoints[waypoint_index];

		while (true)
		{
			if (allow)
			{
				transform.position = Vector3.MoveTowards(transform.position, waypoint_target, movement_speed * Time.deltaTime);
				transform.LookAt(waypoint_target);

				if (transform.position == waypoint_target)
				{
					walkingTransition(false);
					waypoint_index = (waypoint_index + 1) % waypoints.Length;
					waypoint_target = waypoints[waypoint_index];
					yield return new WaitForSeconds(waypoint_wait_time);
					yield return StartCoroutine(turnTowardsPosition(waypoint_target));
					walkingTransition(true);
				}
			}
			yield return null;
		}
	}




	IEnumerator turnTowardsPosition(Vector3 rotation_target)
	{
		Vector3 direction = (rotation_target - transform.position).normalized;
		float target_angle = 90 - Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;

		while (Mathf.DeltaAngle(transform.eulerAngles.y, target_angle) > Mathf.Abs(0.05f))
		{
			float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.y, target_angle, rotation_speed * Time.deltaTime);
			transform.eulerAngles = Vector3.up * angle;
			yield return null;
		}
	}

	private Vector3[] getWaypointArray()
	{
		Vector3[] waypoint_array = new Vector3[path.childCount];
		for (int i = 0; i < waypoint_array.Length; i++)
		{
			waypoint_array[i] = path.GetChild(i).position;
		}

		return waypoint_array;
	}

	private void walkingTransition(bool walking)
	{
		if (walking)
		{
			henry_animator.SetBool("isWalking", true);
		}
		else
		{
			henry_animator.SetBool("isWalking", false);
		}
	}

	private void OnDrawGizmos()
	{
		Vector3 start_waypoint_position = path.GetChild(0).position;
		Vector3 previous_waypoint_position = start_waypoint_position;

		foreach (Transform waypoint in path)
		{
			Gizmos.color = new Color(238f / 255, 130f / 255, 238f / 255, 255f / 255);
			Gizmos.DrawSphere(waypoint.position, waypoint_size);

			Gizmos.color = Color.white;
			Gizmos.DrawLine(previous_waypoint_position, waypoint.position);

			previous_waypoint_position = waypoint.position;
		}
		Gizmos.DrawLine(previous_waypoint_position, start_waypoint_position);
	}
}

