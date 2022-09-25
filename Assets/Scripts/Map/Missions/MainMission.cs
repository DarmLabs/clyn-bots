using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class MainMission : MonoBehaviour
{
    public int trashRecolected, constructionsFinished, maintainancePlayed, lakesCleaned, cropsGrew;
    public int maxTrash = 500, maxConstructions = 7, maxMaintainance = 5, maxLakes = 2, maxCrops = 2;
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    public object SaveState()
    {
        return new SaveData()
        {
            trashRecolected = this.trashRecolected,
            constructionsFinished = this.constructionsFinished,
            maintainancePlayed = this.maintainancePlayed,
            lakesCleaned = this.lakesCleaned,
            cropsGrew = this.cropsGrew
        };
    }
    public void LoadState(object state)
    {
        var saveData = (SaveData)state;
        trashRecolected = saveData.trashRecolected;
        constructionsFinished = saveData.constructionsFinished;
        maintainancePlayed = saveData.maintainancePlayed;
        lakesCleaned = saveData.lakesCleaned;
        cropsGrew = saveData.cropsGrew;
    }
    [Serializable]
    private struct SaveData
    {
        public int trashRecolected, constructionsFinished, maintainancePlayed, lakesCleaned, cropsGrew;
    }
}
