using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �e�X�e�[�g���ƂɋL��
/// </summary>
public partial class Player
{
    /// <summary>
    /// �g�C��
    /// </summary>
    public class StateToilet : PlayerStateBase
    {
        public override void OnEnter(Player player, PlayerStateBase state)
        {
        }

        //�ҋ@���ɐQ���肷��H
        //�����o�����o���H
        public override void OnUpdate(Player player) { }

        //
        public override void OnExit(Player player, PlayerStateBase state)
        {
            //�v���I��   
        }
    }
}
