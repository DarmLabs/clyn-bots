using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField]
    #region Imports & Required Objects
    public GameObject SaveLoadGameObject;
    SaveLoadSystem saveSystem;
    public GameObject GlobalVariables;
    public GlobalVariables globalVariables;
    public PlayerAnimations playerAnim;
    public GameObject UIManager;
    Player_UI player_UI;
    General_UI general_UI;
    public GameObject BasuralPoint, LobbyPointB, LobbyPointGZ, GreenZonePoint;
    public GameObject basural, central, greenZone;
    GameObject currentTrashPile;
    public GameObject targetConstruction;
    [Space(10)]
    //**************************************************
    #endregion
    float timePressed;
    bool facingTrash;
    bool onInteraction;
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
        saveSystem = SaveLoadGameObject.GetComponent<SaveLoadSystem>();
        globalVariables = GlobalVariables.GetComponent<GlobalVariables>();
        playerAnim = GetComponent<PlayerAnimations>();
        player_UI = UIManager.GetComponent<Player_UI>();
        general_UI = UIManager.GetComponent<General_UI>();
        IntializeFunctions();
    }
    void IntializeFunctions(){
        BagPercentage();
    }
    void Update()
    {
        Controls();
    }
    void Controls(){
        if(Input.GetKeyDown(KeyCode.E)){
            interactionHappen = false;
            if(facingTrash && currentTrashPile.activeSelf && itemsInBag < maxBagSpace){
                playerAnim.Aspire(true);
            }
            if(inDoor != ""){
                changingStage = true;
                interactionHappen = true;
            }
            if(facingArcade){
                general_UI.MinigamePanelSwitcher(true);
            }
            if(targetConstruction != null && targetConstruction.tag != "Untagged"){
                general_UI.ConstructionPanelSwitcher(true);
                targetConstruction.GetComponent<ConstructibleObj>().ShowResources();
                interactionHappen = true;
            }
            if(interactionHappen){
                playerAnim.Interaction(true);
                OnObjectExit();
            }
        }
        if(Input.GetKeyDown(KeyCode.O)){
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
        if(Input.GetKeyDown(KeyCode.P)){
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
    public void ReduceTrashPile(){
        currentTrashPile.SetActive(false);
        currentTrashPile = null;
        facingTrash = false;
    }
    public void BagPercentage(){
        itemsInBag = globalVariables.recTrash + globalVariables.organicTrash + globalVariables.noRecTrash;
        bagPercentage = (itemsInBag *100)/ 30;
    }
    public void ChangeStage(){
        player_UI.fadeState = 1;
        StartCoroutine(WaitInDoor(1));
    }
    public void MovmentState(bool state){
        GetComponent<PlayersMovement>().enabled = state;
    }
    void StageChange(GameObject stageOn, GameObject stageOff){
        stageOn.SetActive(true);
        stageOff.SetActive(false);
    }
    IEnumerator WaitInDoor(float secs){
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
    }
    public void BuildObject(){
        targetConstruction.GetComponent<ConstructibleObj>().BuildObject();
        targetConstruction.GetComponent<SaveTag>().UpdateTag();
    }
    public void UpgradeObject(){
        targetConstruction.GetComponent<Seed>().GrowSeed();
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "trash"){
            facingTrash = true;
            currentTrashPile = other.gameObject;
            onInteraction = true;
        }
        if(other.tag == "door"){
            inDoor = other.name;
            onInteraction = true;
        }
        if(other.tag == "arcade"){
            facingArcade = true;
            onInteraction = true;
        }
        if(other.tag == "construction"){
            targetConstruction = other.gameObject;
            onInteraction = true;
        }
        general_UI.InteractionCloud(onInteraction);
    }
    void OnTriggerExit(Collider other)
    {
        if(other.tag == "trash"){
            facingTrash = false;
            currentTrashPile = null;
            onInteraction = false;
        }
        if(other.tag == "door"){
            if(!changingStage){
                inDoor = "";
                onInteraction = false;
            }
        }
        if(other.tag == "arcade"){
            facingArcade = false;
            onInteraction = false;
        }
        if(other.tag == "construction"){
            targetConstruction = null;
            onInteraction = false;
        }
        general_UI.InteractionCloud(onInteraction);
    }
    public void OnObjectExit(){
        onInteraction = false;
        if(currentTrashPile != null && !currentTrashPile.activeSelf ){
            facingTrash = false;
            currentTrashPile = null;
        }
        general_UI.InteractionCloud(onInteraction);
    }
    void OnPause(){
        Time.timeScale = 0;
    }
    void OnResume(){
        Time.timeScale = 1;
    }
    public void SaveTransform(){
        GetComponent<SavePosition>().PositionUpdated();
        GetComponent<SavePosition>().RotationUpdated();
    }
}
