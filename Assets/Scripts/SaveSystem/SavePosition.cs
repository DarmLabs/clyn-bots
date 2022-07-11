using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class SavePosition : MonoBehaviour, ISaveable
{
    public float xPos, yPos, zPos, xRot, yRot, zRot;
    void Start()
    {
        PositionUpdated();
    }
    void SetPosition(){
        transform.position = new Vector3(xPos, yPos, zPos);
    }
    void SetRotation(){
        transform.Rotate(xRot, yRot, zRot, Space.World);
    }
    public void PositionUpdated(){
        xPos = transform.position.x;
        yPos = transform.position.y;
        zPos = transform.position.z;
    }
    public void RotationUpdated(){
        xRot = transform.localRotation.eulerAngles.x;
        yRot = transform.localRotation.eulerAngles.y;
        zRot= transform.localRotation.eulerAngles.z;
    }

    public object SaveState(){
        return new SaveData(){
            xPos = this.xPos,
            yPos = this.yPos,
            zPos = this.zPos,
            xRot = this.xRot,
            yRot = this.yRot,
            zRot = this.zRot
        };
    }
    //LoadState carga los datos desde el guardado y los asigna a los accesibles, segui el formato de las variables ya puestas
    public void LoadState(object state){
        var saveData = (SaveData)state;
        xPos = saveData.xPos;
        yPos = saveData.yPos;
        zPos = saveData.zPos;
        xRot = saveData.xRot;
        yRot = saveData.yRot;
        zRot = saveData.zRot;
        SetPosition();
        if(gameObject.tag == "Player"){
            SetRotation();
        }
    }
    [Serializable]
    private struct SaveData{
        public float xPos, yPos, zPos, xRot, yRot, zRot;
    }
}
