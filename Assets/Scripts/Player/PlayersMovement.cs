using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersMovement : MonoBehaviour
{
    float speed = 6f;
    float speedValue;
    Vector3 forward, right, heading, rightMovement, upMovement;
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
        if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0){
            if(!wallAhed){
                Movement();
            }else{
                playerAnim.Walking(false);
            }
            Rotation();
            playerAnim.Walking(true);
        }else{
            playerAnim.Walking(false);
        }
    }
    void Movement()
    {
        rb.position += rightMovement;
        rb.position += upMovement;
    }
    void Rotation(){
        rightMovement = right * speed * Time.deltaTime * (Input.GetAxis("Horizontal"));
        upMovement = forward * speed * Time.deltaTime * Input.GetAxis("Vertical");
        heading = Vector3.Normalize(rightMovement + upMovement);
        transform.rotation = Quaternion.LookRotation(heading);
    }
    
}
