using UnityEngine;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Carbon
{
	/// <summary>
	/// Inspector enumで指定されたものを文字列で置き換えます
	/// </summary>
	/// <example>
	/// <code>
	/// [SerializeField, EnumStringProperty(typeof(Type))]
	/// private Type m_TypeString;
	/// </code>
	/// </example>
	public class EnumStringPropertyAttribute : PropertyAttribute
	{
		public Type type { get { return _type; } }

		public EnumStringPropertyAttribute(Type type)
		{
			_type = type;
		}

		private Type _type = null;
	}

#if UNITY_EDITOR
	/// <summary>
	/// Inspector に EnumStringPropertyAttribute 付き項目を描画します.
	/// </summary>
	[CustomPropertyDrawer(typeof(EnumStringPropertyAttribute))]
	public class EnumStringPropertyDrawer : PropertyDrawer
	{
		public new EnumStringPropertyAttribute attribute { get { return (EnumStringPropertyAttribute)base.attribute; } }

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			if (property.propertyType == SerializedPropertyType.String)
			{
				property.stringValue = GetValue(position, property, label);
				return;
			}

			EditorGUI.LabelField(position, label, "EnumStringProperty must be string.");
		}

		/// <summary>
		/// 値を取得
		/// </summary>
		private string GetValue(Rect position, SerializedProperty property, GUIContent label)
		{
			var type = attribute.type;

			Enum selected = null;

			if (Enum.IsDefined(type, property.stringValue))
			{
				selected = (Enum)Enum.Parse(type, property.stringValue);
			}
			else
			{
				// デフォルト値（0）を取得
				selected = (Enum)Activator.CreateInstance(type);
			}

			var value = EditorGUI.EnumPopup(position, label, selected);
			return value.ToString();
		}
	}
#endif
}