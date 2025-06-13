using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    private Rigidbody playerRigidbody;
    private Animator animationController;
    private Transform playerTransform;

    private float runSpeed = 0.2f;
    private float laneChangeSpeed = 5f;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        animationController = GetComponent<Animator>();
        playerTransform = GetComponent<Transform>();
    }

    void FixedUpdate()
    {
        float move = Input.GetAxis("Horizontal");

        if(playerTransform.position.x > -5f && playerTransform.position.x < 5f)
        {
            playerTransform.position += new Vector3(move * laneChangeSpeed, 0, 1) * runSpeed;
        }
        else
        {
            playerTransform.position += new Vector3(0, 0, 1) * runSpeed;
        }
    }
}
