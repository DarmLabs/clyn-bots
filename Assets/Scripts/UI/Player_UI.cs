using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Player_UI : MonoBehaviour
{
    public GameObject SaveData;
    globalVariables globalVariables;
    public TextMeshProUGUI trashText;
    public GameObject FadePanel;
    float fadeTime;
    public int fadeState;
    Color oldColor;
    void Start()
    {
        globalVariables = SaveData.GetComponent<globalVariables>();
        Color oldColor = FadePanel.GetComponent<Image>().color;
    }

    void LateUpdate()
    {
        trashText.text = "Organicos "+ globalVariables.organicTrash + "\nRecuperables "+ globalVariables.recTrash + "\nNo Recuperables " + globalVariables.noRecTrash;                    
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
