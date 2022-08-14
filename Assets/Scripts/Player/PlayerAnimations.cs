using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    PlayerInteraction playerInteraction;
    Animator playerAnimator;

    void Start()
    {
        playerInteraction = GetComponent<PlayerInteraction>();
        playerAnimator = GetComponent<Animator>();
    }
    void LateUpdate()
    {
        if (!playerInteraction.playerMovement.enabled)
        {
            Moving(false);
        }
    }
    public void CheckSpeed(float speed)
    {
        playerAnimator.SetFloat("speed", speed);
    }
    public void Moving(bool state)
    {
        playerAnimator.SetBool("isMoving", state);
    }
    public void Interaction(bool state)
    {
        playerAnimator.Play("Interaction");
    }
    public void Aspire(bool state)
    {
        playerAnimator.SetBool("isAspiring", state);
    }
}
