using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 各ステートごとに記載
/// </summary>
public partial class Player
{
    /// <summary>
    /// トイレ
    /// </summary>
    public class StateToilet : PlayerStateBase
    {
        public override void OnEnter(Player player, PlayerStateBase state)
        {
        }

        //待機中に寝たりする？
        //吹き出しを出す？
        public override void OnUpdate(Player player) { }

        //
        public override void OnExit(Player player, PlayerStateBase state)
        {
            //計測終了   
        }
    }
}
