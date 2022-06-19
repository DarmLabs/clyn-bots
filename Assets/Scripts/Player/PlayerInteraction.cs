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
    Construction_UI constructionUI_Script;
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
    public GameObject BasuralPoint, LobbyPoint;
    public GameObject sun;
    GameObject construction;
    GameObject currentBuilding;
    GameObject currentTrashPile;
    void Start()
    {
        saveSystem = SaveLoadGameObject.GetComponent<SaveLoadSystem>();
        globalVariables = GlobalVariables.GetComponent<GlobalVariables>();
        constructionUI_Script = UIManager.GetComponent<Construction_UI>();
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
            globalVariables.noRecTrash += Random.Range(0, 4);
            globalVariables.organicTrash += Random.Range(0, 4);
            globalVariables.recTrash += Random.Range(0,4);
            currentTrashPile.GetComponent<TrashPile>().RecudeHeight();
            timePressed = 0;
            saveSystem.Save();
        }
    }
    public void Construction(string constructionName){
        if(construction != null){
            Destroy(construction);
        }
        construction = Instantiate(Resources.Load("Constructions/"+constructionName), transform.position + (transform.forward * 4) + (transform.up), transform.rotation) as GameObject;
        construction.transform.parent = transform;
        constructionUI_Script.constructionPanel.SetActive(false);
        OnResume();
    }
    void Controls(){
        if(Input.GetKey(KeyCode.Space) && facingTrash && currentTrashPile.activeSelf){
            Aspire();
        }
        else if(currentTrashPile != null && !currentTrashPile.activeSelf){
            playerAnim.Aspire(false);
            OnObjectExit();
        }
        if(Input.GetKeyDown(KeyCode.Q)){
            playerAnim.Celebrate();
        }
        if(Input.GetKeyDown(KeyCode.Z)){
            if(!constructionUI_Script.constructionPanel.activeSelf){
                constructionUI_Script.constructionPanel.SetActive(true);
                OnPause();
            }
            else{
                constructionUI_Script.constructionPanel.SetActive(false);
                OnResume();
            }
            
        }
        if(Input.GetKeyDown(KeyCode.X)){
            if(construction != null){
                construction.transform.parent = null;
                construction = null;
            }
        }
        if(Input.GetKeyDown(KeyCode.E)){
            if(inDoor != null){
                ChangeStage();
                MovmentState(false);
            }
            if(currentBuilding != null){
                int reqOrg = currentBuilding.GetComponent<Construction>().reqOrg;
                int reqRec = currentBuilding.GetComponent<Construction>().reqRec;
                if(reqOrg <= globalVariables.organicTrash && reqRec <= globalVariables.recTrash){
                    globalVariables.organicTrash -= reqOrg;
                    globalVariables.recTrash -= reqRec;
                    currentBuilding.GetComponent<Construction>().Constructed();
                    playerAnim.Interaction(true);
                    saveSystem.Save();
                }
                else{
                    Debug.Log("No tienes suficientes recursos");
                }
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
        if(other.tag == "construction"){
            currentBuilding = other.gameObject;
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
        if(other.tag == "construction"){
            currentBuilding = null;
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
