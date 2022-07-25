using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera_UI : MonoBehaviour
{
    public GameObject targetCamera;
    public Vector3 offSet;
    void OnEnable()
    {
        transform.LookAt(targetCamera.transform.forward);
        transform.Rotate(180, 0, 270);
        transform.position = targetCamera.transform.position + (targetCamera.transform.forward * 15) + offSet;
    }
}
