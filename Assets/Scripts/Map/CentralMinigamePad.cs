using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CentralMinigamePad : MonoBehaviour
{
    public DepositObject deposit;
    [SerializeField] General_UI general_UI;

    public void ActivatePanel()
    {
        general_UI.CentralMinigameSwitcher(true);
        general_UI.MainPanelSwitcher(false);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            general_UI.InteractionCloud(true);
            general_UI.playerInteraction.targetCentralPad = gameObject;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("salio");
            general_UI.InteractionCloud(false);
            general_UI.playerInteraction.targetCentralPad = null;
        }
    }
    public void ChangeScene()
    {
        SceneManager.LoadScene("Central");
    }
}
