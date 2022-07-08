using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera_UI : MonoBehaviour
{
    public Vector3 offSet;
    void LateUpdate()
    {
        transform.position = Camera.main.transform.position - offSet;
        transform.LookAt(Camera.main.transform);
        transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);
    }
}
