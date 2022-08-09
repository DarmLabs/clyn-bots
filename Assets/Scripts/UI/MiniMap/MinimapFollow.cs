using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    void LateUpdate()
    {
        Vector3 newPosition = target.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition + offset;
    }
}
