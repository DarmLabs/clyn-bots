using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField]
    #region Imports
    Construction_UI constructionUI_Script;
    PlayerAnimations playerAnim;

    public GameObject UIManager;
    Player_UI player_UI;
    [Space(10)]
    #endregion
    float timePressed;
    bool facingTrash;
    string inDoor;
    public GameObject BasuralPoint, LobbyPoint;
    public int noRecTrash, organicTrash, recTrash;
    GameObject construction;
    GameObject currentBuilding;
    void Start()
    {
        constructionUI_Script.GetComponent<Construction_UI>();
        playerAnim = GetComponent<PlayerAnimations>();
        player_UI = UIManager.GetComponent<Player_UI>();
    }
    void Update()
    {
        Controls();
        if(Input.GetKey(KeyCode.Space) && facingTrash){
            Aspire();
        }
        else{
            playerAnim.Aspire(false);
        }
    }
    void Aspire(){
        timePressed +=  Time.deltaTime;
        playerAnim.Aspire(true);
        if(timePressed > 2){
            noRecTrash += Random.Range(0, 4);
            organicTrash += Random.Range(0, 4);
            recTrash += Random.Range(0,4);
            timePressed = 0;
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
            ChangeStage();
            if(currentBuilding != null){
                int reqOrg = currentBuilding.GetComponent<Construction>().reqOrg;
                int reqRec = currentBuilding.GetComponent<Construction>().reqRec;
                if(reqOrg <= organicTrash && reqRec <= recTrash){
                    organicTrash -= reqOrg;
                    recTrash -= reqRec;
                    currentBuilding.GetComponent<Construction>().Constructed();
                    playerAnim.Interaction(true);
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
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "trash"){
            facingTrash = true;
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
        }
        if(other.tag == "construction"){
            currentBuilding = null;
        }
        if(other.tag == "door"){
            inDoor = "";
        }
    }
    void OnPause(){
        Time.timeScale = 0;
    }
    void OnResume(){
        Time.timeScale = 1;
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
    }
}
