using Carbon;
using System;
using UnityEngine;

namespace Shock
{
	/// <summary>
	/// ScrollRectのContent登録するクラス
	/// </summary>
	[DisallowMultipleComponent]
	public sealed class ScrollContent : RectTransformBehaviour
	{
		//==================================================
		// メンバー変数
		//==================================================
		private bool	m_IsInit		= false;
		private float	m_Interval		= 0;        // UI配置の間隔
		private float	m_TopInterval	= 0;        // 上と下で基準位置違うので合わせる用
		private float	m_OffsetPos		= 0;        // 初期配置された時からのスクロール移動量
		private long	m_ScrollIndex	= 0;        // スクロールのインデックス値
		private float	m_ContentOffsetPos	= 0;    // コンテンツのオフセット

		//==================================================
		// イベント
		//==================================================
		// スクロールのインデックス値が更新された時に呼ばれるイベント
		public Action<bool,long> OnUpdateScrollIndex { private get; set; }
		
		//==================================================
		// プロパティ
		//==================================================
		public float AnchoredDirPosition	=> IsHorizontal ? anchoredPosition.x : -anchoredPosition.y;
		public float DefaultSize			=> IsHorizontal ? savedSizeDelta.x : savedSizeDelta.y;

		private bool IsHorizontal	{ get; set; } = false;	// 横方向にスクロールするのか
		private bool IsVertical		=> !IsHorizontal;		// 縦方向にスクロールするのか

		//--------------------------------------------------
		// Monobehaviour
		//--------------------------------------------------
		private void OnDestroy()
		{
			OnUpdateScrollIndex = null;
		}

		//--------------------------------------------
		// public
		//--------------------------------------------
		/// <summary>
		/// 初期化処理
		/// </summary>
		public void Init( bool isHorizontal, float interval, float contentOffset )
		{
			// RectTransformの初期値を保持します(初回のみ)
			SaveValue();

			// 各種初期化
			m_IsInit		= true;
			m_Interval		= interval;

			IsHorizontal	= isHorizontal;
			m_TopInterval	= m_Interval;
			m_OffsetPos		= 0f;
			m_ContentOffsetPos = contentOffset;
			m_ScrollIndex	= 0;

			SetAnchoredPosition( savedAnchoredPosition );
		}

		/// <summary>
		/// クリア
		/// </summary>
		public void Clear()
		{
			m_IsInit		= false;
			m_Interval		= 0;
			m_TopInterval	= 0;
			m_OffsetPos		= 0;
			m_ScrollIndex	= 0;
			m_ContentOffsetPos	= 0;

			IsHorizontal	= false;
		}

		/// <summary>
		/// 毎フレーム更新処理
		/// </summary>
		public void DoUpdate()
		{
			if( !m_IsInit )
			{
				return;
			}

			// 先頭の要素を末尾へ移動
			while( AnchoredDirPosition - m_OffsetPos < -(m_TopInterval + m_ContentOffsetPos))
			{
				m_OffsetPos -= m_Interval;
				OnUpdateScrollIndex.Call( false, m_ScrollIndex );
				m_ScrollIndex++;
			}

			// 末尾の要素を先頭に移動
			while (AnchoredDirPosition - m_OffsetPos > 0 - m_ContentOffsetPos)
			{
				m_OffsetPos += m_Interval;
				m_ScrollIndex--;
				OnUpdateScrollIndex.Call(true, m_ScrollIndex);
			}
		}

		/// <summary>
		/// スクロールする方向(XorY)のAnchoredPositionに値を加算します
		/// </summary>
		public void AddAnchoredPosition( float pos )
		{
			var addPos = IsHorizontal ? new Vector2( pos, 0 ) : new Vector2( 0, -pos );
			AddAnchoredPosition(addPos);
		}

		/// <summary>
		/// 矩形のサイズ設定
		/// </summary>
		public void SetSize( float size )
		{
			var sizeDeita = IsHorizontal ? new Vector2( size, 0 ) : new Vector2( 0, size );
			//オフセット分のずらしたのでその分スクロールできるように対応
			//var addContentOffsetSize = IsHorizontal ? new Vector2(m_ContentOffsetPos + (100 - it), 0) : new Vector2(0, m_ContentOffsetPos * 2f);
			//SetSizeDelta( sizeDeita + addContentOffsetSize);
			SetSizeDelta(sizeDeita);
		}
	}
}
