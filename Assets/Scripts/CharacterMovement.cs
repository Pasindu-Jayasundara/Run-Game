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

        if (move == 0)
        {// Always face forward when no horizontal input
            playerTransform.forward = Vector3.forward;
        }
        else
        {// look changing side
            playerTransform.forward = new Vector3(move*runSpeed, 0, 1).normalized;
        }

        playerTransform.position += new Vector3(move * laneChangeSpeed, 0, 1) * runSpeed;
    }
}
