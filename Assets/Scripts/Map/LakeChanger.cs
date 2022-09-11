using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LakeChanger : MonoBehaviour
{
    [SerializeField] Material cleanWater;
    MainMission mainMission;
    void OnEnable()
    {
        mainMission = GameObject.FindObjectOfType<MainMission>();
        if (mainMission.lakesCleaned == 2)
        {
            gameObject.GetComponent<MeshRenderer>().material = cleanWater;
        }
    }
}
