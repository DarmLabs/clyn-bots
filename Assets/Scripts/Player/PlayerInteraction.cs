using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class PlayerInteraction : MonoBehaviour, ISaveable
{
    [SerializeField]
    #region Imports
    public GameObject SaveLoadGameObject;
    SaveLoadSystem saveSystem;
    Construction_UI constructionUI_Script;
    PlayerAnimations playerAnim;

    public GameObject UIManager;
    Player_UI player_UI;
    [Space(10)]
    #endregion
    public int noRecTrash, organicTrash, recTrash;
    float timePressed;
    bool facingTrash;
    string inDoor;
    public GameObject BasuralPoint, LobbyPoint;
    GameObject construction;
    GameObject currentBuilding;
    GameObject currentTrashPile;
    void Start()
    {
        saveSystem = SaveLoadGameObject.GetComponent<SaveLoadSystem>();
        constructionUI_Script = UIManager.GetComponent<Construction_UI>();
        playerAnim = GetComponent<PlayerAnimations>();
        player_UI = UIManager.GetComponent<Player_UI>();
    }
    void Update()
    {
        Controls();
    }
    void Aspire(){
        timePressed +=  Time.deltaTime;
        playerAnim.Aspire(true);
        if(timePressed > 2){
            noRecTrash += Random.Range(0, 4);
            organicTrash += Random.Range(0, 4);
            recTrash += Random.Range(0,4);
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
                if(reqOrg <= organicTrash && reqRec <= recTrash){
                    organicTrash -= reqOrg;
                    recTrash -= reqRec;
                    currentBuilding.GetComponent<Construction>().Constructed();
                    playerAnim.Interaction(true);
                    saveSystem.Save();
                }
                else{
                    Debug.Log("No tienes suficientes recursos");
                }
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
                        break;
                    case "ToLobby":
                        transform.position = LobbyPoint.transform.position;
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

    public object SaveState(){
        return new SaveData(){
            noRecTrash = this.noRecTrash,
            organicTrash = this.organicTrash,
            recTrash = this.recTrash
        };
    }
    public void LoadState(object state){
        var saveData = (SaveData)state;
        noRecTrash = saveData.noRecTrash;
        organicTrash = saveData.organicTrash;
        recTrash = saveData.recTrash;
    }
    [Serializable]
    private struct SaveData{
        public int noRecTrash, organicTrash, recTrash;
    }
}
