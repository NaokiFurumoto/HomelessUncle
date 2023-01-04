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
    private long HaveMoney => (long)player?.PlayerStatus.HaveMoney;
    private float SliderValue => slider.value;
    private long Minusmoney => (long)(HaveMoney * SliderValue);

    // Start is called before the first frame update
    IEnumerator Start()
    {
       player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
       while (!player.IsInitialized) yield return null;

       SetBeforeMoney();
       SetSliderValue();
    }

    private void SetSliderValue()
    {
        if (slider == null)
            return;

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
        txt_Minus.text = ((long) (HaveMoney * SliderValue)).ToString();
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
    public void SlidebarDrag()
    {
        SetMinusMoney();
        SetAfterMoney();
    }
}
