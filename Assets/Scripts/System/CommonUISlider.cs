using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

/// <summary>
/// 共通スライダークラス
/// </summary>
public class CommonUISlider : Slider
{
    [SerializeField] private Button maxButton;
    [SerializeField] private Button minButton;

    public Button MaxButton => maxButton;
    public Button MinButton => minButton;

    private void Start()
    {
        //押された時に現在値を最小値か最大値にする
        maxButton.onClick.AddListener(() => { value = maxValue; maxButton.interactable = false; });
        minButton.onClick.AddListener(() => { value = minValue; minButton.interactable = false; });
    }
}

[CustomEditor(typeof(CommonUISlider))]
public class CustomCommonSliderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
    }
}