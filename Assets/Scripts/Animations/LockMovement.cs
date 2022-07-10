using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockMovement : StateMachineBehaviour
{
    GameObject player;
    PlayerInteraction playerInteraction;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        player = GameObject.Find("Player");
        playerInteraction = player.GetComponent<PlayerInteraction>();
        playerInteraction.MovmentState(false);
    }
    public override void  OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        if(playerInteraction.inDoor != ""){
            playerInteraction.ChangeStage();
        }
        if(animatorStateInfo.IsName("Suction_Pose_Anim Reverse")){
            playerInteraction.MovmentState(true);
            playerInteraction.isAspiring = false;
        }
        animator.SetBool("isInteracting", false);
    }
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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
