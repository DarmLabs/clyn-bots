using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField]
    #region Imports
    Construction_UI constructionUI_Script;
    [Space(10)]
    #endregion
    float timePressed;
    bool facingTrash;
    public int noRecTrash, organicTrash, recTrash;
    GameObject construction;
    GameObject currentBuilding;
    void Start()
    {
        constructionUI_Script.GetComponent<Construction_UI>();
    }
    void Update()
    {
        Controls();
        if(Input.GetKey(KeyCode.Space) && facingTrash){
            Aspire();
        }
    }
    void Aspire(){
        timePressed +=  Time.deltaTime;
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
        construction = Instantiate(Resources.Load("Constructions/"+constructionName), transform.position + (transform.forward * 4), transform.rotation) as GameObject;
        construction.transform.parent = transform;
        constructionUI_Script.constructionPanel.SetActive(false);
        OnResume();
    }
    void Controls(){
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
            if(currentBuilding != null){
                int reqOrg = currentBuilding.GetComponent<Construction>().reqOrg;
                int reqRec = currentBuilding.GetComponent<Construction>().reqRec;
                Debug.Log(reqOrg);
                Debug.Log(reqRec);
                if(reqOrg <= organicTrash && reqRec <= recTrash){
                    organicTrash -= reqOrg;
                    recTrash -= reqRec;
                    currentBuilding.GetComponent<Construction>().Constructed();
                }
                else{
                    Debug.Log("No tienes suficientes recursos");
                }
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "trash"){
            facingTrash = true;
        }
        if(other.tag == "construction"){
            currentBuilding = other.gameObject;
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
    }
    void OnPause(){
        Time.timeScale = 0;
    }
    void OnResume(){
        Time.timeScale = 1;
    }
}
