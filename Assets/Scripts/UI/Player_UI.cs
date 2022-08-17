using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Player_UI : MonoBehaviour
{
    #region Imports & Required Objects
    public GlobalVariables globalVariables;
    public GameObject bag;
    TextMeshProUGUI trashText;
    GameObject firstArrow, secondArrow, thirdArrow, greenButton;
    public TextMeshProUGUI vidrioRefText, plasticoRefText, compostText, cartonRefText, metalRefText;
    public GameObject FadePanel;
    public PlayerInteraction playerInteraction;
    #endregion
    float fadeTime;
    public int fadeState;
    Color oldColor;
    void Start()
    {
        trashText = bag.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        firstArrow = bag.transform.GetChild(1).gameObject;
        secondArrow = bag.transform.GetChild(2).gameObject;
        thirdArrow = bag.transform.GetChild(3).gameObject;
        greenButton = bag.transform.GetChild(4).gameObject;
        Color oldColor = FadePanel.GetComponent<Image>().color;
    }

    void LateUpdate()
    {
        DisplayRefTrash();
        if (fadeState == 1)
        {
            FadeIn();
        }
        if (fadeState == 2)
        {
            FadeOut();
        }
    }
    public void DisplayBagPercentage()
    {
        trashText.text = playerInteraction.bagPercentage + " %";
        switch (playerInteraction.bagPercentage)
        {
            case 0:
                firstArrow.SetActive(false);
                secondArrow.SetActive(false);
                thirdArrow.SetActive(false);
                greenButton.SetActive(false);
                break;
            case 33:
                firstArrow.SetActive(true);
                break;
            case 66:
                secondArrow.SetActive(true);
                break;
            case 100:
                thirdArrow.SetActive(true);
                greenButton.SetActive(true);
                break;
        }
    }
    void DisplayRefTrash()
    {
        vidrioRefText.text = globalVariables.vidrioRefinado.ToString();
        plasticoRefText.text = globalVariables.plasticoRefinado.ToString();
        compostText.text = globalVariables.compostRefinado.ToString();
        cartonRefText.text = globalVariables.cartonRefinado.ToString();
        metalRefText.text = globalVariables.metalRefinado.ToString();
    }

    void FadeIn()
    {
        fadeTime += Time.deltaTime * 2;
        if (fadeTime <= 1)
        {
            FadePanel.GetComponent<Image>().color = new Color(oldColor.r, oldColor.g, oldColor.b, fadeTime);
        }
    }
    void FadeOut()
    {
        fadeTime -= Time.deltaTime * 2;
        if (fadeTime >= 0)
        {
            FadePanel.GetComponent<Image>().color = new Color(oldColor.r, oldColor.g, oldColor.b, fadeTime);
        }
        else
        {
            fadeState = 0;
            FadePanel.SetActive(false);
        }
    }
    public void SetFade(int value)
    {
        fadeTime = 1;
        FadePanel.SetActive(true);
        FadePanel.GetComponent<Image>().color = new Color(oldColor.r, oldColor.g, oldColor.b, value);
    }
}
