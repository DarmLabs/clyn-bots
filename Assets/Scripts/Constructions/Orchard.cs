using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orchard : MonoBehaviour
{
    General_UI general_UI;
    [SerializeField] GameObject landParent;
    [SerializeField] Transform[] land;
    [SerializeField] Mesh brote, smallTomato, growTomato, smallCarrot, growCarrot;
    [SerializeField] string currentState;
    public string seedType;
    [SerializeField] GameObject seedSelection, seedBtn, growBtn;
    [SerializeField] ConstructibleObj constructible;
    void Start()
    {
        land = landParent.GetComponentsInChildren<Transform>();
        constructible = GetComponent<ConstructibleObj>();
    }
    public void ActivatePanel()
    {
        general_UI = GameObject.FindObjectOfType<General_UI>();
        general_UI.OrchardPanelSwitcher(true);
        general_UI.MinimapSwitcher(false);
        CheckState();
    }
    public void PlantSeed()
    {
        foreach (var pot in land)
        {
            if (pot.gameObject.name != "Compu_Portatil")
            {
                pot.gameObject.GetComponent<MeshFilter>().mesh = brote;
            }
        }
        currentState = "Seeded";
    }
    public void GrowSeed()
    {
        Mesh targetMesh = new Mesh();
        if (currentState == "Seeded")
        {
            switch (seedType)
            {
                case "Tomate":
                    targetMesh = smallTomato;
                    break;
                case "Zanahoria":
                    targetMesh = smallCarrot;
                    break;
            }
        }
        else if (currentState == "Mini")
        {
            switch (seedType)
            {
                case "Tomate":
                    targetMesh = growTomato;
                    break;
                case "Zanahoria":
                    targetMesh = growCarrot;
                    break;
            }
            gameObject.tag = "Untagged";
        }
        foreach (var pot in land)
        {
            if (pot.gameObject.name != "Compu_Portatil")
            {
                pot.gameObject.GetComponent<MeshFilter>().mesh = targetMesh;
            }
        }
        currentState = "Mini";
    }
    public void CheckState()
    {
        switch (currentState)
        {
            case "":
                seedSelection.SetActive(true);
                seedBtn.SetActive(true);
                growBtn.SetActive(false);
                break;
            case "Seeded":
                seedSelection.SetActive(false);
                seedBtn.SetActive(false);
                growBtn.SetActive(true);
                break;
            case "Mini":
                seedSelection.SetActive(false);
                seedBtn.SetActive(false);
                growBtn.SetActive(true);
                break;
        }
    }
}
