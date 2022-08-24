using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Player_UI : MonoBehaviour
{
    #region Imports & Required Objects
    GlobalVariables globalVariables;
    public GameObject bag;
    [SerializeField] TextMeshProUGUI trashText;
    [SerializeField] GameObject greenButton;
    [SerializeField] Slider aspireProgress;
    public TextMeshProUGUI vidrioRefText, plasticoRefText, compostText, cartonRefText, metalRefText;
    public GameObject FadePanel;
    public PlayerInteraction playerInteraction;
    #endregion
    float fadeTime;
    public int fadeState;
    Color oldColor;
    void Start()
    {
        Color oldColor = FadePanel.GetComponent<Image>().color;
        globalVariables = GameObject.FindObjectOfType<GlobalVariables>().GetComponent<GlobalVariables>();
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
        aspireProgress.value = playerInteraction.bagPercentage;
        if (playerInteraction.bagPercentage == 100)
        {
            greenButton.SetActive(true);
        }
        else
        {
            greenButton.SetActive(false);
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
