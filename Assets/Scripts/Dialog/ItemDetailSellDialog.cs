using Carbon;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// アイテム詳細画面
/// アイテムデータを受け取って表示する画面
/// ロードされるものロード完了後に開く
/// </summary>
public class ItemDetailSellDialog : DialogBase
{
    /// <summary>アイテム名</summary>
    [SerializeField] private TextMeshProUGUI txt_ItemName;

    /// <summary>アイテムアイコン</summary>
    [SerializeField] private ItemIcon itemIcon;

    /// <summary>レアアイコン</summary>
    [SerializeField] private Image rareIcon;

    /// <summary>レアアイコンデータ</summary>
    [SerializeField] private UISpriteSheet rareIconSheet;

    /// <summary>売却値</summary>
    [SerializeField] private TextMeshProUGUI txt_SellPrice;

    /// <summary>売却可能：切替用</summary>
    [SerializeField] private GameObject sellOk;

    /// <summary>売却不可：切替用</summary>
    [SerializeField] private GameObject sellNon;

    /// <summary>所持数</summary>
    [SerializeField] private TextMeshProUGUI txt_HaveAmount;

    /// <summary>最大所持数</summary>
    [SerializeField] private TextMeshProUGUI txt_MaxAmount;

    /// <summary>アイテム詳細</summary>
    [SerializeField] private TextMeshProUGUI txt_ItemDetail;

    /// <summary>アビリティ詳細</summary>
    [SerializeField] private TextMeshProUGUI txt_AbilityDetail;

    /// <summary>アビリティ所持：切替用</summary>
    [SerializeField] private GameObject abilityOk;

    /// <summary>アビリティなし：切替用</summary>
    [SerializeField] private GameObject abilityNon;

    /// <summary>売るボタン</summary>
    [SerializeField] private Button btn_Sell;

    /// <summary>使うボタン</summary>
    [SerializeField] private Button btn_Use;

    //渡すデータ
    private ItemIcon itemInfo;

    protected override void Start()
    {
        base.Start();
        btn_Sell.onClick.AddListener(OnClickSell);
        btn_Use.onClick.AddListener(OnClickUsed);
    }

    /// <summary>
    /// ステータスセット
    /// </summary>
    /// <param name="data"></param>
    public void SetStatus(ItemIcon itemIcon = null)
    {
        if (itemIcon == null) return;

        itemInfo = null;
        var itemData = itemIcon.Data;
        this.itemIcon.SetupView(itemData);
        txt_ItemName.text = itemIcon.ItemName;
        rareIcon.sprite = rareIconSheet.GetSprite(itemData.GetRareText());
        txt_SellPrice.text = itemData.SellPrice.ToString();
        ChangeSellObject(itemData.IsSell);
        txt_HaveAmount.text = itemIcon.HoldIndex.ToString();
        txt_MaxAmount.text = itemData.MaxHaveIndex.ToString();
        txt_ItemDetail.text = itemData.ItemInfo;
        txt_AbilityDetail.text = itemData.Ability.AbilityInfo.ToString();
        ChangeAbilityObject(itemData.IsAbility);
        btn_Sell.gameObject.SetActive(itemData.IsSell);
        btn_Use.gameObject.SetActive(itemData.IsUse);

        itemInfo = itemIcon;
        OpenDialog();
    }

    /// <summary>
    /// 売れるアイテムの場合の項目切り替え
    /// </summary>
    /// <param name="judge"></param>
    private void ChangeSellObject(bool judge)
    {
        //売れる商品ならば
        sellOk.SetActive(judge);
        sellNon.SetActive(!judge);
    }

    /// <summary>
    /// 売れるアイテムの場合の項目切り替え
    /// </summary>
    /// <param name="judge"></param>
    private void ChangeAbilityObject(bool judge)
    {
        //売れる商品ならば
        abilityOk.SetActive(judge);
        abilityNon.SetActive(!judge);
    }

    /// <summary>
    /// 売却ボタンを押したとき
    /// </summary>
    private void OnClickSell()
    {
        //売却ダイアログを開く
        var dialogObj = DialogController.Instance.ShowDialog(DIALOGTYPE.ITEMSELL);
        ItemSellDialog dialog = dialogObj?.GetComponent<ItemSellDialog>();
        dialog?.PassItemInfo(itemInfo);
    }

    /// <summary>
    /// 使うボタンを押したとき
    /// </summary>
    private void OnClickUsed()
    {

    }

}
