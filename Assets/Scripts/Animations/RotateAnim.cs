using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAnim : MonoBehaviour
{
    [SerializeField] Vector3 direction;
    void LateUpdate()
    {
        transform.Rotate(direction, Space.Self);
    }
}
