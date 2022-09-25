using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionTrack : MonoBehaviour
{
    public Transform[] trackStages;
    [SerializeField] GameObject trackArrow;
    [SerializeField] GameObject trackArrowIcon;
    GlobalVariables gv;
    [SerializeField] Transform door;
    bool requirements;
    void Awake()
    {
        gv = GameObject.FindObjectOfType<GlobalVariables>();
        CheckDestroy();
        CheckGuideStage();
    }
    public void CheckGuideStage()
    {
        if (trackStages[gv.currentMissionStage] != null)
        {
            Debug.Log("la");
            trackArrow.transform.position = trackStages[gv.currentMissionStage].position;
        }
        else
        {
            Debug.Log("?");
            trackArrow.transform.position = door.position;
        }
        CheckStageType();
    }
    public void CheckStageType()
    {
        RecyclerNPC recylcer = new RecyclerNPC();
        if (trackStages[gv.currentMissionStage] != null)
        {
            recylcer = trackStages[gv.currentMissionStage].parent.gameObject.GetComponent<RecyclerNPC>();
        }
        if (recylcer != null)
        {
            recylcer.missionTarget = true;
            if (gv.currentMissionStage == 5)
            {
                recylcer.gameObject.name = "RecyclerGuide_02_02";
            }
            if (gv.currentMissionStage == 9)
            {
                recylcer.gameObject.name = "RecyclerGuide_02_03";
            }
        }
    }
    public void NextStage()
    {
        CheckRequieredToPass();
        if (requirements)
        {
            gv.currentMissionStage++;
            CheckGuideStage();
            requirements = false;
        }
    }
    public void MaxBagMission()
    {
        if (gv.currentMissionStage == 3)
        {
            NextStage();
        }
    }
    public void CheckRequieredToPass()
    {
        if (gv.currentMissionStage == 4 && (gv.divisionCarton + gv.divisionMetal + gv.divisionNoRec + gv.divisionOrganic + gv.divisionPlastico + gv.divisionVidrio) == 0)
        {
            requirements = false;
        }
        else
        {
            requirements = true;
        }
        CheckDestroy();
    }
    public void CheckDestroy()
    {
        if (gv.currentMissionStage == 10)
        {
            Destroy(trackArrow.gameObject);
            Destroy(trackArrowIcon);
            Destroy(this.gameObject);
        }
    }
}
