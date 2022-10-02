using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ConstructibleObj : MonoBehaviour
{
    GlobalVariables globalVariables;
    GameObject UIManager;
    General_UI general_UI;
    SaveLoadSystem saveSystem;
    MainMission mainMission;
    GameObject cinematicCamera;
    Orchard orchard;
    [Header("[0] = Vidrio\n[1] = Plastico\n[2] = Carton\n[3] = Metal\n[4] = Compost")]
    public int[] reqResources = new int[5];
    public GameObject target;
    public GameObject building;
    bool reqMeet = true;
    [SerializeField] ConstructedDetector constructedDetector;
    void Start()
    {
        if (GetComponent<Orchard>() != null)
        {
            orchard = GetComponent<Orchard>();
        }
        cinematicCamera = GameObject.Find("CinematicCamera");
        globalVariables = GameObject.FindObjectOfType<GlobalVariables>().GetComponent<GlobalVariables>();
        UIManager = GameObject.Find("CanvasOverlay");
        general_UI = UIManager.GetComponent<General_UI>();
        mainMission = GameObject.FindObjectOfType<MainMission>();
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
        if (orchard == null)
        {
            title = building.name;
            general_UI.ConstructionButtonState(reqMeet);
            general_UI.BuildingConstructionMenu(title, req, building.name, false);
        }
        else
        {
            orchard.ButtonsStates(reqMeet);
            title = orchard.seedType;
            general_UI.BuildingConstructionMenu(title, req, orchard.seedType, true);
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
        saveSystem = general_UI.saveSystem;
        building.transform.position = target.transform.position;
        target.transform.position = new Vector3(target.transform.position.x, target.transform.position.y - 10000, target.transform.position.z);
        building.GetComponent<SavePosition>().PositionUpdated();
        target.GetComponent<SavePosition>().PositionUpdated();
        PlayCinematic(building);
        ResourcesSubstraction();
        if (mainMission.constructionsFinished < mainMission.maxConstructions)
        {
            mainMission.constructionsFinished++;
        }
    }
    public void PlayCinematic(GameObject target)
    {
        Vector3 offset = new Vector3();
        Vector3 lookAtOffset = new Vector3();
        switch (target.name)
        {
            case "Molino":
                offset = new Vector3(-25, 50, 25);
                lookAtOffset = new Vector3(0, 10, 0);
                break;
            case "Bomba de Agua":
                offset = new Vector3(-15, 20, -15);
                break;
            case "Panel Solar":
                offset = new Vector3(-25, 40, -25);
                break;
        }
        cinematicCamera.GetComponent<OffsetCinematicCamera>().SetTarget(target, offset, lookAtOffset);
    }
    public void PipesCheck()
    {
        if (building.name == "Bomba de Agua")
        {
            gameObject.tag = "Pipes";
            GetComponent<SaveTag>().UpdateTag();
            general_UI.playerInteraction.targetPipes = general_UI.playerInteraction.targetConstruction;
            general_UI.playerInteraction.targetConstruction = null;
            general_UI.InteractionCloud(true);
        }
        else
        {
            gameObject.tag = "Untagged";
            GetComponent<SaveTag>().UpdateTag();
        }
        if (constructedDetector != null)
        {
            constructedDetector.CheckConstruction();
        }
    }
}
