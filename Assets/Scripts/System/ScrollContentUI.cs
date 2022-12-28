using Carbon;
using System;
using UnityEngine;

/// <summary>
/// ScrollRectのContent登録するクラス
/// </summary>
[DisallowMultipleComponent]
public sealed class ScrollContentUI : RectTransformBehaviour
{
    //==================================================
    // メンバー変数
    //==================================================
    private bool isInit = false;
    private float interval = 0;        // UI配置の間隔
    private float topInterval = 0;        // 上と下で基準位置違うので合わせる用
    private float offsetPos = 0;        // 初期配置された時からのスクロール移動量
    private long scrollIndex = 0;        // スクロールのインデックス値
    private float contentOffsetPos = 0;    // コンテンツのオフセット

    //==================================================
    // イベント
    //==================================================
    // スクロールのインデックス値が更新された時に呼ばれるイベント
    public Action<bool,long> OnUpdateScrollIndex { private get; set; }

    //==================================================
    // プロパティ
    //==================================================
    public float AnchoredDirPosition => IsHorizontal ? anchoredPosition.x : -anchoredPosition.y;
    public float DefaultSize => IsHorizontal ? savedSizeDelta.x : savedSizeDelta.y;
    private bool IsHorizontal { get; set; } = false;    // 横方向にスクロールするのか
    private bool IsVertical => !IsHorizontal;       // 縦方向にスクロールするのか

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
	public void Init(bool isHorizontal, float interval, float contentOffset)
	{
		// RectTransformの初期値を保持します(初回のみ)
		SaveValue();

		// 各種初期化
		isInit = true;
		this.interval = interval;

		IsHorizontal = isHorizontal;
		topInterval = interval;
		offsetPos = 0f;
		contentOffsetPos = contentOffset;
		scrollIndex = 0;

		SetAnchoredPosition(savedAnchoredPosition);
	}

	/// <summary>
	/// クリア
	/// </summary>
	public void Clear()
	{
		isInit = false;
		interval = 0;
		topInterval = 0;
		offsetPos = 0;
		scrollIndex = 0;
		contentOffsetPos = 0;

		IsHorizontal = false;
	}

	/// <summary>
	/// 毎フレーム更新処理
	/// </summary>
	public void DoUpdate()
	{
		if (!isInit)
		{
			return;
		}

		// 先頭の要素を末尾へ移動
		while (AnchoredDirPosition - offsetPos < -(topInterval + contentOffsetPos))
		{
			offsetPos -= interval;
			OnUpdateScrollIndex.Call(false, scrollIndex);
			scrollIndex++;
		}

		// 末尾の要素を先頭に移動
		while (AnchoredDirPosition - offsetPos > 0 - contentOffsetPos)
		{
			offsetPos += interval;
			scrollIndex--;
			OnUpdateScrollIndex.Call(true, scrollIndex);
		}
	}

	/// <summary>
	/// スクロールする方向(XorY)のAnchoredPositionに値を加算します
	/// </summary>
	public void AddAnchoredPosition(float pos)
	{
		var addPos = IsHorizontal ? new Vector2(pos, 0) : new Vector2(0, -pos);
		AddAnchoredPosition(addPos);
	}

	/// <summary>
	/// 矩形のサイズ設定
	/// </summary>
	public void SetSize(float size)
	{
		var sizeDeita = IsHorizontal ? new Vector2(size, 0) : new Vector2(0, size);
		SetSizeDelta(sizeDeita);
	}
}
