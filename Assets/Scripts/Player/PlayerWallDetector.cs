using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallDetector : Player
{
    void Start()
    {
        playerMovement = GetComponentInParent<PlayerMovement>();
    }
    void FixedUpdate()
    {
        RaycastHit hit;
        Ray forwardRay = new Ray(transform.position, transform.parent.forward);

        if (Physics.Raycast(forwardRay, out hit))
        {
            if (hit.distance < 0.5)
            {
                playerMovement.wallAhed = true;
            }
            else
            {
                playerMovement.wallAhed = false;
            }
        }
    }
}
