using UnityEngine;
using System.Collections.Generic;
using TMPro;
using Carbon;

namespace Shock
{
    /// <summary>
    /// テキストメッシュテスト
    /// </summary>
    [RequireComponent(typeof(CanvasRenderer))]
    [DisallowMultipleComponent]
	[ExecuteInEditMode]
	public sealed class TextMeshUIPerspective : MonoBehaviour
    {
		//============================================
		// メンバー変数(SerializeField)
		//============================================
		[SerializeField] private TMP_Text		m_tmpText			= null;     // パースさせるやつ
		[SerializeField] private CanvasRenderer	m_canvasRenderer	= null;     // キャンバス描画
		[SerializeField] private float			m_minRange			= 0;		// 最小パース係数(この値～１の間でずらす)
		[SerializeField] private bool			m_playOnStart		= false;	// 最初にパースを反映させるか

		//============================================
		// メンバー変数
		//============================================
		private bool m_isReady;


		//--------------------------------------------
		// Monobehaviour
		//--------------------------------------------
		/// <summary>
		/// 開始処理
		/// </summary>
		void Start()
		{
			if( !m_playOnStart ) return;
			RegisterWillRenderCanvases();
		}

		/// <summary>
		/// 破棄処理
		/// </summary>
		private void OnDestroy()
		{
			RemoveWillRenderCanvases();
		}

		/// <summary>
		/// Reset処理
		/// </summary>
		private void Reset()
		{
			m_minRange		 = 0.9f;
			m_canvasRenderer = gameObject.GetComponent<CanvasRenderer>();
			m_tmpText		 = gameObject.GetComponent<TMP_Text>();
		}

		//--------------------------------------------
		// public
		//--------------------------------------------
		/// <summary>
		/// Canvas.willRenderCanvasesに登録します
		/// </summary>
		public void RegisterWillRenderCanvases()
		{
			if( m_isReady )
			{
				return;
			}
			m_isReady = true;
			Canvas.willRenderCanvases += SetPerspectiveMesh;
		}

		/// <summary>
		/// Canvas.willRenderCanvasesからイベントを取り除きます
		/// </summary>
		public void RemoveWillRenderCanvases()
		{
			Canvas.willRenderCanvases -= SetPerspectiveMesh;
			m_isReady = false;
		}

		//--------------------------------------------
		// private
		//--------------------------------------------
		/// <summary>
		/// パースを設定する
		/// </summary>
		private void SetPerspectiveMesh()
		{
			var renderer = m_canvasRenderer != null ? m_canvasRenderer : this.GetComponent<CanvasRenderer>();
			var mesh = new Mesh();
			var text = m_tmpText;
			var list = new List<Vector3>();

			for (int i = 0; i < text.mesh.vertexCount; i++)
			{
				var v = text.mesh.vertices[i];
				list.Add(new Vector3(CalcVerticsePos(v.x, v.y, i, (int)(text.rectTransform.GetRectWidth() / 2f)), v.y, v.z));
			}
			mesh.vertices	= list.ToArray();
			mesh.triangles	= text.mesh.triangles;
			mesh.uv			= text.mesh.uv;
			mesh.uv2		= text.mesh.uv2;
			mesh.uv3		= text.mesh.uv3;
			mesh.uv4		= text.mesh.uv4;

			renderer.SetMesh(mesh);
			renderer.SetColor( m_tmpText.color );
		}

		/// <summary>
		/// 座標計算
		/// </summary>
		private float CalcVerticsePos( float xPos, float yPos, int index, int max )
		{
			var val = index % 4;
			if ( val != 0 && val != 3 ) { return xPos; } // 頂点的に下になるものだけ抽出

			// 中心からの座標になってるならたぶん以下で絶対値の距離からの算出でいける
			var dis = Mathf.Abs( xPos );
			var r = ( 1f - m_minRange ) * dis / max;
			var range = Mathf.Clamp( 1f - r, m_minRange, 1f );
			return xPos * range;
		}
	}
}