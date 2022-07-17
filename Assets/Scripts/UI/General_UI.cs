using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class General_UI : MonoBehaviour
{
    public PlayerInteraction playerInteraction;
    public GameObject mainPanel;
    public GameObject miniGamePanel;
    public GameObject constructionPanel;
    public GameObject minimap;
    public GameObject constructonRender;
    public Sprite Panel, Molino,Bomba , Trigo, Zanahoria, Tomate;
    public GameObject exitPanel;
    public TextMeshProUGUI constructionTitle, reqVidrio, reqPlastico, reqCompost, reqMetal, reqCarton;
    public GameObject constructionBtn;
    public GameObject upgradeBtn;
    public GameObject seedSection;
    public GameObject []plantacionBtns = new GameObject[3];
    public Color32 lockColor;
    public Color32 unlockColor;
    public GameObject minigameAspire;
    public GameObject interactionCloud;
    

    public void MainPanelSwitcher(bool state){
        mainPanel.SetActive(state);
    }
    public void ConstructionPanelSwitcher(bool state){
        constructionPanel.SetActive(state);
    }
    public void BuildingConstructionMenu(string title, string[] req, string reqSprite)
    {
        constructionTitle.text = title; 

        reqVidrio.text = req[0];
        reqPlastico.text = req[1];
        reqCarton.text = req[2];
        reqMetal.text = req[3];
        reqCompost.text = req[4];

        bool found = false;
        switch (reqSprite)
        {
            case "Panel Solar":
            constructonRender.GetComponent<Image>().sprite = Panel;
            found = true;
                break;
            case "Molino":
            constructonRender.GetComponent<Image>().sprite = Molino;
            found = true;
                break;
            case "Bomba de Agua":
            constructonRender.GetComponent<Image>().sprite = Bomba;
            found = true;
                break;
            case "Trigo":
            constructonRender.GetComponent<Image>().sprite = Trigo;
            found = true;
                break;
            case "Zanahoria":
            constructonRender.GetComponent<Image>().sprite = Zanahoria;
            found = true;
                break;
            case "Tomate":
            constructonRender.GetComponent<Image>().sprite = Tomate;
            found = true;
                break;
        }
        if(!found){
            constructonRender.GetComponent<Image>().sprite = null;
        }
    }
    public void EnabledSection(string name){
        switch (name)
        {
            case "Construir":
                constructionBtn.SetActive(true);
                seedSection.SetActive(false);
                upgradeBtn.SetActive(false);
                break;
            case "Sembrar":
                seedSection.SetActive(true);
                constructionBtn.SetActive(false);
                upgradeBtn.SetActive(false);
                break;
            case "Mejorar":
                seedSection.SetActive(false);
                upgradeBtn.SetActive(true);
                constructionBtn.SetActive(false);
                break;
        }
    }
    public void ConstructionButtonState(bool state){
        constructionBtn.GetComponent<Button>().enabled = state;
        if(!state){
            constructionBtn.GetComponent<Button>().image.color = lockColor;
        }else{
            constructionBtn.GetComponent<Button>().image.color = unlockColor;
        }
    }
    public void SeedButtonsState(bool state){
        if(!state){
            foreach (var button in plantacionBtns)
            {
                button.GetComponent<Button>().enabled = state;
                button.GetComponent<Button>().image.color = lockColor;
            }
        }else{
            int i = 0;
            foreach (var button in plantacionBtns)
            {   
                button.GetComponent<Button>().onClick.RemoveAllListeners();
                button.GetComponent<Button>().enabled = state;
                button.GetComponent<Button>().image.color = unlockColor;
                if(playerInteraction.targetConstruction.GetComponent<Seed>() != null){
                    switch (i)
                    {
                        case 0:
                            button.GetComponent<Button>().onClick.AddListener(()=>{playerInteraction.targetConstruction.GetComponent<Seed>().ChooseSeed(0);});
                            break;
                        case 1:
                            button.GetComponent<Button>().onClick.AddListener(()=>{playerInteraction.targetConstruction.GetComponent<Seed>().ChooseSeed(1);});
                            break;
                        case 2:
                            button.GetComponent<Button>().onClick.AddListener(()=>{playerInteraction.targetConstruction.GetComponent<Seed>().ChooseSeed(2);});
                            break;
                    }
                    button.GetComponent<Button>().onClick.AddListener(delegate{playerInteraction.targetConstruction.GetComponent<Seed>().PlaceSeed();});
                }
                i += 1;
            }
        }
    }
    public void UpgradeButtonState(bool state){
        upgradeBtn.GetComponent<Button>().enabled = state;
        upgradeBtn.GetComponent<Button>().onClick.RemoveAllListeners();
        upgradeBtn.GetComponent<Button>().onClick.AddListener(delegate{playerInteraction.targetConstruction.GetComponent<Seed>().GrowSeed();});
        if(!state){
            upgradeBtn.GetComponent<Button>().image.color = lockColor;
        }else{
            upgradeBtn.GetComponent<Button>().image.color = unlockColor;
        }
    }
    public void MinigamePanelSwitcher(bool state){
        GameObject memoryBtn;
        GameObject centarlBtn;
        memoryBtn = miniGamePanel.transform.GetChild(2).gameObject;
        centarlBtn = miniGamePanel.transform.GetChild(1).gameObject;
        GlobalVariables globalVariables;
        globalVariables = playerInteraction.globalVariables;
        bool noResources = true;
        if(globalVariables.divisionNoRec == 0 && globalVariables.divisionRec ==0 && globalVariables.divisionOrganic == 0){
            memoryBtn.SetActive(false);
        }else{
            memoryBtn.SetActive(true);
            noResources = false;
        }
        if(globalVariables.recTrash == 0 && globalVariables.noRecTrash == 0 && globalVariables.organicTrash == 0){
            centarlBtn.SetActive(false);
        }else{
            centarlBtn.SetActive(true);
            noResources = false;
        }
        if(noResources){
            Debug.Log("No tienes basura para separar en los minijuegos");
        }else{
            playerInteraction.interactionHappen = true;
            miniGamePanel.SetActive(state);
            MainPanelSwitcher(false);
            playerInteraction.SaveTransform();
        }
    }
    public void MinigameAspireSwitcher(bool state){
        minigameAspire.SetActive(state);
        if(state){
            MainPanelSwitcher(false);
            playerInteraction.BagPercentage();
            playerInteraction.enabled = false;
        }else{
            MainPanelSwitcher(true);
            playerInteraction.enabled = true;
            playerInteraction.playerAnim.Aspire(false);
        }
    }
    public void MinimapSwitcher(bool state){
        minimap.SetActive(state);
    }
    public void ExitPanelSwitcher(bool state){
        exitPanel.SetActive(state);
    }
    public void InteractionCloud(bool state){
        interactionCloud.SetActive(state);
    }
    public void ChangeScene(string scene){
        SceneManager.LoadScene(scene);
    }
    public void ExitGame(){
        Application.Quit();
    }
}
