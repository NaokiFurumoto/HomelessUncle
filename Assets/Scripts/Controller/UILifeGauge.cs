using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// �̗̓Q�[�W��UI����
/// </summary>
public class UILifeGauge : GaugeController
{
    [SerializeField] private TextMeshProUGUI txt_Min;
    [SerializeField] private TextMeshProUGUI txt_Max;
    [SerializeField] private TextMeshProUGUI txt_Now;

    // Start is called before the first frame update
    protected override void Start() { }

    /// <summary>
    /// �ŏ��l�A�ő�l�̐ݒ�
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
    /// �X���C�_�[�̐��l�𔽉f
    /// </summary>
    protected override void SetValueSlider()
    {
        base.SetValueSlider();
        txt_Now.text = currentValue.ToString();
    }

}
