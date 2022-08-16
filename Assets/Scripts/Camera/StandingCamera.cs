using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandingCamera : MonoBehaviour
{
    [SerializeField] Vector3 cameraInsidePos;
    [SerializeField] Transform target;
    void OnEnable()
    {
        transform.position = cameraInsidePos;
    }
    void FixedUpdate()
    {
        transform.LookAt(target);
    }
}
