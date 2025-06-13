using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    private Animator animationController;
    private Transform playerTransform;
    private Rigidbody playerRigidbody;

    private float runSpeed = 0.2f;
    private float laneChangeSpeed = 5f;
    private float safeDistance = 5f;
    private float jumpForce = 6f;

    private bool isJumping = false;

    void Start()
    {
        animationController = GetComponent<Animator>();
        playerTransform = GetComponent<Transform>();
        playerRigidbody = GetComponent<Rigidbody>();

        animationController.SetBool("Breath", false);
        animationController.SetBool("Run", true);
    }

    void FixedUpdate()
    {
        // Reset jump state when grounded (you may want to implement a ground check)
        if (playerRigidbody.velocity.y <= 0)
        
        {
            isJumping = false;
            animationController.SetBool("Run", true);
            animationController.SetBool("Jump", false);
        }

        float move = Input.GetAxis("Horizontal");

        if(playerTransform.position.x > -safeDistance && playerTransform.position.x < safeDistance)
        {
            playerTransform.position += new Vector3(move * laneChangeSpeed, 0, 1) * runSpeed;
        }
        else
        {
            playerTransform.position += new Vector3(0, 0, 1) * runSpeed;
        }


        // Jumping
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            isJumping = true;
            animationController.SetBool("Run", false);
            animationController.SetBool("Jump", true);
            
            playerRigidbody.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
        }

    }
}
