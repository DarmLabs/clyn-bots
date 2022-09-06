using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class RefinerPanelUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI vidrioText, plasticoText, cartonText, metalText;
    [SerializeField] GlobalVariables gv;
    [SerializeField] GameObject selector;
    [SerializeField] OnPointerOver[] onPointerOvers;
    TextMeshProUGUI selectorText;
    int selectorMax, selectorMin = 0;
    void OnEnable()
    {
        CheckVariables();
    }
    void Awake()
    {
        selectorText = selector.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }
    void CheckVariables()
    {
        vidrioText.text = gv.divisionVidrio.ToString();
        plasticoText.text = gv.divisionPlastico.ToString();
        cartonText.text = gv.divisionCarton.ToString();
        metalText.text = gv.divisionMetal.ToString();
    }
    public void HighlightSelected(TextMeshProUGUI text)
    {
        foreach (var components in onPointerOvers)
        {
            components.enabled = false;
        }
        selectorMax = Convert.ToInt32(text.text);
        selector.SetActive(true);
    }
}
