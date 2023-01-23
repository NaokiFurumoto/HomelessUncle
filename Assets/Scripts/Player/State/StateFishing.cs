using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 各ステートごとに記載
/// </summary>
public partial class Player
{
    /// <summary>
    /// 釣り
    /// </summary>
    public class StateFishing : PlayerStateBase
    {
        private FishAction fishAction;
        private Player player;

        public override void OnEnter(Player player, PlayerStateBase state)
        {
            this.player = player;
            fishAction = GameObject.FindGameObjectWithTag("FishAction").GetComponent<FishAction>();
            //アニメーション開始
            player.isMove = false;
            player.PlayerAnim.SetTrigger("Fish");
            fishAction.FishingStart();
        }

        public override void OnUpdate(Player player) { }
        
        /// <summary>
        /// 釣り終了後のアクション
        /// </summary>
        public void FishingEnd()
        {
            //ボタンの復帰
            UIController.Instance.SetBtnInteractable(NameType.FISH, true);
            player.ChangeState(player.Idle);
        }
    }
}
