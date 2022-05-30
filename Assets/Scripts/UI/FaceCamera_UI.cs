using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera_UI : MonoBehaviour
{
    void LateUpdate()
    {
        transform.LookAt(Camera.main.transform);
        transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);
    }
}
