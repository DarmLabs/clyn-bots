using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class General_UI : MonoBehaviour
{
    public GameObject player;
    PlayerInteraction playerInteraction;
    public GameObject mainPanel;
    public GameObject miniGamePanel;
    public GameObject constructionPanel;
    public TextMeshProUGUI constructionTitle, youHaveRS, reqText;
    public GameObject constructionBtn;
    public GameObject upgradeBtn;
    public GameObject seedSection;
    public GameObject []plantacionBtns = new GameObject[3];
    public Color32 lockColor;
    public Color32 unlockColor;
    void Start()
    {
        playerInteraction = player.GetComponent<PlayerInteraction>();
    }
    public void MainPanelSwitcher(bool state){
        mainPanel.SetActive(state);
    }
    public void ConstructionPanelSwitcher(bool state){
        constructionPanel.SetActive(state);
        if(state){
            MainPanelSwitcher(false);
        }else{
            MainPanelSwitcher(true);
        }
    }
    public void BuildingConstructionMenu(string title, string req)
    {
        constructionTitle.text = "Construyendo " + title; 

        youHaveRS.text =
        "Materiales Refinados que tienes:\nVidrio: " + playerInteraction.globalVariables.vidrioRefinado + 
        "\nPlastico: " + playerInteraction.globalVariables.plasticoRefinado +
        "\nCarton: " + playerInteraction.globalVariables.cartonRefinado +
        "\nMetal: " + playerInteraction.globalVariables.metalRefinado +
        "\nCompost: " + playerInteraction.globalVariables.compostRefinado;

        reqText.text = "Materiales Refinados necesarios:" + req;
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
        miniGamePanel.SetActive(state);
        if(state){
            MainPanelSwitcher(false);
        }else{
            MainPanelSwitcher(true);
        }
        GameObject memoryBtn;
        memoryBtn = miniGamePanel.transform.GetChild(2).gameObject;
        GlobalVariables globalVariables;
        globalVariables = playerInteraction.globalVariables;
        if(globalVariables.divisionNoRec == 0 && globalVariables.divisionRec ==0 && globalVariables.divisionOrganic == 0){
            memoryBtn.SetActive(false);
        }else{
            memoryBtn.SetActive(true);
        }
    }
    
    public void ChangeScene(string scene){
        SceneManager.LoadScene(scene);
    }
}
