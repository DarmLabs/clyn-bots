using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Player_UI : MonoBehaviour
{
    #region Imports & Required Objects
    public GameObject GlobalVariables;
    GlobalVariables globalVariables;
    public TextMeshProUGUI trashText;
    public GameObject FadePanel;
    public GameObject Player;
    PlayerInteraction playerInteraction;
    #endregion
    float fadeTime;
    public int fadeState;
    
    Color oldColor;
    void Start()
    {
        globalVariables = GlobalVariables.GetComponent<GlobalVariables>();
        Color oldColor = FadePanel.GetComponent<Image>().color;
        playerInteraction = Player.GetComponent<PlayerInteraction>();
    }

    void LateUpdate()
    {
        DisplayTrashText();              
        if(fadeState == 1){
            FadeIn();
        }
        if(fadeState == 2){
            FadeOut();
        }
    }
    void DisplayTrashText(){
         trashText.text = "Mochila " + playerInteraction.bagPercentage + "%" + "\nOrganicos "+ globalVariables.organicTrash + "\nRecuperables "+ globalVariables.recTrash + "\nNo Recuperables " + globalVariables.noRecTrash;
    }

    void FadeIn(){ 
        fadeTime += Time.deltaTime * 2;
        if(fadeTime <= 1){
            FadePanel.GetComponent<Image>().color = new Color(oldColor.r, oldColor.g, oldColor.b, fadeTime);
        }
    }
    void FadeOut(){
        fadeTime -= Time.deltaTime * 2;
        if(fadeTime >= 0){
                FadePanel.GetComponent<Image>().color = new Color(oldColor.r, oldColor.g, oldColor.b, fadeTime);
        }
        else{
            fadeState = 0;
        }
    }
}
