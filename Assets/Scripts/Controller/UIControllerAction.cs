using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// インゲームUIに関するメソッドの登録
/// </summary>
public partial class UIController
{
    private ViewController viewCtrl;

   

    /// <summary>
    /// 釣りアクション
    /// </summary>
    private void OnClickFishBtn()
    {
        //プレイヤーのステートがDig状態ならば連打できない
        if (player.CurrentState == player.Idle)
        {
            SetBtnInteractable(NameType.FISH, false);

            player.transform.localScale = Vector3.one;
            player.ChangeState(player.Fishing);
        }
    }

    /// <summary>
    /// アイテムボタンを押したとき
    /// </summary>
    private void OnClickItemBtn()
    {
        if (viewCtrl == null) return;
        viewCtrl.ActiveView(VIEWTYPE.ITEMVIEW, true);
    }

    /// <summary>
    /// 指定ボタンの有効無効切替
    /// </summary>
    /// <param name="type"></param>
    /// <param name="judge"></param>
    public void SetBtnInteractable(NameType type, bool judge)
    {
        switch (type)
        {
            case NameType.FISH:
                btn_Fish.SetInteractable(judge);
                break;
        }
    }

    /// <summary>
    /// 全UIボタンの有効無効切替
    /// </summary>
    public void SetBtnAllInteractable(bool judge)
    {
        List<ButtonController> btnList = uiPartsList.Where(ui => ui.TypeUI == UIType.BTN) as List<ButtonController>;
        btnList.ForEach(list => list.SetInteractable(judge));
    }



}
