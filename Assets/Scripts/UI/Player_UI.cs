using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Player_UI : MonoBehaviour
{
    #region Imports & Required Objects
    public GlobalVariables globalVariables;
    public TextMeshProUGUI trashText;
    public TextMeshProUGUI vidrioRefText, plasticoRefText, compostText, cartonRefText, metalRefText;
    public GameObject FadePanel;
    public GameObject Player;
    PlayerInteraction playerInteraction;
    #endregion
    float fadeTime;
    public int fadeState;
    
    Color oldColor;
    void Start()
    {
        Color oldColor = FadePanel.GetComponent<Image>().color;
        playerInteraction = Player.GetComponent<PlayerInteraction>();
    }

    void LateUpdate()
    {
        DisplayBagPercentage();   
        DisplayRefTrash();           
        if(fadeState == 1){
            FadeIn();
        }
        if(fadeState == 2){
            FadeOut();
        }
    }
    void DisplayBagPercentage(){
        trashText.text = "Mochila " + playerInteraction.bagPercentage + " %";
    }
    void DisplayRefTrash(){
        vidrioRefText.text = "x " + globalVariables.vidrioRefinado;
        plasticoRefText.text = "x " + globalVariables.plasticoRefinado;
        compostText.text = "x " + globalVariables.compostRefinado;
        cartonRefText.text = "x " + globalVariables.cartonRefinado;
        metalRefText.text = "x " + globalVariables.metalRefinado;
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
