using Carbon;
using UnityEngine;
using UnityEngine.UI;

public sealed class ScrollItemIconParent : RectTransformBehaviour
{
	//============================================
	//! メンバー変数
	//============================================
	[SerializeField] private HorizontalOrVerticalLayoutGroup layoutGroup;
	[SerializeField] private bool isHorizontal;

	//============================================
	//! プロパティ
	//============================================
	public int Index { get; private set; }
	public int InstanceId { get { return gameObject.GetInstanceID(); } }


	//--------------------------------------------
	// private
	//--------------------------------------------
	/// <summary>
	/// 初期化します
	/// </summary>
	private void Init(bool isHorizontal)
	{
		if (layoutGroup != null)
		{
			return;
		}

		this.isHorizontal = isHorizontal;

		if (!this.isHorizontal)
		{
			layoutGroup = gameObject.AddComponent<HorizontalLayoutGroup>();
			layoutGroup.childAlignment = TextAnchor.MiddleLeft;
		}
		else
		{
			layoutGroup = gameObject.AddComponent<VerticalLayoutGroup>();
			layoutGroup.childAlignment = TextAnchor.UpperCenter;
		}

		layoutGroup.childControlWidth = false;
		layoutGroup.childControlHeight = false;
		layoutGroup.childForceExpandWidth = false;
		layoutGroup.childForceExpandHeight = false;
	}

	//--------------------------------------------
	// public
	//--------------------------------------------
	/// <summary>
	/// LayoutGroup反映
	/// </summary>
	public void ApplyLayoutGroup(int gridCount, int totalPrefabSize)
	{
		// Spacing設定
		var rect = rectTransform.rect;
		var rectSize = isHorizontal ? rect.height : rect.width;
		var spacing = (rectSize - totalPrefabSize) / gridCount;
		layoutGroup.spacing = spacing;

		// 開始位置を調整
		var halfSpacing = spacing * 0.5f;
		if (isHorizontal)
		{
			layoutGroup.padding.top = (int)halfSpacing;
		}
		else
		{
			layoutGroup.padding.left = (int)halfSpacing;
		}

		// 子のLayoutElementの位置調整
		layoutGroup.CalculateLayoutInputHorizontal();
		layoutGroup.CalculateLayoutInputVertical();
		layoutGroup.SetLayoutHorizontal();
		layoutGroup.SetLayoutVertical();

		layoutGroup.enabled = false;
	}

	/// <summary>
	/// インデックス値を設定します
	/// </summary>
	public void SetIndex(int index)
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
	public static ScrollItemIconParent Create(
		RectTransform parent,
		RectTransformBehaviour content,
		Vector2 pivot,
		bool isHorizontal
	)
	{
		// ゲームオブジェクト作成
		var gameObject = new GameObject("ScrollItemIconParent");
		var scrollParent = gameObject.AddComponent<ScrollItemIconParent>();
		scrollParent.SetParent(parent, false);
		scrollParent.SetLayer(parent);
		scrollParent.ResetLocalTransform();

		// Anchor、Size、Pivot設定
		scrollParent.AdaptAnchor(content.rectTransform);
		scrollParent.SetSizeDelta(content.sizeDelta);
		scrollParent.SetPivot(pivot);

		if (!isHorizontal)
		{
			scrollParent.SetSizeDeltaY(0);
		}
		else
		{
			scrollParent.SetSizeDeltaX(0);
		}

		// ScrollItemParentコンポーネント初期化
		scrollParent.Init(isHorizontal);

		return scrollParent;
	}
}

