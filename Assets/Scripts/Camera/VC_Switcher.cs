using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VC_Switcher : MonoBehaviour
{
    [SerializeField] GameObject VC_MainMenu;
    [SerializeField] GameObject VC_Transition_01;
    [SerializeField] GameObject VC_PlayerView;

    public void VC_MainMenuSwitcher(bool state)
    {
        VC_MainMenu.SetActive(state);
    }
    public void VC_Transition_01Switcher(bool state)
    {
        VC_Transition_01.SetActive(state);
    }
    public void VC_PlayerViewSwitcher(bool state)
    {
        VC_PlayerView.SetActive(state);
    }
}
