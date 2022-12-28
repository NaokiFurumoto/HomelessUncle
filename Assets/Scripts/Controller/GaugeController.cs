using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// スライダーの操作親
/// </summary>
public class GaugeController : UIParts
{
    [SerializeField]
    private Slider slider;

    private (float, float) valueMinMax;
    protected float currentValue;

    public Slider Slider => slider;
    public virtual (float, float) ValueMinMax => valueMinMax;
    public float CurrentValue { get { return currentValue; } set { currentValue = value; SetValueSlider(); } }

    protected virtual void Start() { }

    /// <summary>
    /// 最小値、最大値の設定
    /// </summary>
    /// <param name="min"></param>
    /// <param name="max"></param>
    public virtual void SetMinMaxValue(float min, float max)
    {
        if (slider == null) return;
        slider.minValue = valueMinMax.Item1 = min;
        slider.maxValue = valueMinMax.Item2 = max;
    }

    
    protected virtual void SetValueSlider()
    {
        slider.value = currentValue;
    }

    /// <summary>
    /// 減算
    /// </summary>
    /// <param name="value"></param>
    public void SubValue(float value)
    {
        currentValue -= value;
        currentValue = Mathf.Clamp(currentValue, valueMinMax.Item1, valueMinMax.Item2);
        SetValueSlider();
    }

    /// <summary>
    /// 加算
    /// </summary>
    /// <param name="value"></param>
    public void AddValue(float value)
    {
        currentValue += value;
        currentValue = Mathf.Clamp(currentValue, valueMinMax.Item1, valueMinMax.Item2);
        SetValueSlider();
    }
}
