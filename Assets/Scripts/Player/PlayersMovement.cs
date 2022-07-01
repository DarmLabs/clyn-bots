using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersMovement : MonoBehaviour
{
    float speed = 5f;
    float speedValue;
    Vector3 forward, right, heading, direction, rightMovement, upMovement;
    PlayerAnimations playerAnim;
    Rigidbody rb;
    public bool wallAhed = false;
    
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
        if(!wallAhed){
            Movement();
            
        }else{
            playerAnim.Walking(false);
        }
        Rotation();
    }
    void Movement()
    {
        rb.position += rightMovement;
        rb.position += upMovement;
        if(heading.x == 0 && heading.y == 0 && heading.z == 0){
            playerAnim.Walking(false);
        }else{
            playerAnim.Walking(true);
        }
    }
    void Rotation(){
        direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        rightMovement = right * speed * Time.deltaTime * Input.GetAxis("Horizontal");
        upMovement = forward * speed * Time.deltaTime * Input.GetAxis("Vertical");
        heading = Vector3.Normalize(rightMovement + upMovement);
        transform.forward += heading;
    }
    
}
