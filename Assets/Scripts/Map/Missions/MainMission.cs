using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class MainMission : MonoBehaviour
{
    public int trashRecolected, constructionsFinished, compostPlayed, lakesCleaned, cropsGrew;
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
            compostPlayed = this.compostPlayed,
            lakesCleaned = this.lakesCleaned,
            cropsGrew = this.cropsGrew
        };
    }
    public void LoadState(object state)
    {
        var saveData = (SaveData)state;
        trashRecolected = saveData.trashRecolected;
        constructionsFinished = saveData.constructionsFinished;
        compostPlayed = saveData.compostPlayed;
        lakesCleaned = saveData.lakesCleaned;
        cropsGrew = saveData.cropsGrew;
    }
    [Serializable]
    private struct SaveData
    {
        public int trashRecolected, constructionsFinished, compostPlayed, lakesCleaned, cropsGrew;
    }
}
