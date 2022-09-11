using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviromentChanger : MonoBehaviour
{
    [SerializeField] GameObject[] firstStageTrees;
    [SerializeField] GameObject firstStageAmbience;
    [SerializeField] GameObject[] lastStageTrees;
    [SerializeField] GameObject lastStageAmbience;
    [SerializeField] Material terrainMaterial;
    [SerializeField] GameObject[] trashSpawners;
    bool z1Status, z2Status, z3Status, z4Status;
    bool allCleared;
    void Start()
    {
        trashSpawners = GameObject.FindGameObjectsWithTag("Zone");
        if (allCleared)
        {
            firstStageAmbience.SetActive(true);
            lastStageAmbience.SetActive(false);
            for (int i = 0; i < 4; i++)
            {
                firstStageTrees[i].SetActive(false);
                lastStageTrees[i].SetActive(true);
            }
        }
        else
        {
            if (z1Status)
            {
                CheckTreesStatus(0, false);
            }
            if (z2Status)
            {
                CheckTreesStatus(1, false);
            }
            if (z3Status)
            {
                CheckTreesStatus(2, false);
            }
            if (z4Status)
            {
                CheckTreesStatus(3, false);
            }
        }
    }
    public void CheckTreesStatus(int id, bool passStatus)
    {
        firstStageTrees[id].SetActive(false);
        lastStageTrees[id].SetActive(true);
        if (passStatus)
        {
            SetZoneStatus(id);
            CheckWholeStatus();
        }
    }
    public void SetZoneStatus(int id)
    {
        switch (id)
        {
            case 0:
                z1Status = true;
                break;
            case 1:
                z2Status = true;
                break;
            case 2:
                z3Status = true;
                break;
            case 3:
                z4Status = true;
                break;
        }
    }
    public void CheckWholeStatus()
    {
        if (z1Status && z2Status && z3Status && z4Status & !allCleared)
        {
            firstStageAmbience.SetActive(false);
            lastStageAmbience.SetActive(true);
            allCleared = true;
        }
    }
    public void CheckZones(int code)
    {
        if (trashSpawners[code].transform.childCount == 0)
        {
            CheckTreesStatus(code, true);
        }
    }
}
