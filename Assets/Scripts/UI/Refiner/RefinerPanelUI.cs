using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class RefinerPanelUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI vidrioText, plasticoText, cartonText, metalText;
    [HideInInspector] public int vidrioDiv, plasticoDiv, cartonDiv, metalDiv;
    [HideInInspector] GlobalVariables gv;
    [SerializeField] GameObject selector;
    [SerializeField] GameObject toRefineButtons;
    TextMeshProUGUI selectorText;
    int selectorMax = 200, selectorMin = 0;
    [HideInInspector] public int selectorValue;
    void OnEnable()
    {
        TakeValuesFromGV();
    }
    void Awake()
    {
        selectorText = selector.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        gv = GameObject.FindObjectOfType<GlobalVariables>();
    }
    public void TakeValuesFromGV()
    {
        vidrioDiv = gv.divisionVidrio;
        plasticoDiv = gv.divisionPlastico;
        cartonDiv = gv.divisionCarton;
        metalDiv = gv.divisionMetal;
        CheckVariables();
    }
    public void CheckVariables()
    {
        vidrioText.text = vidrioDiv.ToString();
        plasticoText.text = plasticoDiv.ToString();
        cartonText.text = cartonDiv.ToString();
        metalText.text = metalDiv.ToString();
    }
    public void SelectorValue(int value)
    {
        if (selectorValue > selectorMin && selectorValue < selectorMax)
        {
            selectorValue += value;
            selectorText.text = selectorValue.ToString();
        }
        else if (selectorValue == 0 && value == 10 || selectorValue == 200 && value == -10)
        {
            selectorValue += value;
            selectorText.text = selectorValue.ToString();
        }
    }
    public void ResetSelectorValue()
    {
        selectorValue = 0;
        selectorText.text = selectorValue.ToString();
    }
    public void Locked()
    {
        selector.GetComponent<Animator>().Play("Locked");
    }
    public void RefineButtonsSwitcher(bool state)
    {
        toRefineButtons.SetActive(state);
    }
}
