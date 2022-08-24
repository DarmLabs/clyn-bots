using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AspireInteraction : MonoBehaviour
{
    bool startLerp;
    GameObject target;
    public PlayerInteraction playerInteraction;
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
        transform.position = Vector3.Lerp(transform.position, target.transform.GetChild(0).position, 0.5f);
        transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(0, 0, 0), 0.5f);
        if (transform.localScale.x == 0)
        {
            Destroy(gameObject);
        }
    }
    void Aspire()
    {
        mainMission = playerInteraction.mainMission;
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

        playerInteraction.saveSystem.Save();
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

}
