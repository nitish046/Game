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
    [SerializeField] float jumpHeigh = 5f;
    [SerializeField] float gravityScale = 0f;
    [SerializeField] private GameInput gameInput;
    public RaycastHit groundCollider;
    float velocity;


    private void Awake()
    {
        cameraTransform = Camera.main.transform;

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

            bool racconColliding = Physics.CapsuleCast(transform.position, transform.position + (transform.rotation * Vector3.forward) * raccoonLength, raccoonRadius, (movementDirection), moveDistance);
            
            //replace true with !racconColliding
            if (!racconColliding)
            {
                if(groundcheck())
                {
                    if(movementInput.y == 0)
                    {
                        movementInput.x = 0;
                    }
                    transform.position += movementDirection * moveDistance;
                    transform.rotation *= Quaternion.Euler(0, movementInput.x * rotationSpeed * Time.fixedDeltaTime, 0);
                }
                else if(movementInput.x == 0 || movementInput.y == 0)
                {
                    transform.position += transform.rotation * (new Vector3(movementInput.x, 0, movementInput.y)) * moveDistance;
                }
            
            }

            cameraTransform.position = transform.position + transform.rotation * new Vector3(0, 1, -cameraDistance);
            cameraTransform.rotation = Quaternion.LookRotation(transform.position - cameraTransform.position);
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

        if(jump && grounded)
        {
            velocity = Mathf.Sqrt(jumpHeigh * -3f * (Physics.gravity.y * gravityScale));
        }
        transform.Translate(new Vector3(0, velocity, 0) * Time.deltaTime);

        
    }

    public bool groundcheck()
    {
        Debug.DrawRay(transform.position + (Vector3.up * .25f), Vector3.down, UnityEngine.Color.green);
        if (Physics.Raycast(transform.position + (Vector3.up * .25f), Vector3.down, out groundCollider, .35f))
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }

}
