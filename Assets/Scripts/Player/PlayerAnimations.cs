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
