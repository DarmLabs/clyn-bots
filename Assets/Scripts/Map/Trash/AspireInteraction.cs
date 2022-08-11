using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AspireInteraction : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerInteraction playerInteraction = other.GetComponent<PlayerInteraction>();
            if (playerInteraction.isAspiring == true)
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
        }
    }
}
