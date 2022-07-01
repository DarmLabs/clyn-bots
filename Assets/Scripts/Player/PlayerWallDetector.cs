using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallDetector : MonoBehaviour
{
    PlayersMovement playersMovement;
    void Start()
    {
        playersMovement = GetComponentInParent<PlayersMovement>();
    }
    void OnTriggerEnter(Collider collisionInfo)
    {
        Debug.Log("entro");
        if(collisionInfo.gameObject.isStatic){
            playersMovement.wallAhed = true;
        }
    }
    void OnTriggerExit(Collider collisionInfo)
    {
        if(collisionInfo.gameObject.isStatic){
            playersMovement.wallAhed = false;
        }
    }
}
