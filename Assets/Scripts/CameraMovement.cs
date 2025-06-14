using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public Transform playerObj;
    private float smoothness = 5f;

    private Vector3 offset;

    void Start()
    {
        offset = transform.position - playerObj.position;
    }

    void Update()
    {
        Vector3 newCameraPosition = playerObj.position + offset;
        transform.position = Vector3.Lerp(transform.position, newCameraPosition, smoothness * Time.deltaTime);
    }
}
