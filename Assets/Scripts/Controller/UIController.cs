using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;
using static GlobalValue;

/// <summary>
/// インゲームUIの制御に関するクラス
/// </summary>
public partial class UIController : SingletonMonoBehaviour<UIController>
{

    /// <summary> Player </summary>
    private Player player;

    /// <summary> UI </summary>
    private ButtonController btn_Dig;
    private ButtonController btn_Fish;
    private ButtonController btn_Item;
    private GaugeController  gaugeLife;

    [SerializeField]
    private List<UIParts> uiPartsList = new List<UIParts>();

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")
                                   .GetComponent<Player>();
        viewCtrl = ViewController.Instance;

        SetInitializeUI();
        //更新タイミングは後々
        UpdateSetParamUI(player);
    }

    /// <summary>
    /// UIパーツにアクション登録
    /// </summary>
    private void SetInitializeUI()
    {
        btn_Dig = GetUIParts(UIType.BTN, NameType.DIG) as ButtonController;
        btn_Dig?.SetBtnEvent(DigBtnCallback);

        btn_Item = GetUIParts(UIType.BTN, NameType.ITEM) as ButtonController;
        btn_Item?.SetBtnEvent(ItemBtnCallback);

        gaugeLife = GetUIParts(UIType.GAUGE, NameType.LIFE) as GaugeController;

    }

    /// <summary>
    /// UIパラメーターの更新
    /// </summary>
    public void UpdateSetParamUI(Player player)
    {
        //体力ゲージ更新
        SetLifeGaugeParam(player);

    }

    /// <summary>
    /// 体力ゲージの設定
    /// </summary>
    /// <param name="player"></param>
    public void SetLifeGaugeParam(Player player)
    {
        //初期値:後で変更
        if (gaugeLife != null)
        {
            gaugeLife.SetMinMaxValue(LIFE_GAUGE_MINVALUE, player.PlayerStatus.MaxHp);
            gaugeLife.CurrentValue = player.PlayerStatus.Hp;
        }
    }

    /// <summary>
    /// UIを返す処理
    /// </summary>
    /// <param name="uiType"></param>
    /// <param name="btnType"></param>
    /// <returns></returns>
    private UIParts GetUIParts(UIType uiType = UIType.NONE, NameType nameType = NameType.NONE)
    {
        if (uiPartsList == null || uiType == UIType.NONE || nameType == NameType.NONE) 
            return null;

        UIParts ui = uiPartsList.Where(ui => ui.TypeUI == uiType)
                                .FirstOrDefault(ui => ui.NameType == nameType) as UIParts;
        return ui;
    }

}

/// <summary>
/// UIのパーツであること
/// </summary>
public abstract class UIParts : MonoBehaviour
{
    [SerializeField]
    private UIType uiType;

    [SerializeField]
    private NameType nameType;

    public UIType TypeUI => uiType;
    public NameType NameType => nameType;

}

/// <summary>
/// UIの種類
/// </summary>
public enum UIType
{
    NONE,
    BTN,
    GAUGE,
    CLOCK,
    MONEY,
}

/// <summary>
/// 種類
/// </summary>
public enum NameType
{
    NONE,
    DIG,
    FISH,
    LIFE,
    ITEM,
}
