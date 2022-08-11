using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GlobalVariables : MonoBehaviour, ISaveable
{
    //Agregar variables accesibles aqui, como las de abajo, estas variables son las que vas a usar para sacar info
    //Vidrio, Plástico, Cartón, Metal, Compost
    public int noRecTrash, organicTrash, vidrioTrash, plasticoTrash, cartonTrash, metalTrash;
    public int recTrash, divisionRec;
    public int divisionNoRec, divisionOrganic, divisionVidrio, divisionPlastico, divisionCarton, divisionMetal;
    public int vidrioRefinado, plasticoRefinado, cartonRefinado, metalRefinado, compostRefinado;
    public bool cardDistribution = false;
    void Awake()
    {
        DontDestroyOnLoad(this);
    }
    //SaveState es el pase de las accesibles a un estado de "dato de guardado" para luego guardarlo, agrega las variables siguiendo las de abajo (no olvides las comas!!)
    public object SaveState()
    {
        return new SaveData()
        {
            //Old 
            recTrash = this.recTrash,
            divisionRec = this.divisionRec,
            //New trash
            noRecTrash = this.noRecTrash,
            organicTrash = this.organicTrash,
            vidrioTrash = this.vidrioTrash,
            plasticoTrash = this.plasticoTrash,
            cartonTrash = this.cartonTrash,
            metalTrash = this.metalTrash,
            //?
            cardDistribution = this.cardDistribution,
            //Divisiones
            divisionNoRec = this.divisionNoRec,
            divisionOrganic = this.divisionOrganic,
            divisionVidrio = this.divisionVidrio,
            divisionPlastico = this.divisionPlastico,
            divisionCarton = this.divisionCarton,
            divisionMetal = this.divisionMetal,
            //Refinados
            vidrioRefinado = this.vidrioRefinado,
            plasticoRefinado = this.plasticoRefinado,
            cartonRefinado = this.cartonRefinado,
            metalRefinado = this.metalRefinado,
            compostRefinado = this.compostRefinado
        };
    }
    //LoadState carga los datos desde el guardado y los asigna a los accesibles, segui el formato de las variables ya puestas
    public void LoadState(object state)
    {
        var saveData = (SaveData)state;
        //Trash
        noRecTrash = saveData.noRecTrash;
        organicTrash = saveData.organicTrash;
        vidrioTrash = saveData.vidrioTrash;
        plasticoTrash = saveData.plasticoTrash;
        cartonTrash = saveData.cartonTrash;
        metalTrash = saveData.metalTrash;
        //Old
        recTrash = saveData.recTrash;
        divisionRec = saveData.divisionRec;
        //
        cardDistribution = saveData.cardDistribution;
        //Divisiones
        divisionNoRec = saveData.divisionNoRec;
        divisionOrganic = saveData.divisionOrganic;
        divisionVidrio = saveData.divisionVidrio;
        divisionPlastico = saveData.divisionPlastico;
        divisionCarton = saveData.divisionCarton;
        divisionMetal = saveData.divisionMetal;
        //Refinados
        vidrioRefinado = saveData.vidrioRefinado;
        plasticoRefinado = saveData.plasticoRefinado;
        cartonRefinado = saveData.cartonRefinado;
        metalRefinado = saveData.metalRefinado;
        compostRefinado = saveData.compostRefinado;
    }
    [Serializable]
    private struct SaveData
    {
        //Esto ni me acuerdo para que es pero, asignale las variables como si las estuvieras declarando normalmente, no hay mucha magia 
        public int noRecTrash, organicTrash, vidrioTrash, plasticoTrash, cartonTrash, metalTrash;
        public int recTrash, divisionRec;
        public int divisionNoRec, divisionOrganic, divisionVidrio, divisionPlastico, divisionCarton, divisionMetal;
        public int vidrioRefinado, plasticoRefinado, cartonRefinado, metalRefinado, compostRefinado;
        public bool cardDistribution;
    }
}
