using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

/// <summary>
/// プレイヤーのお金に関する制御
/// 表示用：プレイヤーのデータから取得して表示
/// </summary>
public class MoneyController : UIParts
{
    /// <summary> 所持金 </summary>
    [SerializeField] private TextMeshProUGUI txt_HaveMoney;

    ///// <summary> 借金 </summary>
    //[SerializeField] private TextMeshProUGUI txt_LoanMoney;

    ///// <summary> 減算額 </summary>
    //[SerializeField] private TextMeshProUGUI txt_MinusMoney;

    ///// <summary> マイナスオブジェクト </summary>
    //[SerializeField] private GameObject minusObj;

    ///// <summary> マイナスオブジェクト </summary>
    [SerializeField] private Button btn_Change;

    /// <summary> プレイヤー </summary>
    private Player player;

    private ViewController viewCtrl;

    private long minusValue;

    /// <summary> お金交換ボタンコールバック </summary>
    private UnityAction changeMoneyCallBack;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")
                                   .GetComponent<Player>();

        viewCtrl = ViewController.Instance;

        changeMoneyCallBack = OnClickChangeMoney;
        btn_Change.onClick.AddListener(OnClickChangeMoney);

        //プレイヤーの初期化完了待ち
        while (!player.IsInitialized) { yield return null; }

        InitializeMoney(player.PlayerStatus);
    }

    /// <summary>
    /// 初期化
    /// </summary>
    /// <param name="status"></param>
    private void InitializeMoney(Player.Status status)
    {
        minusValue = 0;
        UpdateMoney(status);
    }

    /// <summary>
    /// 金額更新処理
    /// </summary>
    /// <param name="status"></param>
    public void UpdateMoney(Player.Status status)
    {
        SetHaveMoney(status.HaveMoney);
    }

    /// <summary>
    /// 借金返済時の更新処理
    /// </summary>
    //public void UpdateMoneyChangeView()
    //{
    //    //借金が更新されるとアニメーション実行
    //    SetHaveMoney(player.PlayerStatus.HaveMoney);
    //    SetLoanMoney(player.PlayerStatus.Loan);

    //    if(minusValue > 0)
    //    {
    //        minusObj.SetActive(true);
    //    }
    //}

    /// <summary>
    /// 所持金設定
    /// </summary>
    /// <param name="value"></param>
    public void SetHaveMoney(long value)
    {
        txt_HaveMoney.text = value.ToString();
    }

   
    //ボタンコールバック
    private void OnClickChangeMoney()
    {
        if (viewCtrl == null) return;
        viewCtrl.ActiveView(VIEWTYPE.MONEYCHANGEVIEW, true);
    }
}
