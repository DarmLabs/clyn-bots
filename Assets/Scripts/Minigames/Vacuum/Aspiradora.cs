using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aspiradora : MonoBehaviour
{
    [Header("Dirty Area")]
    [SerializeField] Transform topBounds;
    [SerializeField] Transform bottomBounds;

    [Header("Trash Settings")]
    [SerializeField] Transform Trash;
    [SerializeField] float smoothMotion = 3f;
    [SerializeField] float TrashTimeRandomizer = 3f;
    float TrashPosition;
    float TrashSpeed;
    float TrashTimer;
    float TrashTargetPosition;

    [Header("Vacuum Settings")]
    [SerializeField] Transform Vacuum;
    [SerializeField] float VacuumSize = 0.18f;
    [SerializeField] float VacuumSpeed = 0.1f;
    [SerializeField] float VacuumGravity = 0.05f;
    float VacuumPosition;
    float VacuumPullVelocity;

    [Header("Progress Bar Settings")]
    [SerializeField] Transform progressBarContainer;
    [SerializeField] float VacuumPower;
    [SerializeField] float progressBarDecay;
    float catchProgress;
    
    void Start()
    {
        catchProgress = 0.02f;
    }

    void FixedUpdate()
    {
        MoveTrash();
        MoveVacuum();
        CheckProgress();
    }

    private void CheckProgress()
    {
        Vector3 progressBarScale = progressBarContainer.localScale;
        progressBarScale.y = catchProgress;
        progressBarContainer.localScale = progressBarScale;

        float min = VacuumPosition-VacuumSize/2;
        float max = VacuumPosition+VacuumSize/2;

        if(min<TrashPosition && TrashPosition<max)
        {
            catchProgress += VacuumPower*Time.deltaTime;
            if(catchProgress>=1)
            {
                Debug.Log("ASPIRASTE TODOOOO");
            }
            if(catchProgress<1)
            {
                catchProgress-= progressBarDecay*Time.deltaTime;
                if(catchProgress<=0)
                {
                    Debug.Log("La aspiradora exploto");
                }
            }
            catchProgress=Mathf.Clamp(catchProgress,0,1);
        }
    }

    private void MoveVacuum()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            VacuumPullVelocity += VacuumSpeed * Time.deltaTime;
        }
        VacuumPullVelocity -= VacuumGravity * Time.deltaTime;        
        VacuumPosition += VacuumPullVelocity;

        if(VacuumPosition - VacuumSize/2 <= 0 && VacuumPullVelocity<0)
        {
            VacuumPullVelocity = 0;
        }

        if(VacuumPosition + VacuumSize/2 >= 1 && VacuumPullVelocity>0)
        {
            VacuumPullVelocity = 0;
        }
        VacuumPosition = Mathf.Clamp(VacuumPosition,VacuumSize/2,1-VacuumSize/2);
        Vacuum.position = Vector3.Lerp(bottomBounds.position, topBounds.position, VacuumPosition);
    }

    private void MoveTrash()
    {
        TrashTimer -= Time.deltaTime;
        if(TrashTimer < 0)
        {
            TrashTimer = Random.value * TrashTimeRandomizer;
            TrashTargetPosition = Random.value;

        }
        TrashPosition = Mathf.SmoothDamp(TrashPosition,TrashTargetPosition, ref TrashSpeed, smoothMotion);
        Trash.position = Vector3.Lerp(bottomBounds.position,topBounds.position,TrashPosition);
    }
}