using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    public Transform target;

    float smoothSpeed = 10f;
    public Vector3 offset;
    Vector3 targetOffset;
    void Start()
    {
        targetOffset = new Vector3(0, 1.3f,0);
    }
    void FixedUpdate()
    {
        Vector3 desiredPosition = (target.position + targetOffset) + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
    }
}
