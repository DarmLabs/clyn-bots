using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TutorialPanel : MonoBehaviour
{
    public void DestroyThisPanel()
    {
        if (gameObject.name == "Salida(Clone)" && EventSystem.current.currentSelectedGameObject.name == "Cerrar")
        {
            TutorialManager tutorialManager = GetComponentInParent<TutorialManager>();
            VC_Switcher vC_Switcher = GameObject.FindObjectOfType<VC_Switcher>();
            if (!tutorialManager.fromMenu)
            {
                vC_Switcher.VC_MainMenuSwitcher(false);
                vC_Switcher.VC_Transition_01Switcher(true);
            }
        }
        Destroy(gameObject);
    }
    public void GetParentScript(string name)
    {
        TutorialManager tutorialManager = GetComponentInParent<TutorialManager>();
        tutorialManager.SpawnTutorialWindow(name);
    }
}
