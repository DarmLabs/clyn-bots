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
    void FixedUpdate()
    {
        RaycastHit hit;
        Ray forwardRay = new Ray(transform.position, transform.parent.forward);

        if(Physics.Raycast(forwardRay, out hit)){
            if(hit.distance < 0.5){
                playersMovement.wallAhed = true;
            }else{
                playersMovement.wallAhed = false;
            }
        }
    }
}
