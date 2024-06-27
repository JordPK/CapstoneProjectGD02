using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FPSPlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] float moveSpeed = 5f;
   
   
    [SerializeField] float sprintSpeedMultiplier;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] float gravityMultiplier;
   [SerializeField] CharacterController characterController;
    [SerializeField] Camera fpsCam;

    CapsuleCollider capsuleCollider;
    NavMeshAgent agent;
    

    [Header("Physics Settings")]
    public float gravity = -9.81f;

    private Vector3 velocity;
    private bool isGrounded;

    private void Start()
    {
       capsuleCollider = GetComponent<CapsuleCollider>();
        agent = GetComponent<NavMeshAgent>();
        fpsCam = FindAnyObjectByType<Camera>();
        gravity *= gravityMultiplier;
        
    }

    void Update()
    {
        if (CameraManager.Instance.isFirstPerson)
        {
            agent.enabled = false;
            capsuleCollider.enabled = false;
            characterController.enabled = true;
            Move();
        }
        else
        {
            agent.enabled = true;
            characterController.enabled = true;
            capsuleCollider.enabled = false;
        }
        
    }

    private void Move()
    {
        // Check if the player is grounded
        isGrounded = characterController.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Small value to keep the player grounded
        }

        // Get input for movement
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Calculate movement direction
        Vector3 move = transform.right * horizontal + transform.forward * vertical;
        characterController.Move(move * moveSpeed * Time.deltaTime);

        // Jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }
}