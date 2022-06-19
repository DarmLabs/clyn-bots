using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GlobalVariables : MonoBehaviour, ISaveable
{
    public int noRecTrash, organicTrash, recTrash;
    void Awake()
    {
        DontDestroyOnLoad(this);
    }
    public object SaveState(){
        return new SaveData(){
            noRecTrash = this.noRecTrash,
            organicTrash = this.organicTrash,
            recTrash = this.recTrash
        };
    }
    public void LoadState(object state){
        var saveData = (SaveData)state;
        noRecTrash = saveData.noRecTrash;
        organicTrash = saveData.organicTrash;
        recTrash = saveData.recTrash;
    }
    [Serializable]
    private struct SaveData{
        public int noRecTrash, organicTrash, recTrash;
    }
}
