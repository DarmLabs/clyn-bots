using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class FilterManager : MonoBehaviour
{
    RefinerPanelUI refinerPanelUI;
    FilterComponent[] filterComponents;
    [SerializeField] GameObject[] resources;
    [SerializeField] GameObject numPad;
    [SerializeField] GameObject guideText;
    public GlobalVariables gv;
    [HideInInspector] public int plasticoValue, vidrioValue, cartonValue, metalValue;
    void Awake()
    {
        refinerPanelUI = GameObject.FindObjectOfType<RefinerPanelUI>();
        filterComponents = GetComponentsInChildren<FilterComponent>();
        gv = GameObject.FindObjectOfType<GlobalVariables>();
    }
    public void ReciveActiveFilter(string code)
    {
        if (code != "")
        {
            guideText.SetActive(false);
        }
        else
        {
            guideText.SetActive(true);
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
            RefreshNumPad(code);
        }
        else
        {
            numPad.SetActive(false);
        }
    }
    public void RefreshNumPad(string code)
    {
        if (!numPad.activeSelf)
        {
            numPad.SetActive(true);
        }
        TextMeshProUGUI numPadText = numPad.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        switch (code)
        {
            case "Plastico":
                numPadText.text = plasticoValue.ToString();
                break;
            case "Vidrio":
                numPadText.text = vidrioValue.ToString();
                break;
            case "Carton":
                numPadText.text = cartonValue.ToString();
                break;
            case "Metal":
                numPadText.text = metalValue.ToString();
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
