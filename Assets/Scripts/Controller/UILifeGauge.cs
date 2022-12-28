using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// 体力ゲージのUI制御
/// </summary>
public class UILifeGauge : GaugeController
{
    [SerializeField] private TextMeshProUGUI txt_Min;
    [SerializeField] private TextMeshProUGUI txt_Max;
    [SerializeField] private TextMeshProUGUI txt_Now;

    // Start is called before the first frame update
    protected override void Start() { }

    /// <summary>
    /// 最小値、最大値の設定
    /// </summary>
    /// <param name="min"></param>
    /// <param name="max"></param>
    public override void SetMinMaxValue(float min, float max)
    {
        base.SetMinMaxValue(min, max);
        txt_Min.text = min.ToString();
        txt_Max.text = max.ToString();
    }

    /// <summary>
    /// スライダーの数値を反映
    /// </summary>
    protected override void SetValueSlider()
    {
        base.SetValueSlider();
        txt_Now.text = currentValue.ToString();
    }

}
