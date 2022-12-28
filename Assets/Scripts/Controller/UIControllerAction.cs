using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// インゲームUIに関するメソッドの登録
/// </summary>
public partial class UIController
{
    private ViewController viewCtrl;

    /// <summary>
    /// 掘るアクション
    /// </summary>
    private void DigBtnCallback()
    {
        //プレイヤーのステートがDig状態ならば連打できない
        if (player.CurrentState == player.Idle)
        {
            player.transform.localScale = Vector3.one;
            player.ChangeState(player.Dig);
            //プレイヤーの体力を減算
        }
    }

    /// <summary>
    /// アイテムボタン
    /// </summary>
    private void ItemBtnCallback()
    {
        if (viewCtrl == null) return;
        viewCtrl.ActiveView(VIEWTYPE.ITEMVIEW,true);
    }


}
