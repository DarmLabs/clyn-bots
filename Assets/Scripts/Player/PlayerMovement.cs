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
    [SerializeField] PhysicMaterial antiClimb;
    CapsuleCollider playerCollider;
    void Start()
    {
        playerAnim = GetComponent<PlayerAnimations>();
        rb = GetComponent<Rigidbody>();
        playerCollider = GetComponent<CapsuleCollider>();
    }
    void FixedUpdate()
    {
        Controls();
        TakeOrientation();
    }
    void Controls()
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
    public void TakeOrientation()
    {
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;
    }
    void Movement()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 10f;
        }
        else
        {
            speed = 6f;
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
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Recycler")
        {
            playerCollider.material = antiClimb;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Recycler")
        {
            playerCollider.material = null;
        }
    }
}
