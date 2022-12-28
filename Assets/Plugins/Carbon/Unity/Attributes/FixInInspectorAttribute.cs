using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Carbon
{
	/// <summary>
	/// Inspector に表示されている property を変更不可にする
	/// </summary>
	/// <example>
	/// <code>
	/// [SerializeField, FixInInspector] private string m_Hoge;
	/// </code>
	/// </example>
	public sealed class FixInInspectorAttribute : PropertyAttribute
	{
		// do nothing
	}

#if UNITY_EDITOR
	/// <summary>
	/// Inspector に FixInInspectorAttribute 付き項目を描画します.
	/// </summary>
	[CustomPropertyDrawer(typeof(FixInInspectorAttribute))]
	public sealed class FixInInspectorDrawer : PropertyDrawer
	{
		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			return EditorGUI.GetPropertyHeight(property, label, true);
		}

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			GUI.enabled = false;
			EditorGUI.PropertyField(position, property, label, true);
			GUI.enabled = true;
		}
	}
#endif
}