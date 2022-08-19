using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeStageObject : MonoBehaviour
{
    [SerializeField] string destination;
    [SerializeField] General_UI general_UI;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            general_UI.BuildChangeStagePanel(destination);
            general_UI.ChangeStageSwitcher(true);
            general_UI.MainPanelSwitcher(false);
        }
    }
}
