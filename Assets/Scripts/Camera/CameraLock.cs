using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLock : MonoBehaviour
{
    [SerializeField] Cinemachine.CinemachineFreeLook freeLookCamera;
    GlobalVariables gv;
    void Awake()
    {
        gv = GameObject.FindObjectOfType<GlobalVariables>();
    }
    void FixedUpdate()
    {
        CheckCameraBlocked();
    }
    void CheckCameraBlocked()
    {
        if (gv.isCameraBlocked && freeLookCamera.m_XAxis.m_MaxSpeed != 0)
        {
            CameraBlock();
        }
        else if (!gv.isCameraBlocked && freeLookCamera.m_XAxis.m_MaxSpeed != 275)
        {
            CameraUnblock();
        }
    }
    public void CameraBlock()
    {
        freeLookCamera.m_XAxis.m_MaxSpeed = 0;
        freeLookCamera.m_YAxis.m_MaxSpeed = 0;
    }
    public void CameraUnblock()
    {
        freeLookCamera.m_XAxis.m_MaxSpeed = 275;
        freeLookCamera.m_YAxis.m_MaxSpeed = 2;
    }
}
