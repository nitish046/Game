using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Player : MonoBehaviour
{

  private Transform cameraTransform;
  [SerializeField] private float speed = 7f;
  [SerializeField] private float cameraDistance = 5f;
  [SerializeField] private float rotationSpeed = 50f;
  [SerializeField] float jumpHeigh = 5f;
  [SerializeField] float gravityScale = 0f;
  [SerializeField] private GameInput gameInput;
  public RaycastHit groundCollider;
  float velocity;


    public AudioClip laugh;
    private AudioSource audioSource;

    private void Awake()
  {
    cameraTransform = Camera.main.transform;
    audioSource = GetComponent<AudioSource>();
    if (audioSource == null)
    {
        UnityEngine.Debug.LogError("AudioSource component not found on " + gameObject.name);
    }
    else
    {
        UnityEngine.Debug.Log("AudioSource successfully initialized on " + gameObject.name);
    }

    }

  private void FixedUpdate()
  {
    playerMovement();
    playerJump();
  }

  private void playerMovement()
  {
   
    Vector2 movementInput = gameInput.getMovementInputVectorNormalized(); //Getting error here

    Vector3 movementDirection = transform.rotation * (new Vector3(0, 0, movementInput.y));

    float raccoonRadius = 0.1849963f;
    float raccoonLength = .54f;
    float moveDistance = Time.deltaTime * speed;

    if (gameInput.getSprintInput() && movementInput.y > 0)
    {
      moveDistance *= 3;
      movementInput.x *= 0.5f;
    }

    if (movementInput.y < 0)
      movementInput.x *= -1;

    // if (movementInput.y == 0 && movementInput.x != 0)
    // {
    //   movementDirection = transform.rotation * (new Vector3(0, 0, 1));
    //   moveDistance = 0.05f;
    // }

    bool racconColliding = Physics.CapsuleCast(transform.position, transform.position + (transform.rotation * Vector3.forward) * raccoonLength, raccoonRadius, (movementDirection), moveDistance);

    if (!racconColliding)
    {
      if (groundcheck())
      {
        // if (movementInput.y < 0)
        //   movementInput.x *= -1;
        //movementInput.x *= movementInput.y;

        transform.position += movementDirection * moveDistance;
        transform.rotation *= Quaternion.Euler(0, movementInput.x * rotationSpeed * Time.fixedDeltaTime, 0);
      }
      else if (movementInput.y == 0)
      {
        transform.position += transform.rotation * (new Vector3(movementInput.x, 0, 0)) * moveDistance;
      }
	  else
	  {
		transform.position += transform.rotation * (new Vector3(0, 0, movementInput.y)) * moveDistance;
	  }

    }

    cameraTransform.position = transform.position + transform.rotation * new Vector3(0, 0.75f, -(cameraDistance + 1));
    cameraTransform.rotation = Quaternion.LookRotation(transform.position - cameraTransform.position) * Quaternion.Euler(-5, 0, 0);
    cameraTransform.position += new Vector3(0, 1, 0);
  }

  private void playerJump()
  {
    bool jump = gameInput.getJumpInput();

    bool grounded = groundcheck();

    velocity += Physics.gravity.y * gravityScale * Time.deltaTime;

    if (grounded && velocity < 0f)
    {
      float offset = .01f;
      velocity = 0;
      Vector3 closestPoint = groundCollider.collider.ClosestPointOnBounds(transform.position);
      Vector3 snappedPosition = new Vector3(transform.position.x, closestPoint.y + offset, transform.position.z);

      transform.position = snappedPosition;
    }

    if (jump && grounded)
    {
      velocity = Mathf.Sqrt(jumpHeigh * -3f * (Physics.gravity.y * gravityScale));
    }
    transform.Translate(new Vector3(0, velocity, 0) * Time.deltaTime);


  }

  public bool groundcheck()
  {
        UnityEngine.Debug.DrawRay(transform.position + (Vector3.up * .25f), Vector3.down, UnityEngine.Color.green);
    if (Physics.Raycast(transform.position + (Vector3.up * .25f), Vector3.down, out groundCollider, .35f))
    {
      return true;
    }
    else
    {
      return false;
    }

  }
    public void PlayPickupSound()
    {
        UnityEngine.Debug.Log("PlayPickupSound called");
        if (laugh != null && audioSource != null)
        {
            UnityEngine.Debug.Log("Playing sound");
            audioSource.PlayOneShot(laugh);
        }
        else
        {
            UnityEngine.Debug.Log("Cannot play sound: 'laugh' is " + (laugh == null ? "null" : "not null") +
                      ", 'audioSource' is " + (audioSource == null ? "null" : "not null"));
        }
    }

}
