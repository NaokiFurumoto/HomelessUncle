using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace GetMoney
{
    public class GeneratorBar : GaugeController
    {
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
            txt_Max.text = max.ToString();
        }

        /// <summary>
        /// 現在値、最大値の設定
        /// </summary>
        /// <param name="now"></param>
        /// <param name="max"></param>
        public void SetMinMaxIntValue(int now, int max)
        {
            txt_Max.text = max.ToString();
            txt_Now.text = now.ToString();
        }

        /// <summary>
        /// 値の設定
        /// 現在の値とスライダーの設定
        /// </summary>
        /// <param name="value"></param>
        public override void SetValie(float value)
        {
            currentValue = value;
            txt_Now.text = ((int)value).ToString();
            SetValueSlider();
        }

        /// <summary>
        /// スライダーの数値を反映
        /// </summary>
        protected override void SetValueSlider()
        {
            base.SetValueSlider();
        }
    }
}
