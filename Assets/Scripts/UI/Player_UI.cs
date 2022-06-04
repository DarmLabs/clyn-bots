using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Player_UI : MonoBehaviour
{
    public GameObject Player;
    PlayerInteraction playerInteractionScript;
    public TextMeshProUGUI trashText;
    public GameObject FadePanel;
    float fadeTime;
    public int fadeState;
    Color oldColor;
    void Start()
    {
        playerInteractionScript = Player.GetComponent<PlayerInteraction>();
        Color oldColor = FadePanel.GetComponent<Image>().color;
    }

    void LateUpdate()
    {
        trashText.text = "Organicos "+ playerInteractionScript.organicTrash + "\nRecuperables "+ playerInteractionScript.recTrash + "\nNo Recuperables " + playerInteractionScript.noRecTrash;                    
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
