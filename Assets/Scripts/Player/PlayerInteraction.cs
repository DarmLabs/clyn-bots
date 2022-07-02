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
    PlayerAnimations playerAnim;
    public GameObject UIManager;
    Player_UI player_UI;
    General_UI general_UI;
    public GameObject BasuralPoint, LobbyPointB, LobbyPointGZ, GreenZonePoint;
    public GameObject sun, spotCentral, spotGreenZone;
    GameObject currentTrashPile;
    GameObject targetConstruction;
    [Space(10)]
    //**************************************************
    #endregion
    float timePressed;
    bool facingTrash;
    bool facingArcade;
    bool changingStage;
    public string inDoor;
    int maxBagSpace = 50, itemsInBag;
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
        if(Input.GetKey(KeyCode.Space) && facingTrash && currentTrashPile.activeSelf && itemsInBag < maxBagSpace){
            Aspire();
        }
        else if((currentTrashPile != null && !currentTrashPile.activeSelf)){
            playerAnim.Aspire(false);
            OnObjectExit();
        }
        else{
            playerAnim.Aspire(false);
            timePressed = 0;
        }
        if(Input.GetKeyDown(KeyCode.E)){
            bool interactionHappen = false;
            if(inDoor != ""){
                changingStage = true;
                interactionHappen = true;
            }
            if(facingArcade){
                general_UI.MinigamePanelSwitcher(true);
                interactionHappen = true;
                SaveTransform();
            }
            if(targetConstruction != null){
                general_UI.ConstructionPanelSwitcher(true);
                targetConstruction.GetComponent<ConstructibleObj>().ShowResources();
                interactionHappen = true;
            }
            if(interactionHappen){
                playerAnim.Interaction(true);
            }
        }
        if(Input.GetKeyDown(KeyCode.O)){
            int aux;
            aux = (maxBagSpace - itemsInBag) / 3;
            globalVariables.recTrash += aux;
            globalVariables.organicTrash += aux;
            globalVariables.noRecTrash += aux;
            BagPercentage();
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
     void Aspire(){
        MovmentState(false);
        timePressed +=  Time.deltaTime;
        playerAnim.Aspire(true);
        if(timePressed > 2){
            if(itemsInBag < 44){
                RandomNRT(1,3);
                RandomRT(1,3);
                RandomOT(1,3);
            }else{
                if(itemsInBag < maxBagSpace){
                    RandomNRT(1,2);
                }
                if(itemsInBag < maxBagSpace){
                    RandomRT(1,2);
                }
                if(itemsInBag < maxBagSpace){
                    RandomOT(1,2);
                }
            }
            currentTrashPile.GetComponent<TrashPile>().RecudeHeight();
            timePressed = 0;
            saveSystem.Save();
        }
    }
    void RandomNRT(int inf,int ext){
        globalVariables.noRecTrash += Random.Range(inf, ext);
        BagPercentage();
    }
    void RandomRT(int inf,int ext){
        globalVariables.recTrash += Random.Range(inf, ext);
        BagPercentage();
    }
    void RandomOT(int inf,int ext){
        globalVariables.organicTrash += Random.Range(inf, ext);
        BagPercentage();
    }
    void BagPercentage(){
        itemsInBag = globalVariables.recTrash + globalVariables.organicTrash + globalVariables.noRecTrash;
        bagPercentage = (itemsInBag *100)/ 50;
    }
    public void ChangeStage(){
        player_UI.fadeState = 1;
        StartCoroutine(WaitInDoor(1));
    }
    public void MovmentState(bool state){
        GetComponent<PlayersMovement>().enabled = state;
    }
    void LightChange(GameObject lightOn, GameObject lightOff){
        lightOn.SetActive(true);
        lightOff.SetActive(false);
    }
    IEnumerator WaitInDoor(float secs){
        yield return new WaitForSeconds(secs);
            switch (inDoor)
            {
                case "ToBasural":
                    transform.position = BasuralPoint.transform.position;
                    LightChange(sun, spotCentral);
                    //Camera.main.GetComponent<Camera>()
                    break;
                case "ToLobbyFB":
                    transform.position = LobbyPointB.transform.position;
                    LightChange(spotCentral, sun);
                    break;
                case "ToLobbyFGZ":
                    transform.position = LobbyPointGZ.transform.position;
                    LightChange(spotCentral, spotGreenZone);
                    break;
                case "ToGreenZone":
                    transform.position = GreenZonePoint.transform.position;
                    LightChange(spotGreenZone, spotCentral);
                    break;
                }
        player_UI.fadeState = 2;
        MovmentState(true);
        inDoor = "";
    }
    public void BuildObject(){
        targetConstruction.GetComponent<ConstructibleObj>().BuildObject();
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "trash"){
            facingTrash = true;
            currentTrashPile = other.gameObject;
        }
        if(other.tag == "door"){
            inDoor = other.name;
        }
        if(other.tag == "arcade"){
            facingArcade = true;
        }
        if(other.tag == "construction"){
            targetConstruction = other.gameObject;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.tag == "trash"){
            facingTrash = false;
            currentTrashPile = null;
        }
        if(other.tag == "door"){
            if(!changingStage){
                inDoor = "";
            }
        }
        if(other.tag == "arcade"){
            facingArcade = false;
        }
        if(other.tag == "construction"){
            targetConstruction = null;
        }
    }
    void OnObjectExit()
    {
        currentTrashPile = null;
        facingTrash = false;
    }
    void OnPause(){
        Time.timeScale = 0;
    }
    void OnResume(){
        Time.timeScale = 1;
    }
    void SaveTransform(){
        GetComponent<SavePosition>().PositionUpdated();
        GetComponent<SavePosition>().RotationUpdated();
        Debug.Log("guardo");
    }
}
