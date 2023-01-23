using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEndResetTrigger : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //トリガーの挙動を無くす
        animator.ResetTrigger("Move");
        animator.ResetTrigger("Piku");
        animator.ResetTrigger("Hit");
    }
}
