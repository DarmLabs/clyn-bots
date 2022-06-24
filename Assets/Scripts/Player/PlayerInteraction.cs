using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField]
    #region Imports
    public GameObject SaveLoadGameObject;
    SaveLoadSystem saveSystem;
    public GameObject GlobalVariables;
    GlobalVariables globalVariables;
    PlayerAnimations playerAnim;
    public GameObject UIManager;
    Player_UI player_UI;
    General_UI general_UI;
    [Space(10)]
    //**************************************************
    #endregion
    float timePressed;
    bool facingTrash;
    bool facingArcade;
    string inDoor;
    int maxBagSpace = 50, itemsInBag;
    public GameObject BasuralPoint, LobbyPoint;
    public GameObject sun;
    GameObject currentTrashPile;
    void Start()
    {
        saveSystem = SaveLoadGameObject.GetComponent<SaveLoadSystem>();
        globalVariables = GlobalVariables.GetComponent<GlobalVariables>();
        playerAnim = GetComponent<PlayerAnimations>();
        player_UI = UIManager.GetComponent<Player_UI>();
        general_UI = UIManager.GetComponent<General_UI>();
    }
    void Update()
    {
        Controls();
    }
    void Aspire(){
        timePressed +=  Time.deltaTime;
        playerAnim.Aspire(true);
        if(timePressed > 2){
            globalVariables.noRecTrash += Random.Range(1, 3);
            globalVariables.organicTrash += Random.Range(1, 3);
            globalVariables.recTrash += Random.Range(1,3);
            currentTrashPile.GetComponent<TrashPile>().RecudeHeight();
            timePressed = 0;
            itemsInBag = globalVariables.recTrash + globalVariables.organicTrash + globalVariables.noRecTrash;
            saveSystem.Save();
        }
    }
    void Controls(){
        if(Input.GetKey(KeyCode.Space) && facingTrash && currentTrashPile.activeSelf && itemsInBag <= maxBagSpace){
            Aspire();
        }
        else if(currentTrashPile != null && !currentTrashPile.activeSelf){
            playerAnim.Aspire(false);
            OnObjectExit();
        }
        if(Input.GetKeyDown(KeyCode.E)){
            if(inDoor != null){
                ChangeStage();
                MovmentState(false);
            }
            if(facingArcade){
                general_UI.MinigamePanelSwitcher(true);
            }
        }
    }
    void ChangeStage(){
        player_UI.fadeState = 1;
        StartCoroutine(WaitInDoor(1));
    }
    void MovmentState(bool state){
        GetComponent<PlayersMovement>().enabled = state;
    }
    IEnumerator WaitInDoor(float secs){
        yield return new WaitForSeconds(secs);
        if(inDoor != ""){
                switch (inDoor)
                {
                    case "ToBasural":
                        transform.position = BasuralPoint.transform.position;
                        sun.SetActive(true);
                        break;
                    case "ToLobby":
                        transform.position = LobbyPoint.transform.position;
                        sun.SetActive(false);
                        break;
                }
        }
        player_UI.fadeState = 2;
        MovmentState(true);
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
    }
    void OnTriggerExit(Collider other)
    {
        if(other.tag == "trash"){
            facingTrash = false;
            currentTrashPile = null;
        }
        if(other.tag == "door"){
            inDoor = null;
        }
        if(other.tag == "arcade"){
            facingArcade = false;
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
}
