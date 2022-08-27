using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionTrack : MonoBehaviour
{
    public GameObject[] stages;
    [SerializeField] GameObject door;
    GlobalVariables gv;
    SaveLoadSystem saveSystem;
    void Start()
    {
        gv = GameObject.FindObjectOfType<GlobalVariables>();
        saveSystem = GameObject.FindObjectOfType<SaveLoadSystem>();
        CheckSpecialStages();
    }
    void CheckSpecialStages()
    {
        if (gv.currentMissionStage == 16)
        {
            Destroy(gameObject);
        }
        else
        {
            PlaceStage(false);
        }
        if (gv.memoriaAccesible && gv.currentMissionStage == 5)
        {
            gv.currentMissionStage++;
        }
        if (gv.metalRefinado + gv.cartonRefinado + gv.vidrioRefinado + gv.compostRefinado + gv.plasticoRefinado > 0 && gv.currentMissionStage == 7)
        {
            gv.currentMissionStage++;
        }
    }
    public void TransportMissionTraget()
    {
        gv.currentMissionStage++;
        if (gv.currentMissionStage == 16)
        {
            Destroy(gameObject);
        }
        if (stages[gv.currentMissionStage] != null && stages[gv.currentMissionStage].GetComponent<RecyclerNPC>() != null)
        {
            stages[gv.currentMissionStage].GetComponent<RecyclerNPC>().missionTarget = true;
        }
        PlaceStage(true);
    }
    public void PlaceStage(bool save)
    {
        if (stages[gv.currentMissionStage] != null)
        {
            transform.position = stages[gv.currentMissionStage].transform.position;
            transform.position += transform.up * 4f;
        }
        else
        {
            transform.position = door.transform.position;
            transform.position += transform.up * 4;
        }
        if (save)
        {
            saveSystem.Save();
        }
        else if (!save && stages[gv.currentMissionStage] != null && stages[gv.currentMissionStage].GetComponent<RecyclerNPC>() != null)
        {
            stages[gv.currentMissionStage].GetComponent<RecyclerNPC>().missionTarget = true;
        }
    }
}
