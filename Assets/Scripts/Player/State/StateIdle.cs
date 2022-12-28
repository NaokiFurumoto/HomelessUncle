using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player
{
    /// <summary>
    /// �ҋ@��
    /// Player��Private�ȕϐ��ɃA�N�Z�X�o����̂Ŏq�N���X�ɂ��Ă���
    /// </summary>
    public class StateIdle : PlayerStateBase
    {
        //�ҋ@���Ԍv���p
        private Time idleTimes;
        public override void OnEnter(Player player, PlayerStateBase state) 
        {
            player.isMove = true;
            //�ҋ@���Ԃ��v��
            player.playerAnim.SetTrigger("Idle");
        }

        //�ҋ@���ɐQ���肷��H
        //�����o�����o���H
        public override void OnUpdate(Player player) { }

        //
        public override void OnExit(Player player, PlayerStateBase state)
        {
            //�v���I��   
           player.playerAnim.ResetTrigger("Idle");
        }
    }
}
