using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aspiradora : MonoBehaviour
{
    public General_UI general_UI;    
    [Header("Dirty Area")]
    [SerializeField] Transform topBounds;
    [SerializeField] Transform bottomBounds;

    [Header("Trash Settings")]
    [SerializeField] Transform Trash;
    [SerializeField] float smoothMotion = 1f; //movimiento suave
    [SerializeField] float TrashTimeRandomizer = 1f; //frecuencia de movimiento
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
    //private GameObject globalaux;
    public GlobalVariables gv; 
    //private GameObject saveaux;
    public SaveLoadSystem saveSystem;
    public bool primeraVez = false;
    public bool activoJuego = false;
    
    void Start()
    {
        catchProgress = 0.1f;
        VacuumPosition = 0.5f;
        TrashPosition = 0.5f;
        primeraVez = false;
        activoJuego = false;
        //globalaux = GameObject.Find("GlobalVariables");
        //gv = globalaux.GetComponent<GlobalVariables>();
        //saveaux = GameObject.Find ("SaveLoadSystem");
        //saveSystem = saveaux.GetComponent<SaveLoadSystem>();
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
            if(activoJuego)
            {
                catchProgress += VacuumPower*Time.deltaTime;
                Debug.Log("PrimeraVez: "+primeraVez);
            }
            
            if(catchProgress>=1)
            {
                general_UI.MinigameAspireSwitcher(false);
                if (!primeraVez)
                {
                    Debug.Log("ASPIRASTE TODOOOO");
                    //Lógica aspiradora normal
                    gv.noRecTrash += 5;
                    gv.recTrash += 5;
                    gv.organicTrash += 5;
                    primeraVez = true;
                    activoJuego = false;
                    catchProgress = 0.1f;
                    VacuumPosition = 0;
                    TrashPosition = 0.5f;
                    VacuumPosition = 0.5f;
                    general_UI.playerInteraction.ReduceTrashPile();
                    general_UI.playerInteraction.OnObjectExit();
                    saveSystem.Save();
                    general_UI.playerInteraction.BagPercentage();
                }                
            }                        
        }
        else
        {
            if (activoJuego)
            {
                catchProgress -=  progressBarDecay*Time.deltaTime;
                if(catchProgress<=0)
                {
                    general_UI.MinigameAspireSwitcher(false);
                    primeraVez = true;
                    activoJuego = false;
                    catchProgress = 0.1f;
                    VacuumPosition = 0;
                    TrashPosition = 0.5f;
                    VacuumPosition = 0.5f;
                    saveSystem.Save();
                    Debug.Log("La aspiradora exploto");
                }
            }
            
        }     
        catchProgress=Mathf.Clamp(catchProgress,0,1);
        
        
    }

    private void MoveVacuum()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            VacuumPullVelocity += VacuumSpeed * Time.deltaTime;
            activoJuego = true;
        }
        if(activoJuego)
        {
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
        }
        
        VacuumPosition = Mathf.Clamp(VacuumPosition,VacuumSize/2,1-VacuumSize/2);
        Vacuum.position = Vector3.Lerp(bottomBounds.position, topBounds.position, VacuumPosition);
    }

    private void MoveTrash()
    {
        if (activoJuego)
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
}
