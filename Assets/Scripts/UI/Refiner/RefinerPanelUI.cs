using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class RefinerPanelUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI vidrioText, plasticoText, cartonText, metalText;
    [HideInInspector] GlobalVariables gv;
    [SerializeField] GameObject selector;
    TextMeshProUGUI selectorText;
    int selectorMax = 200, selectorMin = 0;
    [HideInInspector] public int selectorValue;
    void OnEnable()
    {
        CheckVariables();
    }
    void Awake()
    {
        selectorText = selector.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        gv = GameObject.FindObjectOfType<GlobalVariables>();
    }
    void CheckVariables()
    {
        vidrioText.text = gv.divisionVidrio.ToString();
        plasticoText.text = gv.divisionPlastico.ToString();
        cartonText.text = gv.divisionCarton.ToString();
        metalText.text = gv.divisionMetal.ToString();
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
}
