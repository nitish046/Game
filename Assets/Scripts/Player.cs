using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Player : MonoBehaviour
{

  private Transform cameraTransform;
  [SerializeField] private float speed = 7f;
  [SerializeField] private float cameraDistance = 5f;
  [SerializeField] private float rotationSpeed = 50f;
  [SerializeField] float jumpHeight = 0.5f;
  [SerializeField] float gravityScale = 0f;
  [SerializeField] private GameInput gameInput;
  public RaycastHit groundCollider;
  public RaycastHit objectCollider;
  float velocity;
  CharacterController controller;

  public AudioClip laugh;
  private AudioSource audioSource;

  private void Awake()
  {
    cameraTransform = Camera.main.transform;
    audioSource = GetComponent<AudioSource>();
    
    controller = GetComponent<CharacterController>();
  }

  private void FixedUpdate()
  {
    playerMovement();
    playerJump();
  }

  private void playerMovement()
  {
    Vector2 movementInput = gameInput.getMovementInputVectorNormalized();

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
    if (movementInput.y == 0)
      moveDistance = 0;
    
    if (movementInput.y < 0)
      movementInput.x *= -1;

    // if (movementInput.y == 0 && movementInput.x != 0)
    // {
    //   movementDirection = transform.rotation * (new Vector3(0, 0, 1));
    //   moveDistance = 0.05f;
    // }
    
    var c = GetComponentInChildren<CapsuleCollider>();
    var adjHeight = c.height - 2 * c.radius;
    
    Vector3[] capPoints = {c.center + new Vector3(0, adjHeight/2, 0), c.center - new Vector3(0, adjHeight/2, 0)};
    
    transform.TransformPoints(capPoints);
    
    //Debug.Log((c.center + new Vector3(0, adjHeight/2, 0)));
    
    
    Vector3 trueMovementDirection = movementDirection + new Vector3(0, velocity, 0);
    float trueMoveDistance = Mathf.Sqrt(Mathf.Pow(moveDistance, 2) + Mathf.Pow(velocity, 2));
    trueMovementDirection.Normalize();
    
    //Debug.Log(movementDirection);
    
    bool racconColliding = Physics.CapsuleCast
    (capPoints[0], 
    capPoints[1],
    c.radius,
    (trueMovementDirection),
    out objectCollider,
    trueMoveDistance);
    if (objectCollider.collider != null)
    {
      if (objectCollider.collider.CompareTag("Henry") || objectCollider.collider.CompareTag("Terrain"))
      {
        racconColliding = false;
      }
    }
    
    controller.Move(movementDirection * moveDistance);
    transform.rotation *= Quaternion.Euler(0, movementInput.x * rotationSpeed * Time.fixedDeltaTime, 0);
    
    Debug.Log(groundcheck());
    
    /*Debug.Log(racconColliding);


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
      //else if (movementInput.y == 0)
      //{
        //transform.position += transform.rotation * (new Vector3(movementInput.x, 0, 0)) * moveDistance;
      //}
      else
      {
        transform.position += movementDirection * moveDistance;
      }
    }
    else
    {
      transform.rotation *= Quaternion.Euler(0, movementInput.x * rotationSpeed * Time.fixedDeltaTime, 0);
    }*/

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
      //Vector3 closestPoint = groundCollider.collider.ClosestPointOnBounds(transform.position);
      //Vector3 snappedPosition = new Vector3(transform.position.x, closestPoint.y + offset, transform.position.z);

      //transform.position = snappedPosition;
    }

    if (jump && grounded)
    {
      velocity = Mathf.Sqrt(jumpHeight * -3f * (Physics.gravity.y * gravityScale));
    }
    controller.Move(new Vector3(0, velocity, 0) * Time.deltaTime);


  }

  public bool groundcheck()
  {
    Debug.DrawRay(transform.position + (Vector3.up * .25f), Vector3.down, UnityEngine.Color.green);
    if (Physics.Raycast(transform.position + (Vector3.up * .25f), Vector3.down, out groundCollider, .27f) && !(groundCollider.collider.CompareTag("Henry")))
    {
      return true;
    }
    else
    {
      //return controller.isGrounded;
      return false;
    }

  }

  public void PlayPickupSound()
  {
    if (laugh != null && audioSource != null)
    {
      audioSource.PlayOneShot(laugh);
    }

  }

}
