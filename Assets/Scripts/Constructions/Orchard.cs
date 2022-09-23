using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class Orchard : MonoBehaviour, ISaveable
{
    General_UI general_UI;
    [SerializeField] MeshFilter[] land;
    [SerializeField] Mesh brote, smallTomato, growTomato, smallCarrot, growCarrot, smallOnion, growOnion, smallRab, growRab;
    public string currentState;
    public string seedType;
    [SerializeField] GameObject seedSelection, seedBtn, growBtn;
    [SerializeField] ConstructibleObj constructible;
    MainMission mainMission;
    void Start()
    {
        constructible = GetComponent<ConstructibleObj>();
        mainMission = GameObject.FindObjectOfType<MainMission>();
        general_UI = GameObject.FindObjectOfType<General_UI>();
    }
    public void ActivatePanel()
    {
        general_UI.OrchardPanelSwitcher(true);
        general_UI.MinimapSwitcher(false);
        general_UI.InteractionCloud(false);
        if (currentState == "")
        {
            seedType = "Tomate";
        }
        CheckState();
    }
    public void PlantSeed(bool fromLoad)
    {
        foreach (var pot in land)
        {
            pot.mesh = brote;
        }
        if (!fromLoad)
        {
            currentState = "Seeded";
            constructible.ResourcesSubstraction();
        }
    }
    public void GrowSeed(bool fromLoad)
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
                case "Cebolla":
                    targetMesh = smallOnion;
                    break;
                case "Rabanito":
                    targetMesh = smallRab;
                    break;
            }
            currentState = "Mini";
            if (!fromLoad)
            {
                general_UI.InteractionCloud(true);
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
                case "Cebolla":
                    targetMesh = growOnion;
                    break;
                case "Rabanito":
                    targetMesh = growRab;
                    break;
            }
            gameObject.tag = "Untagged";
            GetComponent<SaveTag>().UpdateTag();
            if (!fromLoad && mainMission.cropsGrew < mainMission.maxCrops)
            {
                mainMission.cropsGrew++;
            }
        }
        foreach (var pot in land)
        {
            pot.mesh = targetMesh;
        }
        if (!fromLoad)
        {
            constructible.ResourcesSubstraction();
        }
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
        general_UI.RenderConstruction(seedType, general_UI.orchardRender);
    }
    public void ButtonsStates(bool state)
    {
        if (currentState == "")
        {
            seedBtn.GetComponent<Button>().enabled = state;
            if (!state)
            {
                seedBtn.GetComponent<Button>().image.color = general_UI.lockColor;
            }
            else
            {
                seedBtn.GetComponent<Button>().image.color = general_UI.unlockColor;
            }
        }
        else
        {
            growBtn.GetComponent<Button>().enabled = state;
            if (!state)
            {
                growBtn.GetComponent<Button>().image.color = general_UI.lockColor;
            }
            else
            {
                growBtn.GetComponent<Button>().image.color = general_UI.unlockColor;
            }
        }
    }
    public object SaveState()
    {
        return new SaveData()
        {
            currentState = this.currentState,
            seedType = this.seedType
        };
    }
    public void LoadState(object state)
    {
        var saveData = (SaveData)state;
        currentState = saveData.currentState;
        seedType = saveData.seedType;
        if (currentState == "Seeded")
        {
            PlantSeed(true);
        }
        else if (currentState == "Mini" && gameObject.tag != "Untagged")
        {
            Debug.Log("im right");
            currentState = "Seeded";
            GrowSeed(true);
        }
        else if (gameObject.tag == "Untagged")
        {
            GrowSeed(true);
        }
    }
    [Serializable]
    private struct SaveData
    {
        public string currentState, seedType;
    }
}
