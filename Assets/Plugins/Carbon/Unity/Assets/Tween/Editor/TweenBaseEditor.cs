using UnityEditor;
using UnityEngine;

namespace Carbon
{
	[CustomEditor(typeof(TweenBase), true)]
	public class TweenBehaviourEditor : Editor
	{
		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// SerializedProperty
		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		private SerializedProperty msp_Settings;

		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// Method
		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		private void OnEnable()
		{
			// Fetch the objects from the MyScript script to display in the inspector
			msp_Settings = serializedObject.FindProperty("m_Settings");
		}

		public override void OnInspectorGUI()
		{
			GUILayout.Space(6f);
			EditorUtils.SetLabelWidth(110f);
			base.OnInspectorGUI();
			DrawCommonProperties();
		}

		protected void DrawCommonProperties()
		{
			TweenBase tw = target as TweenBase;

			// other properties
			if (EditorUtils.DrawHeader("TweenBase")) {
				EditorUtils.BeginContents();
				EditorUtils.SetLabelWidth(112f);

				if (tw.Settings) {
					// GUI 変更チェックフラグ
					GUI.changed = false;

					// 処理節約のため, readonly をデフォルトモードにします.
					// editable にする際, readonly に戻すことを忘れなく.
					// * readonly : GUI.enabled = false;
					// * editable : GUI.enabled = true;

					// readonly BEGIN
					GUI.enabled = false;

					// method (readonly)
					EditorGUILayout.EnumPopup("Tween Method", tw.Method);

					// style (readonly)
					EditorGUILayout.EnumPopup("Tween Style", tw.Style);

					// factor curve (readonly)
					EditorGUILayout.CurveField("Factor Curve", tw.FactorCurve, GUILayout.Width(170f), GUILayout.Height(62f));

					// duration (readonly + editable)
					GUILayout.BeginHorizontal();
					EditorGUILayout.FloatField("Duration", tw.DurationBase, GUILayout.MaxWidth(170f));
					GUILayout.Label("+", GUILayout.MaxWidth(13f));
					GUI.enabled = true;
					float durationOffset = EditorGUILayout.FloatField(tw.DurationOffset, GUILayout.MaxWidth(58f));
					GUI.enabled = false;
					GUILayout.Label("seconds");
					GUILayout.EndHorizontal();

					// delay (readonly + editbale)
					GUILayout.BeginHorizontal();
					EditorGUILayout.FloatField("Delay", tw.DelayBase, GUILayout.MaxWidth(170f));
					GUILayout.Label("+", GUILayout.MaxWidth(13f));
					GUI.enabled = true;
					float delayOffset = EditorGUILayout.FloatField(tw.DelayOffset, GUILayout.MaxWidth(58f));
					GUI.enabled = false;
					GUILayout.Label("seconds");
					GUILayout.EndHorizontal();

					// usesRealTime (readonly)
					EditorGUILayout.Toggle("Use RealTime", tw.UsesRealTime);

					// updatesSmoothly (readonly)
					EditorGUILayout.Toggle("Update Smoothly", tw.UpdatesSmoothly);

					// readonly END
					GUI.enabled = true;

					if (GUI.changed) {
						EditorUtils.RegisterUndo("Tween Change", tw);
						tw.DurationOffset = durationOffset;
						tw.DelayOffset    = delayOffset;
						EditorUtility.SetDirty(tw);
					}
				}
				else {
					// GUI 変更チェックフラグ
					GUI.changed = false;

					// method
					Tween.Method method = (Tween.Method)EditorGUILayout.EnumPopup("Tween Method", tw.Method);

					// style
					Tween.Style style = (Tween.Style)EditorGUILayout.EnumPopup("Tween Style", tw.Style);

					// factor curve
					AnimationCurve factorCurve = EditorGUILayout.CurveField("Factor Curve", tw.FactorCurve, GUILayout.Width(170f), GUILayout.Height(62f));

					// duration
					GUILayout.BeginHorizontal();
					float durationBase = EditorGUILayout.FloatField("Duration", tw.DurationBase, GUILayout.MaxWidth(170f));
					GUILayout.Label("+", GUILayout.MaxWidth(13f));
					float durationOffset = EditorGUILayout.FloatField(tw.DurationOffset, GUILayout.MaxWidth(58f));
					GUILayout.Label("seconds");
					GUILayout.EndHorizontal();

					// delay
					GUILayout.BeginHorizontal();
					float delayBase = EditorGUILayout.FloatField("Delay Base", tw.DelayBase, GUILayout.MaxWidth(170f));
					GUILayout.Label("+", GUILayout.MaxWidth(13f));
					float delayOffset = EditorGUILayout.FloatField(tw.DelayOffset, GUILayout.MaxWidth(58f));
					GUILayout.Label("seconds");
					GUILayout.EndHorizontal();

					// usesRealTime
					bool usesRealTime = EditorGUILayout.Toggle("Use RealTime", tw.UsesRealTime);

					// updateSmootyly
					bool updateSmootyly = tw.UpdatesSmoothly;
					if (EditorApplication.isPlayingOrWillChangePlaymode) {
						EditorGUILayout.Toggle("Update Smoothly", tw.UpdatesSmoothly);
					}
					else {
						updateSmootyly = EditorGUILayout.Toggle("Update Smoothly", tw.UpdatesSmoothly);
					}

					// beginsOnEnable
					bool beginsOnEnable = EditorGUILayout.Toggle("Begin on Enable", tw.BeginsOnEnable);

					if (GUI.changed) {
						EditorUtils.RegisterUndo("Tween Change", tw);
						tw.Method			= method;
						tw.Style			= style;
						tw.FactorCurve		= factorCurve;
						tw.BeginsOnEnable	= beginsOnEnable;
						tw.UsesRealTime		= usesRealTime;
						tw.UpdatesSmoothly	= updateSmootyly;
						tw.DurationBase		= durationBase;
						tw.DurationOffset	= durationOffset;
						tw.DelayBase		= delayBase;
						tw.DelayOffset		= delayOffset;
						EditorUtility.SetDirty(tw);
					}
				}

				// settings (sub-window)
				if (EditorUtils.DrawHeader("TweenSettings")) {
					EditorUtils.BeginContents();

					GUI.changed = false;

					// property
					EditorGUILayout.PropertyField(msp_Settings, new GUIContent(string.Empty), true);
					serializedObject.ApplyModifiedProperties();

					// reapply-settings button
					bool forceReapply = false;
					if (tw.Settings) {
						forceReapply = GUILayout.Button("Reapply Settings");
					}

					// reapply if forced or changed
					if (forceReapply || GUI.changed) {
						tw.ReapplySettings();
						EditorUtility.SetDirty(tw);
					}

					EditorUtils.EndContents();
				}

				EditorUtils.EndContents();
			}

			// Apply changes to the serializedProperty - always do this in the end of OnInspectorGUI.
			serializedObject.ApplyModifiedProperties();
		}
	}
}