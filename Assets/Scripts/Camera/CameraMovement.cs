using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;
    private Vector3 moveVec;
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position -target.position;
     }

    // Update is called once per frame
    void Update()
    {
        moveVec = target.position + startPosition;
        moveVec.x = 0f;
        moveVec.y = startPosition.y;
        transform.position = moveVec;
    }
}
