using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class FilterManager : MonoBehaviour
{
    RefinerPanelUI refinerPanelUI;
    FilterComponent[] filterComponents;
    [SerializeField] GameObject[] resources;
    [SerializeField] GameObject textContainer;
    public GameObject guideText, noSufText;
    [SerializeField] TextMeshProUGUI refinedName, recycleType, quantity, numPadText;
    [SerializeField] TextMeshProUGUI plasticText, vidrioText, cartonText, metalText;
    public GlobalVariables gv;
    [HideInInspector] public int plasticoValue, vidrioValue, cartonValue, metalValue;
    void Awake()
    {
        refinerPanelUI = GameObject.FindObjectOfType<RefinerPanelUI>();
        filterComponents = GetComponentsInChildren<FilterComponent>();
        gv = GameObject.FindObjectOfType<GlobalVariables>();
        DisplayFilterQuantity();
    }
    public void DisplayFilterQuantity()
    {
        plasticText.text = "x" + plasticoValue.ToString();
        vidrioText.text = "x" + vidrioValue.ToString();
        cartonText.text = "x" + cartonValue.ToString();
        metalText.text = "x" + metalValue.ToString();
    }
    public void ReciveActiveFilter(string code)
    {
        if (code != "")
        {
            guideText.SetActive(false);
            noSufText.SetActive(false);
        }
        else
        {
            guideText.SetActive(true);
            noSufText.SetActive(false);
        }
        foreach (var button in filterComponents)
        {
            if (button.gameObject.name == code)
            {
                button.OnSelected(false);
            }
            else
            {
                button.OnDeselect();
            }
        }
        ActiveSelectedResource(code);
    }
    void ActiveSelectedResource(string code)
    {
        foreach (var resource in resources)
        {
            if (resource.name == code + "Refinado")
            {
                resource.SetActive(true);
            }
            else
            {
                resource.SetActive(false);
            }
        }
        if (code != "")
        {
            RefreshRefinerInfo(code);
        }
        else
        {
            textContainer.SetActive(false);
        }
        DisplayFilterQuantity();
    }
    public void RefreshRefinerInfo(string code)
    {
        if (!textContainer.activeSelf)
        {
            textContainer.SetActive(true);
        }
        switch (code)
        {
            case "Plastico":
                numPadText.text = "x" + plasticoValue.ToString();
                refinedName.text = "PLÁSTICO RECICLADO";
                recycleType.text = "RECICLAJE DE PLÁSTICO";
                quantity.text = (plasticoValue * 10).ToString();
                break;
            case "Vidrio":
                numPadText.text = "x" + vidrioValue.ToString();
                refinedName.text = "VIDRIO RECICLADO";
                recycleType.text = "RECICLAJE DE VIDRIO";
                quantity.text = (vidrioValue * 10).ToString();
                break;
            case "Carton":
                numPadText.text = "x" + cartonValue.ToString();
                refinedName.text = "CARTÓN RECICLADO";
                recycleType.text = "RECICLAJE DE CARTÓN";
                quantity.text = (cartonValue * 10).ToString();
                break;
            case "Metal":
                numPadText.text = "x" + metalValue.ToString();
                refinedName.text = "METAL RECICLADO";
                recycleType.text = "RECICLAJE DE METAL";
                quantity.text = (metalValue * 10).ToString();
                break;
        }
    }
    public void SaveFilterValues(string code, int value)
    {
        switch (code)
        {
            case "Plastico":
                plasticoValue += value;
                break;
            case "Vidrio":
                vidrioValue += value;
                break;
            case "Carton":
                cartonValue += value;
                break;
            case "Metal":
                metalValue += value;
                break;
        }
        SubstractDivValues(code, value);
        ReciveActiveFilter(code);
    }
    public void SubstractDivValues(string code, int value)
    {
        switch (code)
        {
            case "Plastico":
                refinerPanelUI.plasticoDiv -= value * 10;
                break;
            case "Vidrio":
                refinerPanelUI.vidrioDiv -= value * 10;
                break;
            case "Carton":
                refinerPanelUI.cartonDiv -= value * 10;
                break;
            case "Metal":
                refinerPanelUI.metalDiv -= value * 10;
                break;
        }
        refinerPanelUI.CheckVariables();
    }
    public void ResetValues()
    {
        vidrioValue = 0;
        plasticoValue = 0;
        cartonValue = 0;
        metalValue = 0;
        ReciveActiveFilter("");
        refinerPanelUI.TakeValuesFromGV();
        refinerPanelUI.RefineButtonsSwitcher(false);
    }
}
