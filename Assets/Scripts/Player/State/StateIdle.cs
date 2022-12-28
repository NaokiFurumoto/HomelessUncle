using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player
{
    /// <summary>
    /// 待機中
    /// PlayerのPrivateな変数にアクセス出来るので子クラスにしている
    /// </summary>
    public class StateIdle : PlayerStateBase
    {
        //待機時間計測用
        private Time idleTimes;
        public override void OnEnter(Player player, PlayerStateBase state) 
        {
            player.isMove = true;
            //待機時間を計測
            player.playerAnim.SetTrigger("Idle");
        }

        //待機中に寝たりする？
        //吹き出しを出す？
        public override void OnUpdate(Player player) { }

        //
        public override void OnExit(Player player, PlayerStateBase state)
        {
            //計測終了   
           player.playerAnim.ResetTrigger("Idle");
        }
    }
}
