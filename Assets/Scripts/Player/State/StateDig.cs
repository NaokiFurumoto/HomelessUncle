using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GlobalValue;

/// <summary>
/// 各ステートごとに記載
/// </summary>
public partial class Player
{
    /// <summary>
    /// 掘る
    /// 掘る>アイテム発掘
    /// </summary>
    public class StateDig : PlayerStateBase
    {
        private DigAction digAction;
        private ItemData digItem;

        public override void OnEnter(Player player, PlayerStateBase state)
        {
            player.isMove = false;
            digAction = GameObject.FindGameObjectWithTag("DigAction").GetComponent<DigAction>();

            player.playerAnim.SetTrigger("Dig");
            EffectManager.Instance.PlayEffectPlayer(EffectManager.EFFECT_TYPE.DIG_SCOP, PlayerEffectRoot.ROOT_TYPE.UNDER);

            digItem = digAction?.GetItem();
            if(digItem!= null)
            {
                digAction.DigItemGenerate(digItem);
                player.SetHoldPlayerItem(digItem, 1);
            }
            else
            {
                //はずれ
            }

            //体力減少
            player.PlayerStatus.Hp -= DIG_DAMAGE_INIT;
            UIController.Instance.SetLifeGaugeParam(player.PlayerStatus);
        }
       

        //待機中に寝たりする？
        //吹き出しを出す？
        public override void OnUpdate(Player player) { }


        public override void OnExit(Player player, PlayerStateBase state)
        {
            player.playerAnim.ResetTrigger("Dig");
        }
       
    }
}
