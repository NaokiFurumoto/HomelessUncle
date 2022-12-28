using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �e�X�e�[�g���ƂɋL��
/// </summary>
public partial class Player
{
    /// <summary>
    /// ����
    /// </summary>
    public class StateWalking : PlayerStateBase
    {
        public override void OnEnter(Player player, PlayerStateBase state)
        {
            player.playerAnim.SetTrigger("Walk");
        }

        public override void OnUpdate(Player player) { }

        public override void OnExit(Player player, PlayerStateBase state)
        {
            //�v���I��   
            player.playerAnim.ResetTrigger("Walk");
        }
    }
}
