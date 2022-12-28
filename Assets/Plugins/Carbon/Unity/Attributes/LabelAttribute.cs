using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Carbon
{
	/// <summary>
	/// Inspector ビューのラベル名を指定された文字列で置き換えます
	/// </summary>
	/// <example>
	/// <code>
	/// [Label("名前")] public string m_Hoge;
	/// </code>
	/// </example>
	public sealed class LabelAttribute : PropertyAttribute
	{
		/// <summary>
		/// 表示する文字列
		/// </summary>
		public string Label;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="label">表示する文字列</param>
		public LabelAttribute(string label)
		{
			Label = label;
		}
	}

#if UNITY_EDITOR
	/// <summary>
	/// Inspector に LabelAttribute 付き項目を描画します.
	/// </summary>
	[CustomPropertyDrawer(typeof(LabelAttribute))]
	public sealed class LabelDrawer : PropertyDrawer
	{
		/// <summary>
		/// LabelAttributeの描画を行います
		/// </summary>
		/// <param name="position">GUI を描画するスクリーン上の Rect</param>
		/// <param name="property">対象となるシリアライズ化されたプロパティー</param>
		/// <param name="label">文字列、テクスチャ、ツールチップ</param>
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginProperty(position, label, property);
			var labelAttribute = attribute as LabelAttribute;
			EditorGUI.PropertyField(position, property, new GUIContent(labelAttribute.Label));
			EditorGUI.EndProperty();
		}
	}
#endif
}