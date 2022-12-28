using UnityEngine;
using UnityEditor;

namespace Carbon
{
	/// <summary>
	/// Editor Utilities (from NGUI)
	/// </summary>
	public static class EditorUtils
	{
		#region ContentArea
		/// <summary>
		/// Begin drawing the content area.
		/// </summary>
		public static void BeginContents()
		{
			GUILayout.BeginHorizontal();
			EditorGUILayout.BeginHorizontal("TextArea", GUILayout.MinHeight(10f));
			GUILayout.BeginVertical();
			GUILayout.Space(2f);
		}

		/// <summary>
		/// End drawing the content area.
		/// </summary>
		public static void EndContents()
		{
			GUILayout.Space(3f);
			GUILayout.EndVertical();
			EditorGUILayout.EndHorizontal();
			GUILayout.Space(3f);
			GUILayout.EndHorizontal();
			GUILayout.Space(3f);
		}
		#endregion

		/// <summary>
		/// Create an undo point for the specified objects.
		/// </summary>
		public static void RegisterUndo(string name, params Object[] objects)
		{
			if (objects != null && objects.Length > 0) {
				Undo.RecordObjects(objects, name);

				foreach (Object obj in objects) {
					if (obj == null)
						continue;
					EditorUtility.SetDirty(obj);
				}
			}
		}

		/// <summary>
		/// Draw a distinctly different looking header label
		/// </summary>
		public static bool DrawHeader(string text, string key, bool forceOn)
		{
			bool state = EditorPrefs.GetBool(key, true);

			GUILayout.Space(3f);

			if (!forceOn && !state) {
				GUI.backgroundColor = new Color(0.8f, 0.8f, 0.8f);
			}

			GUILayout.BeginHorizontal();
			GUI.changed = false;

			text = "<b><size=11>" + text + "</size></b>";
			if (state) {
				text = "\u25BC " + text;
			}
			else {
				text = "\u25BA " + text;
			}

			if (!GUILayout.Toggle(true, text, "dragtab", GUILayout.MinWidth(20f)))
				state = !state;

			if (GUI.changed) {
				EditorPrefs.SetBool(key, state);
			}

			GUILayout.Space(2f);
			GUILayout.EndHorizontal();
			GUI.backgroundColor = Color.white;
			if (!forceOn && !state) {
				GUILayout.Space(3f);
			}
			return state;
		}

		/// <summary>
		/// Draw a distinctly different looking header label
		/// </summary>
		public static bool DrawHeader(string text)
		{
			return DrawHeader(text, text, false);
		}

		/// <summary>
		/// Draw a button with given label
		/// </summary>
		public static bool DrawPrefixButton(string text, float width = 76f)
		{
			return GUILayout.Button(text, "DropDown", GUILayout.Width(width));
		}

		/// <summary>
		/// Set label's width
		/// </summary>
		/// <param name="width"></param>
		public static void SetLabelWidth(float width)
		{
			EditorGUIUtility.labelWidth = width;
		}
	}
}