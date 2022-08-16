using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentralMinigamePad : MonoBehaviour
{
    [SerializeField] DepositObject deposit;
    [SerializeField] General_UI general_UI;

    public void ActivatePanel()
    {
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            general_UI.InteractionCloud(true);
            general_UI.playerInteraction.targetPad = gameObject;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("salio");
            general_UI.InteractionCloud(false);
            general_UI.playerInteraction.targetPad = null;
        }
    }
}
