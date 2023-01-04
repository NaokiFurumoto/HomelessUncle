using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CommonUISlider))]
public class CustomCommonSliderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
    }
}
