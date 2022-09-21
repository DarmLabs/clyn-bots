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
    GlobalVariables gv;
    [SerializeField] TutorialManager tutorialManager;
    AudioManager audioManager;
    int checkBlendNeeded;
    bool transition_01Used;
    void Start()
    {
        CM_Brain = GetComponent<Cinemachine.CinemachineBrain>();
        audioManager = general_UI.playerInteraction.audioManager;
        gv = general_UI.playerInteraction.gv;
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
        if (!transition_01Used)
        {
            VC_Transition_01.SetActive(state);
            if (state)
            {
                audioManager.StopMusic();
                audioManager.PlayMusic("Inside_Theme");
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
            checkBlendNeeded = 0;
        }
    }
    public void CheckFirstTime()
    {
        if (!gv.firstTime)
        {
            general_UI.TutorialPanelSwithcer(true);
            tutorialManager.EnableTutorial();
            general_UI.MainMenuSwitcher(false);
        }
        else
        {
            NotFirstTime();
            general_UI.playerInteraction.MovmentState(true);
            general_UI.MainMenuSwitcher(false);
            general_UI.MainPanelSwitcher(true);
            audioManager.StopMusic();
            audioManager.PlayMusic("Inside_Theme");
        }
    }
    public void NotFirstTime()
    {
        CM_Brain.m_DefaultBlend.m_Time = 0;
        VC_MainMenu.SetActive(false);
        VC_Transition_01.SetActive(false);
        VC_PlayerView.SetActive(true);
    }
}
