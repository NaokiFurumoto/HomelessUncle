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
/// お金の換金に関するクラス
/// </summary>
public class MoneyChangeView : ViewBase
{
    [SerializeField] private CommonUISlider slider;
    [SerializeField] private Button btn_Decide;
    [SerializeField] private TextMeshProUGUI txt_Minus;
    [SerializeField] private TextMeshProUGUI txt_Before;
    [SerializeField] private TextMeshProUGUI txt_After;

    private Player player;
    private MoneyController moneyCtrl;
    private bool isDecide;

    private long HaveMoney => player.PlayerStatus.HaveMoney;
    private long SliderValue => (long)slider.value;
    private long Minusmoney => (long)SliderValue;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        base.Start();

        isDecide = false;
        btn_Decide.onClick.AddListener(OnClickDecide);

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        moneyCtrl = UIController.Instance.GetUIParts(UIType.OTHER, NameType.MONEY) as MoneyController;

        while (!player.IsInitialized) yield return null;

        SetSlider();
        SetBeforeMoney();
    }

    protected override void OnEnable()
    {
        if (player == null)
            return;

        slider.minValue = slider.value = 0;
        slider.maxValue = HaveMoney;
        SetBeforeMoney();
    }

    protected override void OnDisable()
    {
        //決定ボタンが押されたら
        if (isDecide)
        {
            moneyCtrl.SetMinusMoney(SliderValue);
            moneyCtrl.UpdateMoneyChangeView();
            isDecide = false;
            animator.SetTrigger("close");
        }
    }

    private void SetSlider()
    {
        if (slider == null)
            return;

        slider.minValue = slider.value = 0;
        slider.maxValue = HaveMoney;
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
    /// 減算値の設定
    /// </summary>
    private void SetMinusMoney()
    {
        txt_Minus.text = "-" + SliderValue.ToString();
    }

    /// <summary>
    /// 残りの金額
    /// </summary>
    private void SetAfterMoney()
    {
        txt_After.text = (HaveMoney - Minusmoney).ToString();
    }

    /// <summary>
    /// スライダー更新時の処理
    /// </summary>
    public void SlidebarDrag(float value = 0.0f)
    {
        slider.MaxButton.interactable = ((int)value != slider.maxValue) ? true : false;
        slider.MinButton.interactable = ((int)value != slider.minValue) ? true : false;
        btn_Decide.interactable = (SliderValue == slider.minValue) ? false : true;
        SetMinusMoney();
        SetAfterMoney();
    }

    /// <summary>
    /// 決定ボタンが押された
    /// </summary>
    public void OnClickDecide()
    {
        isDecide = true;
        player.PlayerStatus.HaveMoney = (long)HaveMoney - Minusmoney;
        player.PlayerStatus.Loan -= SliderValue;

        animator?.SetTrigger("close");
    }
}
