
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Unity.Mathematics;

public class CameraFollow : MonoBehaviour
{
    // public float FollowSpeed = 2f;
    public Transform target;

    public float smoothTime = 0.25f;
    public Vector3 offset = new Vector3(0, 2, -10);

    Vector3 currentVelocity;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Vector3 newPos = new Vector3(target.position.x, target.position.y, -10f);
        // transform.position = Vector3.Slerp(transform.position, newPos, FollowSpeed * Time.deltaTime);
    }

    private void LateUpdate()
    {
        transform.position = Vector3.SmoothDamp(
             transform.position,
             target.position + offset,
             ref currentVelocity,
              smoothTime);
        
    }
}
