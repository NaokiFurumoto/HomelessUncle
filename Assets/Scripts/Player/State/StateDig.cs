using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GlobalValue;

/// <summary>
/// �e�X�e�[�g���ƂɋL��
/// </summary>
public partial class Player
{
    /// <summary>
    /// �@��
    /// �@��>�A�C�e�����@
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
                //�͂���
            }

            //�̗͌���
            player.PlayerStatus.Hp -= DIG_DAMAGE_INIT;
            UIController.Instance.SetLifeGaugeParam(player);
        }

        /// <summary>
        /// �@��o���A�C�e����\��
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


        //�ҋ@���ɐQ���肷��H
        //�����o�����o���H
        public override void OnUpdate(Player player) { }


        public override void OnExit(Player player, PlayerStateBase state)
        {
            player.playerAnim.ResetTrigger("Dig");
        }
       
    }
}
