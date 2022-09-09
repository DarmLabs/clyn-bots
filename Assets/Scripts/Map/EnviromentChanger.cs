using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviromentChanger : MonoBehaviour
{
    [SerializeField] GameObject[] firstStageTrees;
    [SerializeField] GameObject[] lastStageTrees;
    [SerializeField] Material terrainMaterial;

    public void CheckTreesStatus(int id)
    {
        firstStageTrees[id].SetActive(false);
        lastStageTrees[id].SetActive(true);
    }
}
