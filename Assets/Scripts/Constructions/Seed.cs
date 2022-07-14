using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Seed : MonoBehaviour, ISaveable
{
    ConstructibleObj constructibleObj;
    public SaveLoadSystem saveSystem;
    public GameObject []plantacion = new GameObject[3];
    public GameObject semilla;
    public GameObject target;
    public GameObject building;
    public int index;
    public string currentState;
    void Awake()
    {
        constructibleObj = GetComponent<ConstructibleObj>();
    }

    public void ChooseSeed(int index){
        building = plantacion[index];
        this.index = index;
        GetComponent<ConstructibleObj>().seeded = true;
        Debug.Log(currentState);
    }
    public void PlaceSeed(){
        semilla.transform.position = target.transform.position;
        semilla.GetComponent<SavePosition>().PositionUpdated();
        target.GetComponent<SavePosition>().PositionUpdated();
        currentState = "Mejorar";
        GetComponent<ConstructibleObj>().ResourcesSubstraction();
        GetComponent<ConstructibleObj>().PlayCinematic(semilla);
        saveSystem.Save();
    }
    public void GrowSeed(){
        building.transform.position = semilla.transform.position;
        semilla.transform.position = new Vector3(semilla.transform.position.x, semilla.transform.position.y - 10000, semilla.transform.position.z);
        building.GetComponent<SavePosition>().PositionUpdated();
        semilla.GetComponent<SavePosition>().PositionUpdated();
        gameObject.tag = "Untagged";
        GetComponent<ConstructibleObj>().ResourcesSubstraction();
        GetComponent<ConstructibleObj>().PlayCinematic(building);
        saveSystem.Save();
    }
    public object SaveState(){
        return new SaveData(){
            index = this.index,
            currentState = this.currentState
            
        };
    }
    //LoadState carga los datos desde el guardado y los asigna a los accesibles, segui el formato de las variables ya puestas
    public void LoadState(object state){
        var saveData = (SaveData)state;
        index = saveData.index;
        currentState = saveData.currentState;
        ChooseSeed(index);
    }
    [Serializable]
    private struct SaveData{
        public int index;
        public string currentState;
    }
}

