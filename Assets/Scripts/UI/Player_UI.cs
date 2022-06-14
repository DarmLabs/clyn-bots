using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Player_UI : MonoBehaviour
{
    public GameObject Player;
    PlayerInteraction playerInteraction;
    public TextMeshProUGUI trashText;
    public GameObject FadePanel;
    float fadeTime;
    public int fadeState;
    
    Color oldColor;
    void Start()
    {
        playerInteraction = Player.GetComponent<PlayerInteraction>();
        Color oldColor = FadePanel.GetComponent<Image>().color;
    }

    void LateUpdate()
    {
        trashText.text = "Organicos "+ playerInteraction.organicTrash + "\nRecuperables "+ playerInteraction.recTrash + "\nNo Recuperables " + playerInteraction.noRecTrash;                    
        if(fadeState == 1){
            FadeIn();
        }
        if(fadeState == 2){
            FadeOut();
        }
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
