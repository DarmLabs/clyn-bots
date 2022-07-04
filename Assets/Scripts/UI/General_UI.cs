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
    public Button constructionBtn;
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
                break;
            case "Sembrar":
                break;
        }
    }
    public void ConstructionButtonState(bool state){
        constructionBtn.enabled = state;
        if(!state){
            constructionBtn.image.color = lockColor;
        }else{
            constructionBtn.image.color = unlockColor;
        }
    }
    public void MinigamePanelSwitcher(bool state){
        miniGamePanel.SetActive(state);
        if(state){
            MainPanelSwitcher(false);
        }else{
            MainPanelSwitcher(true);
        }
    }
    
    public void ChangeScene(string scene){
        SceneManager.LoadScene(scene);
    }
}
