using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VC_Switcher : MonoBehaviour
{
    Cinemachine.CinemachineBrain CM_Brain;
    [SerializeField] General_UI general_UI;
    [SerializeField] GameObject VC_MainMenu;
    [SerializeField] GameObject VC_Transition_01;
    [SerializeField] GameObject VC_PlayerView;
    int checkBlendNeeded;
    void Start()
    {
        CM_Brain = GetComponent<Cinemachine.CinemachineBrain>();
    }
    void LateUpdate()
    {
        if (checkBlendNeeded != 0)
        {
            CheckBlend(checkBlendNeeded);
        }
    }

    public void VC_MainMenuSwitcher(bool state)
    {
        VC_MainMenu.SetActive(state);
    }
    public void VC_Transition_01Switcher(bool state)
    {
        VC_Transition_01.SetActive(state);
        checkBlendNeeded = 1;
    }
    public void VC_PlayerViewSwitcher(bool state)
    {
        VC_PlayerView.SetActive(state);
        checkBlendNeeded = 2;
    }
    void CheckBlend(int code)
    {
        if (CM_Brain.ActiveBlend != null && CM_Brain.ActiveBlend.BlendWeight >= 0.9999f)
        {
            if (code == 1)
            {
                general_UI.StartPanelSwitcher(true);
            }
            if (code == 2)
            {
                general_UI.playerInteraction.MovmentState(true);
                general_UI.MainPanelSwitcher(true);
            }
            checkBlendNeeded = 0;
        }
    }
}
