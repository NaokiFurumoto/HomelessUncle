using UnityEngine;
using UnityEngine.EventSystems;
using Carbon;
using UniRx;
using System;

namespace Shock
{
	/// <summary>
	/// ScrollRectのイベント継承用コンポーネント
	/// </summary>
	[DisallowMultipleComponent]
	public sealed class ScrollRectUI : UnityEngine.UI.ScrollRect
	{
		//============================================
		//! 定数
		//============================================
		private const int ElasticMoveTimeFrame = 30;

		//============================================
		//! メンバー変数
		//============================================
		private IDisposable m_elasticTimer;
		private PointerEventData m_CurrentPointerEventData = null;

		//============================================
		//! プロパティ
		//============================================
		public bool		IsDrag			{ get; private set; }
		public	bool	IsElasticMove	{ get; private set; }
		public  bool	IsHorizontal	{ get { return horizontal;											} }	// 横方向にスクロールするのか
		public  bool	IsVertical		{ get { return vertical;											} } // 縦方向にスクロールするのか
		public  float	Velocity		{ get { return Mathf.Abs( IsHorizontal ? velocity.x : velocity.y ); } } // 加速度
		private bool	IsElastic		{ get { return movementType == MovementType.Elastic;				} }	// Elasticタイプなのか
		public float	NormalizedPosition { get { return IsVertical ? verticalNormalizedPosition : horizontalNormalizedPosition; } }
		//============================================
		//! コールバック
		//============================================
		public Action BeginDrag { private get; set; }

		//--------------------------------------------
		// Monobehaviour
		//--------------------------------------------
		/// <summary>
		/// Reset処理
		/// </summary>
		new void Reset()
		{
			// とりあえず良い感じのパラメータ入れておく
			movementType		= MovementType.Unrestricted;
			decelerationRate	= 0.2f;
			scrollSensitivity	= 7f;

			// コンポーンネント設定
			var scrollContent = GetComponentInChildren<ScrollContent>();
			if( scrollContent == null )
			{
				return;
			}
			content = scrollContent.GetComponent<RectTransform>();
		}
		
		/// <summary>
		/// ドラッグ開始時に呼ばれるイベント
		/// </summary>
		public override void OnBeginDrag(PointerEventData eventData)
		{
			m_CurrentPointerEventData = eventData;
			base.OnBeginDrag (eventData);
			BeginDrag.Call();
			IsDrag			= true;
			IsElasticMove	= false;

			m_elasticTimer.TryDispose();
		}

		/// <summary>
		/// ドラッグ時に呼ばれるイベント
		/// </summary>
		public override void OnDrag(PointerEventData eventData)
		{
			if (IsDrag) {
				m_CurrentPointerEventData = eventData;
				base.OnDrag(eventData);
			}
		}

		/// <summary>
		/// ドラッグ終了時に呼ばれるイベント
		/// </summary>
		public override void OnEndDrag(PointerEventData eventData)
		{
			m_CurrentPointerEventData = eventData;
			base.OnEndDrag (eventData);
			IsDrag = false;

			if( !IsElastic )
			{
				return;
			}

			IsElasticMove  = true;
			m_elasticTimer = Observable.TimerFrame( ElasticMoveTimeFrame, FrameCountType.Update )
									   .Subscribe ( _=> IsElasticMove = false )
									   .AddTo(this);
		}

		public void CancelDrag()
		{
			IsDrag = false;
		}

		public void ForceDragEnd()
		{
			if(m_CurrentPointerEventData != null)
			{
				CancelDrag();
				//強制ドラッグ終了
				ExecuteEvents.Execute(m_CurrentPointerEventData.pointerDrag, m_CurrentPointerEventData, ExecuteEvents.endDragHandler);
			}
		}

		//--------------------------------------------
		// private
		//--------------------------------------------
		/// <summary>
		/// スクロールバーのアクティブ設定
		/// </summary>
		private void SetActiveScrollBar( bool isActive )
		{
			verticalScrollbar  .TrySetActive( isActive );
			horizontalScrollbar.TrySetActive( isActive );
		}

		//--------------------------------------------
		// public
		//--------------------------------------------
		/// <summary>
		/// コンポーネントを有効/無効設定
		/// </summary>
		public void SetEnabled( bool enabled )
		{
			// コンポーネント有効/無効設定
			this.enabled = enabled;

			// スクロールバー表示/非表示設定
			SetActiveScrollBar( enabled );
		}
	}
}