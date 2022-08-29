using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DepositObject : MonoBehaviour, ISaveable
{
    public bool isFull;
    [SerializeField] int dVidrio, dCarton, dOrganico, dNoRecu, dPlastico, dMetal;
    GlobalVariables gv;
    MissionTrack missionTrack;
    public Transform depositPoint;
    [SerializeField] PlayerInteraction player;
    [SerializeField] General_UI general_UI;
    [SerializeField] GameObject responseRecycler;
    void Start()
    {
        gv = GameObject.FindObjectOfType<GlobalVariables>();
        missionTrack = GameObject.FindObjectOfType<MissionTrack>();
    }

    public void Response(string id)
    {
        player.targetRecycler = responseRecycler;
        responseRecycler.GetComponent<RecyclerNPC>().fromResponse = true;
        responseRecycler.GetComponent<RecyclerNPC>().Speak(id);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            general_UI.InteractionCloud(true);
            general_UI.playerInteraction.targetDeposit = gameObject;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            general_UI.InteractionCloud(false);
            general_UI.playerInteraction.targetDeposit = null;
        }
    }
    public void SetValues(int vidrio, int carton, int organico, int noRecu, int plastico, int metal)
    {
        dVidrio = vidrio;
        dCarton = carton;
        dOrganico = organico;
        dNoRecu = noRecu;
        dPlastico = plastico;
        dMetal = metal;
        isFull = true;
        if (missionTrack.stages[gv.currentMissionStage + 1] = this.gameObject)
        {
            missionTrack.TransportMissionTraget();
        }
    }
    public object SaveState()
    {
        return new SaveData()
        {
            dVidrio = this.dVidrio,
            dCarton = this.dCarton,
            dOrganico = this.dOrganico,
            dNoRecu = this.dNoRecu,
            dPlastico = this.dPlastico,
            dMetal = this.dMetal,
            isFull = this.isFull
        };
    }
    //LoadState carga los datos desde el guardado y los asigna a los accesibles, segui el formato de las variables ya puestas
    public void LoadState(object state)
    {
        var saveData = (SaveData)state;
        dVidrio = saveData.dVidrio;
        dCarton = saveData.dCarton;
        dOrganico = saveData.dOrganico;
        dNoRecu = saveData.dNoRecu;
        dPlastico = saveData.dPlastico;
        dMetal = saveData.dMetal;
        isFull = saveData.isFull;
    }
    [Serializable]
    private struct SaveData
    {
        public int dVidrio, dCarton, dOrganico, dNoRecu, dPlastico, dMetal;
        public bool isFull;
    }
}

