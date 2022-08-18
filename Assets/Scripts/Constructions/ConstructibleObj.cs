using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ConstructibleObj : MonoBehaviour
{
    GlobalVariables globalVariables;
    GameObject UIManager;
    General_UI general_UI;
    GameObject cinematicCamera;
    public bool seeded;

    [Header("[0] = Vidrio\n[1] = Plastico\n[2] = Carton\n[3] = Metal\n[4] = Compost")]
    public int[] reqResources = new int[5];
    public GameObject target;
    public GameObject targetOptional;
    public GameObject building;
    bool reqMeet = true;
    void Start()
    {
        cinematicCamera = GameObject.Find("CinematicCamera");
        globalVariables = GameObject.FindObjectOfType<GlobalVariables>().GetComponent<GlobalVariables>();
        UIManager = GameObject.Find("CanvasOverlay");
        general_UI = UIManager.GetComponent<General_UI>();
    }
    public void ShowResources()
    {
        string title = "";
        string[] req = new string[5];
        reqMeet = true;
        for (int i = 0; i < reqResources.Length; i++)
        {
            req[i] = reqResources[i].ToString();
            switch (i)
            {
                case 0:
                    if (globalVariables.vidrioRefinado < reqResources[i])
                    {
                        reqMeet = false;
                    }
                    break;
                case 1:
                    if (globalVariables.plasticoRefinado < reqResources[i])
                    {
                        reqMeet = false;
                    }
                    break;
                case 2:
                    if (globalVariables.cartonRefinado < reqResources[i])
                    {
                        reqMeet = false;
                    }
                    break;
                case 3:
                    if (globalVariables.metalRefinado < reqResources[i])
                    {
                        reqMeet = false;
                    }
                    break;
                case 4:
                    if (globalVariables.compostRefinado < reqResources[i])
                    {
                        reqMeet = false;
                    }
                    break;
            }
        }
        if (GetComponent<Seed>() != null)
        {
            switch (GetComponent<Seed>().currentState)
            {
                case "":
                    general_UI.EnabledSection("Construir");
                    if (reqMeet)
                    {
                        general_UI.ConstructionButtonState(true);
                    }
                    else
                    {
                        general_UI.ConstructionButtonState(false);
                    }
                    title = "Construyendo " + building.name;
                    general_UI.BuildingConstructionMenu(title, req, "");
                    break;
                case "Sembrar":
                    general_UI.EnabledSection("Sembrar");
                    if (reqMeet)
                    {
                        general_UI.SeedButtonsState(true);
                    }
                    else
                    {
                        general_UI.SeedButtonsState(false);
                    }
                    title = "Plantando";
                    general_UI.BuildingConstructionMenu(title, req, "");
                    break;
                case "Mejorar":
                    general_UI.EnabledSection("Mejorar");
                    if (reqMeet)
                    {
                        general_UI.UpgradeButtonState(true);
                    }
                    else
                    {
                        general_UI.UpgradeButtonState(false);
                    }
                    title = "Mejorando " + GetComponent<Seed>().building.name;
                    general_UI.BuildingConstructionMenu(title, req, GetComponent<Seed>().building.name);
                    break;
            }
        }
        else
        {
            general_UI.EnabledSection("Construir");
            if (reqMeet)
            {
                general_UI.ConstructionButtonState(true);
            }
            else
            {
                general_UI.ConstructionButtonState(false);
            }
            title = "Construyendo " + building.name;
            general_UI.BuildingConstructionMenu(title, req, building.name);
        }
    }
    public void ResourcesSubstraction()
    {
        globalVariables.vidrioRefinado -= reqResources[0];
        globalVariables.plasticoRefinado -= reqResources[1];
        globalVariables.cartonRefinado -= reqResources[2];
        globalVariables.metalRefinado -= reqResources[3];
        globalVariables.compostRefinado -= reqResources[4];
    }
    public void BuildObject()
    {
        building.transform.position = target.transform.position;
        target.transform.position = new Vector3(target.transform.position.x, target.transform.position.y - 10000, target.transform.position.z);
        building.GetComponent<SavePosition>().PositionUpdated();
        target.GetComponent<SavePosition>().PositionUpdated();
        if (targetOptional != null)
        {
            targetOptional.transform.position = target.transform.position;
            targetOptional.GetComponent<SavePosition>().PositionUpdated();
            GetComponent<Seed>().currentState = "Sembrar";
        }
        else
        {
            if (building.name == "Bomba de Agua")
            {
                gameObject.tag = "Pipes";
                general_UI.InteractionCloud(true);
            }
            else
            {
                gameObject.tag = "Untagged";
            }
        }
        PlayCinematic(building);
        ResourcesSubstraction();
    }
    public void PlayCinematic(GameObject target)
    {
        cinematicCamera.GetComponent<OffsetCinematicCamera>().SetTarget(target);
    }
}
