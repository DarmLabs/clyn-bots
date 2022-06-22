using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    Animator playerAnim;

    void Start()
    {
        playerAnim = GetComponent<Animator>();
    }
    void LateUpdate(){
        if(!GetComponent<PlayersMovement>().enabled)
        {
            Walking(false);
        }
    }
    public void Walking(bool state){
        playerAnim.SetBool("isWalking", state);
    }
    public void Interaction(bool state){
        playerAnim.SetBool("isInteracting", state);
        
    }
    public void Aspire(bool state){
        playerAnim.SetBool("isAspiring", state);
    }
    public void Celebrate(){
        playerAnim.Play("Celebration_Anim");
    }
}