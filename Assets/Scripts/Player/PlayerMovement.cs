using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    PlayerAnimations playerAnim;
    float speed = 6f;
    float speedValue;
    Vector3 forward, right, heading, rightMovement, upMovement;
    Rigidbody rb;
    public bool wallAhed = false;
    public bool isWalking, isRunning;

    void Start()
    {
        playerAnim = GetComponent<PlayerAnimations>();
        rb = GetComponent<Rigidbody>();
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;
    }
    void FixedUpdate()
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            if (!wallAhed)
            {
                Movement();
                playerAnim.Moving(true);
            }
            else
            {
                playerAnim.Moving(false);
            }
            Rotation();
        }
        else
        {
            playerAnim.Moving(false);
        }
    }
    void Movement()
    {
        if (Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.Space))
        {
            speed = 10f;
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            speed = 6f;
            playerAnim.Aspire(true);
        }
        else
        {
            speed = 6f;
            playerAnim.Aspire(false);
        }
        playerAnim.CheckSpeed(speed);
        rb.position += heading;
    }
    void Rotation()
    {
        rightMovement = right * speed * Time.deltaTime * Input.GetAxis("Horizontal");
        upMovement = forward * speed * Time.deltaTime * Input.GetAxis("Vertical");
        heading = rightMovement + upMovement;
        transform.rotation = Quaternion.LookRotation(heading);
    }

}
