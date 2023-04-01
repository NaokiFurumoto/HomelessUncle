using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーの状態
/// ステート基底クラス
/// </summary>
public partial class Player 
{
    public abstract class PlayerStateBase 
    {
        /// <summary> ステートを開始した時に呼ばれる </summary>
        public virtual void OnEnter(Player player, PlayerStateBase state) { }

        /// <summary> 毎フレーム呼ばれる </summary>
        public virtual void OnUpdate(Player player) { }

        /// <summary> ステートを終了した時に呼ばれる </summary>
        public virtual void OnExit(Player player, PlayerStateBase nextState) { }

        //アニメーション終了時の処理も定義する
    }
}
