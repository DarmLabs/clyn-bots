using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMinigame : StateMachineBehaviour
{
    General_UI general_UI;
    bool isAnimating;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        general_UI = GameObject.Find("CanvasOverlay").GetComponent<General_UI>();
        if(!isAnimating){
            general_UI.minigameAspire.GetComponent<Aspiradora>().primeraVez = false;
            general_UI.MinigameAspireSwitcher(true);
            isAnimating = true;
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        isAnimating = false;
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
