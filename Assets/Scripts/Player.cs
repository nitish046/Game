using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Player : MonoBehaviour
{

    private Transform cameraTransform;
    [SerializeField] private float speed = 7f;
    [SerializeField] private float cameraDistance = 5f;
    [SerializeField] private float rotationSpeed = 50f;
    [SerializeField] private GameInput gameInput;


    private void Awake()
    {
        cameraTransform = Camera.main.transform;
    }
    private void Update()
    {
        playerMovement();

    }

    private void playerMovement()
    {
        Vector2 movementInput = gameInput.getMovementInputVectorNormalized();
        Vector3 movementDirection = transform.rotation * (new Vector3(0, 0, movementInput.y));

        float raccoonRadius = 0.1849963f;
        float raccoonLength = .54f;
        float moveDistance = Time.deltaTime * speed;

        bool racconColliding = Physics.CapsuleCast(transform.position, transform.position + (transform.rotation * Vector3.forward) * raccoonLength, raccoonRadius, (movementDirection), moveDistance);

        if (!racconColliding)
        {
            transform.position += movementDirection * moveDistance;
            transform.rotation *= Quaternion.Euler(0, movementInput.x * rotationSpeed * Time.fixedDeltaTime, 0);
        }

        cameraTransform.position = transform.position + transform.rotation * new Vector3(0, 1, -cameraDistance);
        cameraTransform.rotation = Quaternion.LookRotation(transform.position - cameraTransform.position);
    }
}
