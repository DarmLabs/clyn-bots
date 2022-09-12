using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AspireInteraction : MonoBehaviour, ISaveable
{
    public bool destroyed;
    bool startLerp;
    GameObject target;
    public PlayerInteraction playerInteraction;
    EnviromentChanger enviromentChanger;
    MainMission mainMission;
    void Update()
    {
        if (startLerp)
        {
            LerpToHand();
        }
        if (playerInteraction != null && !startLerp)
        {
            if (playerInteraction.isAspiring)
            {
                Aspire();
                startLerp = true;
                GetComponent<BoxCollider>().enabled = false;
            }
        }
    }
    void LerpToHand()
    {
        Debug.Log(transform.position + "position");
        Debug.Log(transform.localScale + "scale");
        transform.position = Vector3.Lerp(transform.position, target.transform.GetChild(0).position, 0.5f);
        transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(0, 0, 0), 0.5f);
        if (transform.localScale.x == 0)
        {
            gameObject.SetActive(false);
        }
    }
    void Aspire()
    {
        mainMission = playerInteraction.mainMission;
        enviromentChanger = playerInteraction.enviromentChanger;
        enviromentChanger.CheckZones(Convert.ToInt32(transform.parent.gameObject.name));
        switch (this.gameObject.tag)
        {
            case "Vidrio":
                playerInteraction.gv.vidrioTrash++;
                break;
            case "Plastico":
                playerInteraction.gv.plasticoTrash++;
                break;
            case "Compostable":
                playerInteraction.gv.organicTrash++;
                break;
            case "Carton":
                playerInteraction.gv.cartonTrash++;
                break;
            case "Metal":
                playerInteraction.gv.metalTrash++;
                break;
            case "NoRecuperable":
                playerInteraction.gv.noRecTrash++;
                break;

        }
        if (mainMission.trashRecolected < mainMission.maxTrash)
        {
            mainMission.trashRecolected++;
        }
        destroyed = true;
        playerInteraction.BagPercentage();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerAspire")
        {
            target = other.gameObject.transform.parent.gameObject;
            playerInteraction = target.GetComponent<PlayerInteraction>();
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "PlayerAspire")
        {
            target = null;
            playerInteraction = null;
        }
    }
    public object SaveState()
    {
        return new SaveData()
        {
            destroyed = this.destroyed
        };
    }
    //LoadState carga los datos desde el guardado y los asigna a los accesibles, segui el formato de las variables ya puestas
    public void LoadState(object state)
    {
        var saveData = (SaveData)state;
        destroyed = saveData.destroyed;
        Debug.Log(destroyed);
        if (destroyed)
        {
            Destroy(gameObject);
        }
    }
    [Serializable]
    private struct SaveData
    {
        public bool destroyed;
    }
}
