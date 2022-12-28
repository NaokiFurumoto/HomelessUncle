using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEndDig : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var player = animator.transform.parent.gameObject.GetComponent<Player>();
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        }
        player.CurrentState = player.Idle;
        player.IsMove = true;
    }
}
