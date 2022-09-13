using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPanel : MonoBehaviour
{
    public void DestroyThisPanel()
    {
        if (gameObject.name == "Salida(Clone)")
        {
            VC_Switcher vC_Switcher = GameObject.FindObjectOfType<VC_Switcher>();
            vC_Switcher.VC_MainMenuSwitcher(false);
            vC_Switcher.VC_Transition_01Switcher(true);
        }
        Destroy(gameObject);
    }
    public void GetParentScript(string name)
    {
        TutorialManager tutorialManager = GetComponentInParent<TutorialManager>();
        tutorialManager.SpawnTutorialWindow(name);
    }
}
