using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GlobalVariables : MonoBehaviour, ISaveable
{
    //Agregar variables accesibles aqui, como las de abajo, estas variables son las que vas a usar para sacar info
    public int noRecTrash, organicTrash, recTrash, cardDistribution;
    void Awake()
    {
        DontDestroyOnLoad(this);
    }
    //SaveState es el pase de las accesibles a un estado de "dato de guardado" para luego guardarlo, agrega las variables siguiendo las de abajo (no olvides las comas!!)
    public object SaveState(){
        return new SaveData(){
            noRecTrash = this.noRecTrash,
            organicTrash = this.organicTrash,
            recTrash = this.recTrash,
            cardDistribution = this.cardDistribution
        };
    }
    //LoadState carga los datos desde el guardado y los asigna a los accesibles, segui el formato de las variables ya puestas
    public void LoadState(object state){
        var saveData = (SaveData)state;
        noRecTrash = saveData.noRecTrash;
        organicTrash = saveData.organicTrash;
        recTrash = saveData.recTrash;
        cardDistribution = saveData.cardDistribution;
    }
    [Serializable]
    private struct SaveData{
        //Esto ni me acuerdo para que es pero, asignale las variables como si las estuvieras declarando normalmente, no hay mucha magia 
        public int noRecTrash, organicTrash, recTrash, cardDistribution;
    }
}
