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

            digItem = digAction?.GetDigItem();
            if(digItem!= null)
            {
                DigItemGenerate(digItem);
                player.SetHoldPlayerItem(digItem, 1);
            }
            else
            {
                //はずれ
            }

            //体力減少
            player.PlayerStatus.Hp -= DIG_DAMAGE_INIT;
            UIController.Instance.SetLifeGaugeParam(player);
        }

        /// <summary>
        /// 掘り出しアイテムを表示
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
         private void DigItemGenerate(ItemData data)
        {
            GameObject itemObj = (GameObject)Resources.Load("Prefabs/DigItem");
            var parent = PlayerEffectRoot.Instance.GetEffectRoot(PlayerEffectRoot.ROOT_TYPE.RIGHT);
            GameObject instace = Instantiate(itemObj, Vector2.zero, Quaternion.identity, parent);

            var status = instace?.GetComponent<DigItem>();
            status?.SetImage(data);
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
