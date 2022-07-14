using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera_UI : MonoBehaviour
{
    public GameObject targetCamera;
    void OnEnable()
    {
        transform.rotation = Quaternion.LookRotation(targetCamera.transform.forward);
        transform.position = targetCamera.transform.position + (-targetCamera.transform.forward *10);
    }
}
