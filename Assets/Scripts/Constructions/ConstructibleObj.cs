using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ConstructibleObj : MonoBehaviour
{
    GameObject saveData;
    GlobalVariables globalVariables;
    GameObject UIManager;
    General_UI general_UI;
    public bool seeded;

    [Header ("[0] = Vidrio\n[1] = Plastico\n[2] = Carton\n[3] = Metal\n[4] = Compost")]
    public int[] reqResources = new int[5];
    public GameObject target;
    public GameObject targetOptional;
    public GameObject building;
    bool reqMeet = true;
    void Start()
    {
        saveData = GameObject.Find("GlobalVariables");
        globalVariables = saveData.GetComponent<GlobalVariables>();
        UIManager = GameObject.Find("CanvasOverlay");
        general_UI = UIManager.GetComponent<General_UI>();
    }
    public void ShowResources(){
        string req = "";
        for (int i = 0; i < reqResources.Length; i++)
        {
            if(reqResources[i] > 0){
                reqMeet = true;
                switch (i)
                {
                    case 0:
                        req = "\nVidrio x" + reqResources[i]; 
                        if(globalVariables.vidrioRefinado < reqResources[i]){
                            reqMeet = false;
                        } 
                        break;
                    case 1:
                        req += "\nPlastico x" + reqResources[i];  
                        if(globalVariables.plasticoRefinado < reqResources[i]){
                            reqMeet = false;
                        }
                        break;
                    case 2:
                        req += "\nCarton x" + reqResources[i]; 
                        if(globalVariables.cartonRefinado < reqResources[i]){
                            reqMeet = false;
                        } 
                        break;
                    case 3:
                        req += "\nMetal x" + reqResources[i]; 
                        if(globalVariables.metalRefinado < reqResources[i]){
                            reqMeet = false;
                        } 
                        break;
                    case 4:
                        req += "\nCompost x" + reqResources[i];  
                        if(globalVariables.compostRefinado < reqResources[i]){
                            reqMeet = false;
                        }
                        break;
                }
            }
        }
        if(GetComponent<Seed>() != null){
            switch(GetComponent<Seed>().state){
                case "Construir":
                    general_UI.EnabledSection("Construir");
                    if(reqMeet){
                        general_UI.ConstructionButtonState(true);
                    }else{
                        general_UI.ConstructionButtonState(false);
                    }
                    break;
                case "Sembrar":
                    general_UI.EnabledSection("Sembrar");
                    if(reqMeet){
                        general_UI.SeedButtonsState(true);
                    }else{
                        general_UI.SeedButtonsState(false);
                    }
                    break;
                case "Mejorar":
                    general_UI.EnabledSection("Mejorar");
                    if(reqMeet){
                        general_UI.UpgradeButtonState(true);
                    }else{
                        general_UI.UpgradeButtonState(false);
                    }
                    break;
            }
        }else{
            general_UI.EnabledSection("Construir");
            if(reqMeet){
                general_UI.ConstructionButtonState(true);
            }else{
                general_UI.ConstructionButtonState(false);
            }
        }
        
        general_UI.BuildingConstructionMenu(building.name, req);
    }
    void ResourcesSubstraction(){
        globalVariables.vidrioRefinado -= reqResources[0];
        globalVariables.plasticoRefinado -= reqResources[1];
        globalVariables.cartonRefinado -= reqResources[2];
        globalVariables.metalRefinado -= reqResources[3];
        globalVariables.compostRefinado -= reqResources[4];
    }
    public void BuildObject(){
        building.transform.position = target.transform.position;
        target.transform.position = new Vector3(target.transform.position.x, target.transform.position.y - 100, target.transform.position.z);
        building.GetComponent<SavePosition>().PositionUpdated();
        target.GetComponent<SavePosition>().PositionUpdated();
        if(targetOptional != null){
            targetOptional.transform.position = target.transform.position;
            targetOptional.GetComponent<SavePosition>().PositionUpdated();
            GetComponent<Seed>().state = "Sembrar";
        }else{
            gameObject.tag = "Untagged";
        }
        ResourcesSubstraction();
    }
}
