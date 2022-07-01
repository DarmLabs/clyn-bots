using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class SavePosition : MonoBehaviour, ISaveable
{
    public float xPos, yPos, zPos;
    void Start()
    {
        PositionUpdated();
    }
    void SetPosition(){
        transform.position = new Vector3(xPos, yPos, zPos);
    }
    public void PositionUpdated(){
        xPos = transform.position.x;
        yPos = transform.position.y;
        zPos = transform.position.z;
    }
    public object SaveState(){
        return new SaveData(){
            xPos = this.xPos,
            yPos = this.yPos,
            zPos = this.zPos
        };
    }
    //LoadState carga los datos desde el guardado y los asigna a los accesibles, segui el formato de las variables ya puestas
    public void LoadState(object state){
        var saveData = (SaveData)state;
        xPos = saveData.xPos;
        yPos = saveData.yPos;
        zPos = saveData.zPos;
        SetPosition();
    }
    [Serializable]
    private struct SaveData{
        public float xPos, yPos, zPos;
    }
}
