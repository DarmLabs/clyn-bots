using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructibleObj : MonoBehaviour
{
    GameObject SaveData;
    GlobalVariables globalVariables;
    GameObject UIManager;
    General_UI general_UI;
    public int[] reqResources = new int[5];
    #region Index = Resource
    //[0] = Vidrio
    //[1] = Plastico
    //[2] = Carton
    //[3] = Metal
    //[4] = Compost
    #endregion
    public GameObject target;
    public GameObject building;
    void Start()
    {
        SaveData = GameObject.Find("GlobalVariables");
        globalVariables = SaveData.GetComponent<GlobalVariables>();
        UIManager = GameObject.Find("CanvasOverlay");
        general_UI = UIManager.GetComponent<General_UI>();
    }
    public void ShowResources(){
        string req = "";
        for (int i = 0; i < reqResources.Length; i++)
        {
            if(reqResources[i] > 0){
                switch (i)
                {
                    case 0:
                        req = "Vidrio x" + reqResources[i];  
                        break;
                    case 1:
                        req += "\nPlastico x" + reqResources[i];  
                        break;
                    case 2:
                        req += "\nCarton x" + reqResources[i];  
                        break;
                    case 3:
                        req += "\nMetal x" + reqResources[i];  
                        break;
                    case 4:
                        req += "\nVidrio x" + reqResources[i];  
                        break;
                }
            }
        }
        //general_UI.BuildingConstructionMenu(building.name, )
    }
    public void BuildObject(){
        building.transform.position = target.transform.position;
    }
}
