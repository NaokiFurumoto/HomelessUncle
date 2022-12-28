using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 各ステートごとに記載
/// </summary>
public partial class Player
{
    /// <summary>
    /// 歩く
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
            //計測終了   
            player.playerAnim.ResetTrigger("Walk");
        }
    }
}
