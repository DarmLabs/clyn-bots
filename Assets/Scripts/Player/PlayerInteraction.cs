using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    #region Imports & Required Objects
    [Header("Imports & Required Objects")]
    public SaveLoadSystem saveSystem;
    public GlobalVariables globalVariables;
    public PlayerAnimations playerAnim;
    public PlayerMovement playerMovement;
    public Player_UI player_UI;
    public General_UI general_UI;
    public GameObject BasuralPoint, LobbyPointB, LobbyPointGZ, GreenZonePoint;
    public GameObject basural, central, greenZone;
    public GameObject currentTrashPile;
    public GameObject targetConstruction;
    public GameObject targetRecycler;
    #endregion
    float timePressed;
    public bool canInteract;
    public bool interactionHappen;
    bool facingArcade;
    bool changingStage;
    public bool isAspiring;
    public bool minigameAsipire;
    public string inDoor;
    int maxBagSpace = 30, itemsInBag;
    public float bagPercentage;
    void Start()
    {
        playerAnim = GetComponent<PlayerAnimations>();
        playerMovement = GetComponent<PlayerMovement>();
        IntializeFunctions();
    }
    void IntializeFunctions()
    {
        OnResume();
        Generador.contadorBasura = 0;
        BagPercentage();
    }
    void Update()
    {
        Controls();
    }
    void Controls()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            general_UI.ExitPanelSwitcher(true);
            general_UI.MainPanelSwitcher(false);
            general_UI.ConstructionPanelSwitcher(false);
            general_UI.MinigamePanelSwitcher(false);
            general_UI.MinigameAspireSwitcher(false);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(targetRecycler != null){
                speakWithRecycler();
            }
            if (inDoor != "")
            {
                changingStage = true;
                interactionHappen = true;
            }
            if (facingArcade)
            {
                general_UI.MinigamePanelSwitcher(true);
            }
            if (targetConstruction != null && targetConstruction.tag != "Untagged")
            {
                general_UI.ConstructionPanelSwitcher(true);
                targetConstruction.GetComponent<ConstructibleObj>().ShowResources();
                interactionHappen = true;
            }
            if (interactionHappen)
            {
                playerAnim.Interaction(true);
            }
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            int aux;
            aux = (maxBagSpace - itemsInBag) / 3;
            globalVariables.recTrash += aux;
            globalVariables.organicTrash += aux;
            globalVariables.noRecTrash += aux;
            BagPercentage();
            globalVariables.compostRefinado += 5;
            globalVariables.metalRefinado += 5;
            globalVariables.vidrioRefinado += 5;
            globalVariables.plasticoRefinado += 5;
            globalVariables.cartonRefinado += 5;
            saveSystem.Save();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            globalVariables.recTrash = 0;
            globalVariables.organicTrash = 0;
            globalVariables.noRecTrash = 0;
            BagPercentage();
            saveSystem.Save();
        }
        /*if(Input.GetKeyDown(KeyCode.Q)){
            playerAnim.Celebrate();
        }*/
    }
    public void ReduceTrashPile()
    {
        currentTrashPile.SetActive(false);
        currentTrashPile = null;
    }
    public void BagPercentage()
    {
        itemsInBag = globalVariables.vidrioTrash + globalVariables.cartonTrash + globalVariables.metalTrash + globalVariables.plasticoTrash + globalVariables.organicTrash + globalVariables.noRecTrash;
        bagPercentage = (itemsInBag * 100) / 30;
        player_UI.DisplayBagPercentage();
    }
    public void ChangeStage()
    {
        player_UI.FadePanel.SetActive(true);
        player_UI.fadeState = 1;
        StartCoroutine(WaitInDoor(1));
    }
    public void MovmentState(bool state)
    {
        playerMovement.enabled = state;
    }
    void StageChange(GameObject stageOn, GameObject stageOff)
    {
        stageOn.SetActive(true);
        stageOff.SetActive(false);
    }
    IEnumerator WaitInDoor(float secs)
    {
        yield return new WaitForSeconds(secs);
        switch (inDoor)
        {
            case "ToBasural":
                transform.position = BasuralPoint.transform.position;
                StageChange(basural, central);
                general_UI.MinimapSwitcher(true);
                break;
            case "ToLobbyFB":
                transform.position = LobbyPointB.transform.position;
                StageChange(central, basural);
                general_UI.MinimapSwitcher(false);
                break;
            case "ToLobbyFGZ":
                transform.position = LobbyPointGZ.transform.position;
                StageChange(central, greenZone);
                general_UI.MinimapSwitcher(false);
                break;
            case "ToGreenZone":
                transform.position = GreenZonePoint.transform.position;
                StageChange(greenZone, central);
                break;
        }
        player_UI.fadeState = 2;
        MovmentState(true);
        changingStage = false;
        inDoor = "";
        InteractionEnds();
    }
    public void BuildObject()
    {
        targetConstruction.GetComponent<ConstructibleObj>().BuildObject();
        targetConstruction.GetComponent<SaveTag>().UpdateTag();
    }
    public void UpgradeObject()
    {
        targetConstruction.GetComponent<Seed>().GrowSeed();
    }
    public void speakWithRecycler(){
        targetRecycler.GetComponent<RecyclerNPC>().Speak();
    }
    public void EnterDetectObject(GameObject targetObject)
    {
        if (targetObject.tag == "trash")
        {
            currentTrashPile = targetObject.gameObject;
            canInteract = true;
        }
        if (targetObject.tag == "door")
        {
            inDoor = targetObject.name;
            canInteract = true;
        }
        if (targetObject.tag == "arcade")
        {
            facingArcade = true;
            canInteract = true;
        }
        if (targetObject.tag == "construction")
        {
            targetConstruction = targetObject.gameObject;
            canInteract = true;
        }
        if (targetObject.tag == "Recycler")
        {
            targetRecycler = targetObject.gameObject;
        }
        general_UI.InteractionCloud(canInteract);
    }
    public void ExitDetectObject(GameObject targetObject)
    {
        if (targetObject.tag == "trash")
        {
            currentTrashPile = null;
            canInteract = false;
        }
        if (targetObject.tag == "door")
        {
            if (!changingStage)
            {
                inDoor = "";
                canInteract = false;
            }
        }
        if (targetObject.tag == "arcade")
        {
            facingArcade = false;
            canInteract = false;
        }
        if (targetObject.tag == "construction")
        {
            targetConstruction = null;
            canInteract = false;
        }
        if (targetObject.tag == "Recycler")
        {
            targetRecycler = null;
        }
        general_UI.InteractionCloud(canInteract);
    }
    public void InteractionEnds()
    {
        interactionHappen = false;
    }
    public void OnPause()
    {
        Time.timeScale = 0;
    }
    public void OnResume()
    {
        Time.timeScale = 1;
    }
    public void SaveTransform()
    {
        GetComponent<SavePosition>().PositionUpdated();
        GetComponent<SavePosition>().RotationUpdated();
    }
}