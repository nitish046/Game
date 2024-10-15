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
  [SerializeField] float jumpHeight = 0.5f;
  [SerializeField] float gravityScale = 0f;
  [SerializeField] private GameInput gameInput;
  public RaycastHit groundCollider;
  public RaycastHit objectCollider;
  float velocity;

  public AudioClip laugh;
  private AudioSource audioSource;

    [Header("Trap Placement Settings")]
    public GameObject trapPrefab;
    public int maxTraps = 1000;
    private int currentTrapCount;

    private bool isSettingTrap = false;
    private bool canMove = true;

    private void Awake()
    {
        cameraTransform = Camera.main.transform;
        audioSource = GetComponent<AudioSource>();
        currentTrapCount = maxTraps;
    }

    private void FixedUpdate()
    {
    playerMovement();
    playerJump();
    }

    private void Update()
    {
        // Check for trap placement input
        if (Input.GetKeyDown(KeyCode.T) && !isSettingTrap)
        {
            StartCoroutine(PlaceTrap());
        }
    }

    private void playerMovement()
  {

    if (!canMove)
    {
        return; // Prevent movement during trap setup
    }
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
    
    bool racconColliding = Physics.CapsuleCast
    (capPoints[0], 
    capPoints[1],
    c.radius,
    (movementDirection),
    out objectCollider,
    moveDistance);
    if (objectCollider.collider != null)
    {
      if (objectCollider.collider.CompareTag("Henry"))
      {
        racconColliding = false;
      }
    }


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
    else
    {
      transform.rotation *= Quaternion.Euler(0, movementInput.x * rotationSpeed * Time.fixedDeltaTime, 0);
    }

    cameraTransform.position = transform.position + transform.rotation * new Vector3(0, 0.75f, -(cameraDistance + 1));
    cameraTransform.rotation = Quaternion.LookRotation(transform.position - cameraTransform.position) * Quaternion.Euler(-5, 0, 0);
    cameraTransform.position += new Vector3(0, 1, 0);
  }

  private void playerJump()
  {
    if (!canMove)
    {
        return; // Prevent jumping during trap setup
    }
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
      velocity = Mathf.Sqrt(jumpHeight * -3f * (Physics.gravity.y * gravityScale));
    }
    transform.Translate(new Vector3(0, velocity, 0) * Time.deltaTime);


  }

  public bool groundcheck()
  {
    UnityEngine.Debug.DrawRay(transform.position + (Vector3.up * .25f), Vector3.down, UnityEngine.Color.green);
    if (Physics.Raycast(transform.position + (Vector3.up * .25f), Vector3.down, out groundCollider, .35f) && !(groundCollider.collider.CompareTag("Henry")))
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
    if (laugh != null && audioSource != null)
    {
      audioSource.PlayOneShot(laugh);
    }

  }
    IEnumerator PlaceTrap()
    {
        if (currentTrapCount > 0)
        {
            isSettingTrap = true;
            canMove = false; // Disable movement

            // Optional: Play an animation or show a visual cue
            UnityEngine.Debug.Log("Setting up trap...");

            yield return new WaitForSeconds(2f); // Wait for 2 seconds

            Vector3 spawnPosition = transform.position;
            Instantiate(trapPrefab, spawnPosition, Quaternion.identity);
            currentTrapCount--;

            canMove = true; // Re-enable movement
            isSettingTrap = false;

            // Optional: Provide feedback to the player
            UnityEngine.Debug.Log("Trap placed. Traps remaining: " + currentTrapCount);
        }
        else
        {
            UnityEngine.Debug.Log("No traps left!");
        }
    }

}
