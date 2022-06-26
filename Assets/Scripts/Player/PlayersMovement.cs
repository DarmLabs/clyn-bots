using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersMovement : MonoBehaviour
{
    float speed = 5f;
    Vector3 forward;
    Vector3 right;
    PlayerAnimations playerAnim;
    Rigidbody rb;
    
    void Start()
    {
        playerAnim =GetComponent<PlayerAnimations>();
        rb = GetComponent<Rigidbody>();
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0,90,0)) * forward;
    }
    void FixedUpdate()
    {
        Movement();
    }
    void Movement()
    {
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 rightMovement = right * speed * Time.deltaTime * Input.GetAxis("Horizontal");

        Vector3 upMovement = forward * speed * Time.deltaTime * Input.GetAxis("Vertical");

        Vector3 heading = Vector3.Normalize(rightMovement + upMovement);
        transform.forward += heading;
        rb.position += rightMovement;
        rb.position += upMovement;
        if(heading.x == 0 && heading.y == 0 && heading.z == 0){
            playerAnim.Walking(false);
        }else{
            playerAnim.Walking(true);
        }
       
    }
}
