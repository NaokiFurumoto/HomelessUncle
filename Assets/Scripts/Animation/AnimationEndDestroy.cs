using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �A�j���[�V�����I����ɍ폜����
/// </summary>
public class AnimationEndDestroy : StateMachineBehaviour
{
    /// <summary>
    /// �A�j���[�V�����I����
    /// </summary>
    /// <param name="animator"></param>
    /// <param name="stateInfo"></param>
    /// <param name="layerIndex"></param>
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Destroy(animator?.gameObject);
    }
}
