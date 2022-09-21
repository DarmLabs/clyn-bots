using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MolinoAnim : MonoBehaviour
{
    void LateUpdate()
    {
        transform.Rotate(-Vector3.up, Space.Self);
    }
}
