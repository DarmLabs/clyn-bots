using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField]
    float timePressed;
    bool facingTrash;
    public int noRecTrash, organicTrash, recTrash;
    void Update()
    {
        if(Input.GetKey(KeyCode.Space) && facingTrash){
            Aspire();
        }
    }
    void Aspire(){
        timePressed +=  Time.deltaTime;
        if(timePressed > 2){
            sumTrash();
            timePressed = 0;
        }
    }
    void sumTrash(){
        noRecTrash += Random.Range(0, 4);
        organicTrash += Random.Range(0, 4);
        recTrash += Random.Range(0,4);
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "trash"){
            facingTrash = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.tag == "trash"){
            facingTrash = false;
        }
    }
}
