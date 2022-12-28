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

    /// <summary> 借金 </summary>
    [SerializeField] private TextMeshProUGUI txt_LoanMoney;

    /// <summary> 減算額 </summary>
    [SerializeField] private TextMeshProUGUI txt_MinusMoney;

    /// <summary> マイナスオブジェクト </summary>
    [SerializeField] private GameObject minusObj;

    /// <summary> プレイヤー </summary>
    private Player player;

    /// <summary> お金交換ボタンコールバック </summary>
    private UnityAction changeMoneyCallBack;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")
                                   .GetComponent<Player>();
        changeMoneyCallBack = OnClickChangeMoney;

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
        SetHaveMoney(status.HaveMoney);
        SetLoanMoney(status.Loan);
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// 所持金設定
    /// </summary>
    /// <param name="value"></param>
    public void SetHaveMoney(long value)
    {
        txt_HaveMoney.text = value.ToString();
    }

    /// <summary>
    /// 借金設定
    /// </summary>
    /// <param name="value"></param>
    public void SetLoanMoney(long value)
    {
        txt_LoanMoney.text = value.ToString();
    }

    /// <summary>
    /// 減算値設定
    /// </summary>
    /// <param name="value"></param>
    public void SetMinusMoney(int value)
    {
        txt_MinusMoney.text = value.ToString();
    }

    //ボタンコールバック
    private void OnClickChangeMoney()
    {

    }
}
