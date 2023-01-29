using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System.Collections;

/// <summary>
/// アイテム売却ダイアログ
/// </summary>
public class ItemSellDialog : DialogBase
{
    [SerializeField] private CommonUISlider slider;
    [SerializeField] private Button btn_Decide;
    [SerializeField] private TextMeshProUGUI txt_SellAmount;
    [SerializeField] private TextMeshProUGUI txt_Before;
    [SerializeField] private TextMeshProUGUI txt_After;
    [SerializeField] private TextMeshProUGUI txt_Plus;

    private Player player;
    private MoneyController moneyCtrl;
    private long price;//売却値
    private ItemIcon selledPriceItemData;//売却後の受け渡しデータ
    private bool isInitialize = false;//初期化判定
    private PlayerItemView playerItemView;

    private long HaveMoney => player.PlayerStatus.HaveMoney;
    private int SliderValue => (int)slider.value;
    private long PlusMoney => (long)(SliderValue * price);
   

    protected override void Start()
    {
        base.Start();
    }

    private IEnumerator Initialize(ItemIcon iconInfo)
    {
        isInitialize = false;

        btn_Decide.onClick.AddListener(OnClickDecide);

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        while (!player.IsInitialized)
        {
            yield return null;
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        }

        playerItemView = ViewController.Instance.GetView(VIEWTYPE.ITEMVIEW) as PlayerItemView;
        moneyCtrl = UIController.Instance.GetUIParts(UIType.OTHER, NameType.MONEY) as MoneyController;
        isInitialize = true;

        SetItemInfo(iconInfo);
    }

    /// <summary>
    /// アイテム情報を受け取って初期化
    /// </summary>
    /// <param name="iconinfo"></param>
    public void PassItemInfo(ItemIcon iconInfo)
    {
        StartCoroutine(Initialize(iconInfo));
    }

    /// <summary>
    /// 情報の設定
    /// </summary>
    /// <param name="itemInfo"></param>
    private void SetItemInfo(ItemIcon iconInfo)
    {
        if (iconInfo == null) return;

        selledPriceItemData = iconInfo;
        price = iconInfo.Data.SellPrice;
        SetSlider(iconInfo.HoldIndex);
        SetBeforeMoney();
        OpenDialog();
    }

    /// <summary>
    /// スライダーに関する値の設定
    /// </summary>
    /// <param name="haveAmount"></param>
    private void SetSlider(int haveAmount)
    {
        //初期化完了してないと呼んだらだめ
        if (!isInitialize || slider == null)
            return;

        txt_Plus.text = "0";
        slider.minValue = slider.value = 0;
        slider.maxValue = haveAmount;//個数に変更
        slider.onValueChanged.AddListener(SlidebarDrag);
        SlidebarDrag();
    }

    /// <summary>
    /// 所持金の設定
    /// </summary>
    private void SetBeforeMoney()
    {
        txt_Before.text = HaveMoney.ToString();
    }

    /// <summary>
    /// 売却数の設定
    /// </summary>
    private void SetItemAmount()
    {
        txt_SellAmount.text = SliderValue.ToString();
    }

    /// <summary>
    /// 残りの金額
    /// </summary>
    private void SetAfterMoney()
    {
        txt_After.text = (HaveMoney + PlusMoney).ToString();
    }

    /// <summary>
    /// スライダー更新時の処理
    /// </summary>
    public void SlidebarDrag(float value = 0.0f)
    {
        slider.MaxButton.interactable = ((int)value != slider.maxValue) ? true : false;
        slider.MinButton.interactable = ((int)value != slider.minValue) ? true : false;
        btn_Decide.interactable = (SliderValue == slider.minValue) ? false : true;
        txt_Plus.text = "+" + PlusMoney.ToString();
        SetItemAmount();
        SetAfterMoney();
    }

    /// <summary>
    /// 決定ボタンが押された
    /// </summary>
    public void OnClickDecide()
    {
        var itemAmount = (int)slider.maxValue - (int)slider.value;
        selledPriceItemData.HoldIndex = itemAmount;

        //所持品の更新
        player.ReWritePlayerHoldItem(selledPriceItemData);
        playerItemView.UpdateList();

        //所持金の更新
        player.PlayerStatus.HaveMoney = (long)HaveMoney + PlusMoney;
        moneyCtrl.SetHaveMoney(player.PlayerStatus.HaveMoney);

        //表示中のダイアログを消去
        DialogController.Instance.DeleteIndexDialog(LayerType.LAYER01);
        animator?.SetTrigger("close");
    }
}
