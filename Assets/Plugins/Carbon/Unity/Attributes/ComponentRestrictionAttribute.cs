using System;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Carbon
{
	public sealed class ComponentRestrictionAttribute : PropertyAttribute
	{
		public Type Type;
		public ComponentRestrictionAttribute(Type type)
		{
			Type = type;
		}
	}

#if UNITY_EDITOR
	[CustomPropertyDrawer(typeof(ComponentRestrictionAttribute))]
	public class ComponentRestrictionDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			var restriction = (ComponentRestrictionAttribute)attribute;

			if (property.propertyType == SerializedPropertyType.ObjectReference) {
				EditorGUI.ObjectField(position, property, restriction.Type);
			}
			else {
				EditorGUI.PropertyField(position, property);
			}
		}
	}
#endif
}
