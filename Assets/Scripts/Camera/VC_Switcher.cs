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
    bool transition_01Used;
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
        if (state)
        {
            checkBlendNeeded = 3;
        }
    }
    public void VC_Transition_01Switcher(bool state)
    {
        if (!transition_01Used)
        {
            VC_Transition_01.SetActive(state);
            if (state)
            {
                checkBlendNeeded = 1;
            }
            else
            {
                transition_01Used = true;
            }
        }
        else
        {
            VC_PlayerViewSwitcher(state);
        }

    }
    public void VC_PlayerViewSwitcher(bool state)
    {
        VC_PlayerView.SetActive(state);
        if (state)
        {
            checkBlendNeeded = 2;
        }
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
            if (code == 3)
            {
                general_UI.MainMenuSwitcher(true);
            }
            checkBlendNeeded = 0;
        }
    }
}