using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionTrack : MonoBehaviour
{
    [SerializeField] GameObject[] stages;
    [SerializeField] GameObject door;
    GlobalVariables gv;
    SaveLoadSystem saveSystem;
    void Start()
    {
        gv = GameObject.FindObjectOfType<GlobalVariables>();
        saveSystem = GameObject.FindObjectOfType<SaveLoadSystem>();
        PlaceStage(false);
    }
    public void TransportMissionTraget()
    {
        gv.currentMissionStage++;
        Debug.Log(gv.currentMissionStage);
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
            transform.position += transform.right * 3;
            transform.position += transform.up * 2;
        }
        if (save)
        {
            saveSystem.Save();
        }
    }
}
