using Carbon;
using UnityEngine;
using UnityEngine.UI;

namespace Shock
{
	/// <summary>
	/// スクロールさせるUIの親クラス
	/// </summary>
	public sealed class ScrollItemParent : RectTransformBehaviour
	{
		//============================================
		//! メンバー変数
		//============================================
		[SerializeField] private HorizontalOrVerticalLayoutGroup	m_layoutGroup;
		[SerializeField] private bool								m_isHorizontal;

		//============================================
		//! プロパティ
		//============================================
		public int Index		{ get; private set; }
		public int InstanceId	{ get { return gameObject.GetInstanceID();	} }


		//--------------------------------------------
		// private
		//--------------------------------------------
		/// <summary>
		/// 初期化します
		/// </summary>
		private void Init( bool isHorizontal )
		{
			if( m_layoutGroup != null )
			{
				return;
			}

			m_isHorizontal = isHorizontal;

			if( !m_isHorizontal )
			{
				m_layoutGroup					= gameObject.AddComponent<HorizontalLayoutGroup>();
				m_layoutGroup.childAlignment	= TextAnchor.MiddleLeft;
			}
			else
			{
				m_layoutGroup					= gameObject.AddComponent<VerticalLayoutGroup>();
				m_layoutGroup.childAlignment	= TextAnchor.UpperCenter;
			}

			m_layoutGroup.childControlWidth			= false;
			m_layoutGroup.childControlHeight		= false;
			m_layoutGroup.childForceExpandWidth		= false;
			m_layoutGroup.childForceExpandHeight	= false;
		}

		//--------------------------------------------
		// public
		//--------------------------------------------
		/// <summary>
		/// LayoutGroup反映
		/// </summary>
		public void ApplyLayoutGroup( int gridCount, int totalPrefabSize )
		{
			// Spacing設定
			var rect	 = rectTransform.rect;
			var rectSize = m_isHorizontal ? rect.height	: rect.width;
			var spacing	 = ( rectSize - totalPrefabSize ) / gridCount;
			m_layoutGroup.spacing = spacing;

			// 開始位置を調整
			var halfSpacing = spacing * 0.5f;
			if( m_isHorizontal )
			{
				m_layoutGroup.padding.top = (int)halfSpacing;
			}
			else
			{
				m_layoutGroup.padding.left = (int)halfSpacing;
			}

			// 子のLayoutElementの位置調整
			m_layoutGroup.CalculateLayoutInputHorizontal();
			m_layoutGroup.CalculateLayoutInputVertical	();
			m_layoutGroup.SetLayoutHorizontal			();
			m_layoutGroup.SetLayoutVertical				();

			m_layoutGroup.enabled = false;
		}

		/// <summary>
		/// インデックス値を設定します
		/// </summary>
		public void SetIndex( int index )
		{
			Index = index;
		}

		//--------------------------------------------
		// public static
		//--------------------------------------------
		/// <summary>
		/// ScrollItemParentを作成して返します
		/// </summary>
		/// <param name="parent">				親となるRectTransform							</param>
		/// <param name="cachedRectTransform">	RectTransformの設定値(Anchor、Pivot、SizeDelta)	</param>
		/// <param name="isHorizontal">			スクロール方向									</param>
		public static ScrollItemParent Create(
			RectTransform			parent		,
			RectTransformBehaviour	content		,
			Vector2					pivot		,
			bool					isHorizontal
		)
		{
			// ゲームオブジェクト作成
			var gameObject	 = new GameObject( "ScrollItemParent" );
			var scrollParent = gameObject.AddComponent<ScrollItemParent>();
			scrollParent.SetParent	( parent, false );
			scrollParent.SetLayer	( parent );
			scrollParent.ResetLocalTransform();

			// Anchor、Size、Pivot設定
			scrollParent.AdaptAnchor ( content.rectTransform );
			scrollParent.SetSizeDelta( content.sizeDelta );
			scrollParent.SetPivot	 ( pivot );

			if( !isHorizontal )
			{
				scrollParent.SetSizeDeltaY( 0 );
			}
			else
			{
				scrollParent.SetSizeDeltaX( 0 );
			}

			// ScrollItemParentコンポーネント初期化
			scrollParent.Init( isHorizontal );

			return scrollParent;
		}
	}
}