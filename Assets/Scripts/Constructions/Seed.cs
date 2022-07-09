using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Seed : MonoBehaviour, ISaveable
{
    ConstructibleObj constructibleObj;
    public GameObject []plantacion = new GameObject[3];
    public GameObject semilla;
    public GameObject target;
    GameObject building;
    public int index;
    public string state = "Construir";
    void Awake()
    {
        constructibleObj = GetComponent<ConstructibleObj>();
    }

    public void ChooseSeed(int index){
        building = plantacion[index];
        this.index = index;
        GetComponent<ConstructibleObj>().seeded = true;
    }
    public void PlaceSeed(){
        semilla.transform.position = target.transform.position;
        semilla.GetComponent<SavePosition>().PositionUpdated();
        target.GetComponent<SavePosition>().PositionUpdated();
        state = "Mejorar";
    }
    public void GrowSeed(){
        building.transform.position = semilla.transform.position;
        semilla.transform.position = new Vector3(semilla.transform.position.x, semilla.transform.position.y - 100, semilla.transform.position.z);
        building.GetComponent<SavePosition>().PositionUpdated();
        semilla.GetComponent<SavePosition>().PositionUpdated();
        gameObject.tag = "Untagged";
    }
    public object SaveState(){
        return new SaveData(){
            index = this.index,
            state = this.state
        };
    }
    //LoadState carga los datos desde el guardado y los asigna a los accesibles, segui el formato de las variables ya puestas
    public void LoadState(object state){
        var saveData = (SaveData)state;
        index = saveData.index;
        state = saveData.state;
        ChooseSeed(index);
    }
    [Serializable]
    private struct SaveData{
        public int index;
        public string state;
    }
}

