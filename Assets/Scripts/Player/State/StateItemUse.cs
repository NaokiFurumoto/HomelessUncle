using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GlobalValue;

public partial class Player
{
    public class StateItemUse : PlayerStateBase
    {
        //View

        public override void OnEnter(Player player, PlayerStateBase state)
        {
            //他のボタンは押せないように
            player.isMove = false;
            //digAction = GameObject.FindGameObjectWithTag("DigAction").GetComponent<DigAction>();

            player.playerAnim.SetTrigger("UseItem");
            //EffectManager.Instance.PlayEffectPlayer(EffectManager.EFFECT_TYPE.DIG_SCOP, PlayerEffectRoot.ROOT_TYPE.UNDER);

            //digItem = digAction?.GetItem();
            //if (digItem != null)
            //{
            //    digAction.DigItemGenerate(digItem);
            //    player.SetHoldPlayerItem(digItem, 1);
            //}
            //else
            //{
            //    //はずれ
            //}

            //効果適用？：秒後とか
            //player.PlayerStatus.Hp -= DIG_DAMAGE_INIT;
            //UIController.Instance.SetLifeGaugeParam(player.PlayerStatus);
        }


        //待機中に寝たりする？
        //吹き出しを出す？
        public override void OnUpdate(Player player) { }


        public override void OnExit(Player player, PlayerStateBase state)
        {
            player.playerAnim.ResetTrigger("UseItem");
        }


    }
}

