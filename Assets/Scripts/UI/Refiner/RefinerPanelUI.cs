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
    [SerializeField] GameObject toRefineButtons;
    void OnEnable()
    {
        TakeValuesFromGV();
    }
    void Awake()
    {
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
    public void RefineButtonsSwitcher(bool state)
    {
        toRefineButtons.SetActive(state);
    }
}
