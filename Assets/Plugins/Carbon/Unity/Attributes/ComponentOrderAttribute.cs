using System;
using System.Linq;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditorInternal;
#endif

namespace Carbon
{
	[AttributeUsage(AttributeTargets.Class)]
	public class ComponentOrderAttribute : Attribute
	{
		public enum ORDER_TYPE
		{
			TYPE,
			NUMBER,
		}

		public uint Order { get; } = 0;
		public Type Type { get; } = null;
		public ORDER_TYPE OrderType { get; } = ORDER_TYPE.NUMBER;

		public ComponentOrderAttribute(uint order)
		{
			Order = order;
			OrderType = ORDER_TYPE.NUMBER;
		}

		public ComponentOrderAttribute(Type type)
		{
			Type = type;
			OrderType = ORDER_TYPE.TYPE;
		}
	}

#if UNITY_EDITOR
	[InitializeOnLoad]
	public class ComponentOrder
	{
		static string lastInstanceIDs = "";
		static ComponentOrder()
		{
			EditorApplication.update += () => {
				if (Selection.gameObjects.Length == 0)
					return;

				foreach (var components in Selection.gameObjects.Select(gameObject => gameObject.GetComponents<Component>()))
				{
					if (lastInstanceIDs == string.Join(",", components.Select(c => c.GetInstanceID().ToString()).ToArray()))
						return;

					for (var i = 1; i < components.Length; i++)
					{
						var component = components[i];
						var attribute = GetComponentOrderAttribute(component.GetType());
						if (attribute == null)
							continue;

						if (attribute.OrderType == ComponentOrderAttribute.ORDER_TYPE.NUMBER)
						{
							if (attribute.Order < i) {
								if (components[i - 1].GetType() == component.GetType())
									continue;

								var _attribute = GetComponentOrderAttribute(components[i - 1].GetType());

								if (_attribute != null && _attribute.Order <= attribute.Order)
									continue;

								ComponentUtility.MoveComponentUp(component);
							}
							else if (attribute.Order > i) {
								if (i != components.Length - 1) {
									var _attribute = GetComponentOrderAttribute(components[i + 1].GetType());

									if (_attribute != null && _attribute.Order >= attribute.Order)
										continue;
								}

								ComponentUtility.MoveComponentDown(component);
							}
						}
						else if (attribute.OrderType == ComponentOrderAttribute.ORDER_TYPE.TYPE)
						{
							int targetComponentIndex = components.FindLastIndex(c => c.GetType() == attribute.Type);
							if (targetComponentIndex < 0 || targetComponentIndex == i)
							{
								continue;
							}

							if (targetComponentIndex < i && targetComponentIndex + 1 != i)
							{
								var _attribute = GetComponentOrderAttribute(components[i - 1].GetType());
								if (_attribute != null && _attribute.Order <= attribute.Order)
									continue;

								ComponentUtility.MoveComponentUp(component);
							}
							else if (targetComponentIndex > i)
							{
								if (i != components.Length - 1)
								{
									var _attribute = GetComponentOrderAttribute(components[i + 1].GetType());
									if (_attribute != null && _attribute.Order >= attribute.Order)
										continue;
								}

								ComponentUtility.MoveComponentDown(component);
							}
						}
					}
					lastInstanceIDs = string.Join(",", components.Select(c => c.GetInstanceID().ToString()).ToArray());
				}
			};
		}

		static ComponentOrderAttribute GetComponentOrderAttribute(Type type)
		{
			return type.GetCustomAttributes(typeof(ComponentOrderAttribute), true).Cast<ComponentOrderAttribute>().FirstOrDefault();
		}
	}
#endif
}