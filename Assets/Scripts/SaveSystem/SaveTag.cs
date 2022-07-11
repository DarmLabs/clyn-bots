using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class SaveTag : MonoBehaviour, ISaveable
{
    public string _tag;
    void Start()
    {
        UpdateTag();
    }
    void SetTag(){
        gameObject.tag = _tag;
    }
    public void UpdateTag(){
        _tag = gameObject.tag;
    }
    public object SaveState(){
        return new SaveData(){
            _tag = this._tag
        };
    }
    //LoadState carga los datos desde el guardado y los asigna a los accesibles, segui el formato de las variables ya puestas
    public void LoadState(object state){
        var saveData = (SaveData)state;
        _tag = saveData._tag;
        SetTag();
    }
    [Serializable]
    private struct SaveData{
        public string _tag;
    }
}

