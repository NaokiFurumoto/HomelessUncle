using UnityEngine.UI;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Shock
{
	/// <summary>
	/// 透明なGraphicコンポーネント
	/// </summary>
	[DisallowMultipleComponent]
	public class TransparentGraphic : Graphic
	{
		//--------------------------------------------
		// protected override
		//--------------------------------------------
		/// <summary>
		/// UI 要素が頂点を生成する必要がある際に呼び出される
		/// </summary>
		protected override void OnPopulateMesh( VertexHelper vertexHelper )
		{
			// 処理負荷軽減. そもそも transparent にメッシュ生成する必要ある?
			//base.OnPopulateMesh( vertexHelper );
			vertexHelper.Clear();
		}
		
#if UNITY_EDITOR
		/// <summary>
		/// Reset処理
		/// </summary>
		protected override void Reset()
		{ 
			base.Reset();
			raycastTarget = true;
		}

		/// <summary>
		/// 選択中のみギズモ描画
		/// </summary>
		void OnDrawGizmosSelected()
		{
			DrawGizmos();
		}

		/// <summary>
		/// ギズモ描画処理
		/// </summary>
		private void DrawGizmos()
		{
			var position		= rectTransform.transform.position;
			var localPosition	= rectTransform.transform.localPosition;
			var lossyScale		= rectTransform.transform.lossyScale;
			var sizeDelta		= rectTransform.sizeDelta;
			var pivot			= rectTransform.pivot;

			float xOffset = position.x - localPosition.x;
			float yOffset = position.y - localPosition.y;

			float left	= (localPosition.x - (sizeDelta.x * pivot.x) * lossyScale.x) + xOffset;
			float right = (localPosition.x + (sizeDelta.x * (1.0f - pivot.x) * lossyScale.x)) + xOffset;
			float top	= (localPosition.y + (sizeDelta.y * (1.0f - pivot.y) * lossyScale.y)) + yOffset;
			float under = (localPosition.y - (sizeDelta.y * pivot.y) * lossyScale.y) + yOffset;

			Gizmos.color = Color.green;

			Gizmos.DrawLine(new Vector3(left	, top	, 0.0f), new Vector3(right	, top	, 0.0f));
			Gizmos.DrawLine(new Vector3(right	, top	, 0.0f), new Vector3(right	, under	, 0.0f));
			Gizmos.DrawLine(new Vector3(right	, under	, 0.0f), new Vector3(left	, under	, 0.0f));
			Gizmos.DrawLine(new Vector3(left	, under	, 0.0f), new Vector3(left	, top	, 0.0f));
		}

		/// <summary>
		/// インスペクター拡張
		/// </summary>
		[CustomEditor(typeof(TransparentGraphic))]
		class RaycastObjectEditor : Editor
		{
			public override void OnInspectorGUI()
			{
				var graphic = target as TransparentGraphic;

				graphic.raycastTarget = EditorGUILayout.Toggle( "Raycast Target", graphic.raycastTarget );
			}
		}
#endif
	}
}
