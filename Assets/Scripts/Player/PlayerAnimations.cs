using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : Player
{
    Animator playerAnimator;

    void Start()
    {
        playerAnimator = GetComponent<Animator>();
    }
    void LateUpdate()
    {
        if (!playerMovement.enabled)
        {
            Walking(false);
        }
    }
    public void Walking(bool state)
    {
        playerAnimator.SetBool("isWalking", state);
    }
    public void Interaction(bool state)
    {
        playerAnimator.SetBool("isInteracting", state);
    }
    public void Aspire(bool state)
    {
        playerAnimator.SetBool("isAspiring", state);
    }
    public void Celebrate()
    {
        playerAnimator.Play("Celebration_Anim");
    }
}
