using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEndDialogDestro : StateMachineBehaviour
{
    /// <summary>
    /// アニメーション終了時
    /// </summary>
    /// <param name="animator"></param>
    /// <param name="stateInfo"></param>
    /// <param name="layerIndex"></param>
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        DialogController.Instance.DeleteFrontDialog();
    }
}
