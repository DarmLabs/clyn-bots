using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionTrack : MonoBehaviour
{
    public Transform[] trackStages;
    [SerializeField] GameObject trackArrow;
    [SerializeField] GameObject trackArrowIcon;
    public GlobalVariables gv;
    [SerializeField] Transform door;
    bool requirements;
    RecyclerNPC recycler;
    [SerializeField] GameObject[] guideRecycler;
    void Start()
    {
        gv = GameObject.FindObjectOfType<GlobalVariables>();
        CheckGuideStage();
        CheckDestroy();
        if (gv.currentMissionStage == 4)
        {
            NextStage();
        }
    }
    public void CheckGuideStage()
    {
        if (gv.currentMissionStage != 10)
        {
            if (trackStages[gv.currentMissionStage] != null)
            {
                trackArrow.transform.position = trackStages[gv.currentMissionStage].position;
            }
            else
            {
                trackArrow.transform.position = door.position;
            }
            CheckStageType();
        }
    }
    public void CheckStageType()
    {
        if (trackStages[gv.currentMissionStage] != null)
        {
            recycler = trackStages[gv.currentMissionStage].parent.gameObject.GetComponent<RecyclerNPC>();
        }
        if (recycler != null)
        {
            recycler.missionTarget = true;
            if (gv.currentMissionStage == 3)
            {
                recycler.gameObject.name = "RecyclerGuide_02_01";
            }
            if (gv.currentMissionStage == 5)
            {
                recycler.gameObject.name = "RecyclerGuide_02_02";
            }
            if (gv.currentMissionStage == 9)
            {
                recycler.gameObject.name = "RecyclerGuide_02_03";
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
            CheckDestroy();
        }
    }
    public void MaxBagMission()
    {
        if (gv.currentMissionStage == 2)
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
    }
    public void CheckDestroy()
    {
        if (gv.currentMissionStage > 1)
        {
            if (guideRecycler[0] != null)
            {
                guideRecycler[0].name = "RecyclerDone";
            }
        }
        if (gv.currentMissionStage == 10)
        {
            if (guideRecycler[1] != null)
            {
                guideRecycler[1].name = "RecyclerDone";
            }
            Destroy(trackArrowIcon);
            Destroy(trackArrow.gameObject);
            Destroy(this.gameObject);
        }
    }
}
