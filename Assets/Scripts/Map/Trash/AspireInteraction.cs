using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AspireInteraction : MonoBehaviour
{
    bool startLerp;
    GameObject target;
    PlayerInteraction playerInteraction;
    void Update()
    {
        if (startLerp)
        {
            Debug.Log(gameObject.name);
            LerpToHand();
        }
        if (playerInteraction != null && !startLerp)
        {
            if (playerInteraction.isAspiring)
            {
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
            Aspire();
            Destroy(gameObject);
        }
    }
    void Aspire()
    {
        switch (this.gameObject.tag)
        {
            case "Vidrio":
                playerInteraction.globalVariables.vidrioTrash++;
                break;
            case "Plastico":
                playerInteraction.globalVariables.plasticoTrash++;
                break;
            case "Compostable":
                playerInteraction.globalVariables.organicTrash++;
                break;
            case "Carton":
                playerInteraction.globalVariables.cartonTrash++;
                break;
            case "Metal":
                playerInteraction.globalVariables.metalTrash++;
                break;
            case "NoRecuperable":
                playerInteraction.globalVariables.noRecTrash++;
                break;

        }
        playerInteraction.BagPercentage();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("detect player " + gameObject.name);
            target = other.gameObject.transform.parent.gameObject;
            playerInteraction = target.GetComponent<PlayerInteraction>();
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            target = null;
            playerInteraction = null;
        }
    }

}
