using Carbon;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
//using Shock;

/// <summary>
/// スクロールの方向Enum
/// </summary>
public enum ScrollDirection
{
    RIGHT = 1,
    LEFT = -1,
    UP = 1,
    DOWN = -1,
    NONE = 0
}
/// <summary>
/// スクロール基底
/// </summary>
[DisallowMultipleComponent]
public abstract class ScrollSystemBase<T> : RectTransformBehaviour where T : ItemIconBase
{
    ///定数
    private const int Margin = 2;

    //メンバ変数:SerializeField
    [SerializeField] protected ScrollRectSystem scrollRect = null;
    [SerializeField] protected ScrollContentUI scrollContentUI = null;
    [SerializeField] private ItemIconBase prefab = null; // 複製して使用するスクロールさせるUI
    [SerializeField] private float itemSize = 0f; // スクロールさせるUIのサイズ(間隔込み)
    [SerializeField] private bool isAutoFit = false; // ドラッグ後にスクロールアイテムの位置補正をするか
    [SerializeField] private int maxScrollSpeed = 0; // スクロールの最大速度
    [SerializeField] protected float thresholdVelocity = 0f; // 加速度のしきい値(この値以下になるとスクロールの停止処理が開始します)
    [SerializeField] protected float fitLength = 0f; // 位置補正時に補正するまでの距離
    [SerializeField] protected float moveScale = 0f; // スクロール停止時の速度に掛けるスケール
    [SerializeField] protected bool isLoop = false; // UIをループ表示させるか
    [SerializeField] protected bool isAutoContentPivot = true; // scrollContentUIのPivotを自動で変更するか
    [SerializeField] private int grid = 0; // グリッド
    [SerializeField] [Range(0, 100)] private int fitChangePersent = 50; //フィット方向を切り替えるパーセント
    [SerializeField] private TextMeshProUGUI emptyDataText = null;
    [SerializeField] private float minVelocity = 0.01f; // velocity最小値判定。float E対策
    [SerializeField] private bool isItemSwitchEnable = true; // スクロールによってitemを位置を交換するか（ガチャメニューではfalse）
    [SerializeField] private float contentOffsetPos = 0f; // Contentsの位置をずらすためのオフセット
    [SerializeField] private bool isScrollCurveX = false; // スクロールのカーブを有効にするかどうか
    [SerializeField] private AnimationCurve scrollCurveX = null; // スクロールのカーブ
    [SerializeField] private float scrollCurveValueX = 0f; // スクロールカーブの値
    [SerializeField] private bool isScrollCurveY = false; // スクロールのカーブを有効にするかどうか
    [SerializeField] private AnimationCurve scrollCurveY = null; // スクロールのカーブ
    [SerializeField] private float scrollCurveValueY = 0f; // スクロールカーブの値

    //メンバ変数
    private int createRequestId = 0;
    private int createImplementId = 0;
    private bool cannotUpdateScrollItem = true;

    private int dataCount;
    private int gridCount;
    private int gridDataCount;

    private float halfItemSize;
    private float fitChangePersentCache;
    private Vector2 pivotOffsetPos;
    private bool isDrag;
    private bool existsMargin;
    private bool forceScroll = false;
    private float fitPrevAnchorPos = 0f;

    private ScrollDirection scrollDirection;
    private ScrollItemIconParent scrollItemIconParent; // ScrollItemParentのオリジナル

    private bool isCentering = false;

    //ィベント:デリゲート(戻り値なし）
    public Action<T> OnInitializeItem { private get; set; }
    public Action OnInitializeItemParent { private get; set; }
    public Action<int> OnFitItem { protected get; set; }
    public Action<int, bool, Action> OnUpdateItemParent { private get; set; } = null;

    //==================================================
    // プロパティ
    //==================================================
   
    private int ItemCount => ScrollItemParents.Count;  // スクロール内のUIの数
    public bool IsHorizontal => scrollRect.IsHorizontal; // 横方向にスクロールするのか
    public bool IsScrolling => scrollRect.Velocity > minVelocity;// スクロール中かの判定
    public bool IsFitDone { get; protected set; } // Fit処理が完了しているか
    protected bool IsScrollActive// スクロールの有効設定
    {
        set { scrollRect.enabled = value; }
        get { return scrollRect.enabled; }
    } 
    protected List<ScrollItemIconParent> ScrollItemParents { get; } = new List<ScrollItemIconParent>();
    protected Dictionary<int, IList<T>> ScrollItemTable { get; } = new Dictionary<int, IList<T>>();
    protected Vector2 RectSize => rectTransform.rect.size;
    public float ItemSize
    {
        get { return itemSize; }
        set { itemSize = value; }
    }

    public Vector2 NormalizedPosition => scrollRect.normalizedPosition;
    public float VerticalNormalizedPosition => scrollRect.verticalNormalizedPosition; 
    public float HorizontalNormalizedPosition => scrollRect.horizontalNormalizedPosition;
    public TextMeshProUGUI EmptyDataText => emptyDataText; 
    public int DataCount => dataCount;// データの数
    protected bool IsCentering
    {
        get { return isCentering; }
        set { isCentering = value; }
    }
    protected ScrollDirection ScrollDirection
    {
        get { return scrollDirection; }
        set { scrollDirection = value; }
    }

    //Monobehavior
    private void Awake()
    {
        //AutoFit補間用
        scrollDirection = ScrollDirection.NONE;
        IsFitDone = true;
        scrollRect.BeginDrag = () => IsFitDone = false;
        fitChangePersentCache = fitChangePersent / 100f;
    }

    // <summary>
    /// Reset処理
    /// </summary>
    private void Reset()
    {
        // 良い感じのパラメータ入れておく
        thresholdVelocity = 300f;
        moveScale = 0.8f;
        fitLength = 50f;

        // コンポーネント設定
        scrollRect = GetComponentInChildren<ScrollRectSystem>();
        scrollContentUI = GetComponentInChildren<ScrollContentUI>();

        // prefab設定
        prefab = gameObject.GetComponentInChildren<ItemIconBase>();
    }

    /// <summary>
    /// 破棄処理
    /// </summary>
    protected virtual void OnDestroy()
    {
        OnInitializeItem = null;
        OnInitializeItemParent = null;
        OnFitItem = null;
        OnUpdateItemParent = null;
    }

    /// <summary>
    /// 毎フレーム更新処理
    /// </summary>
    protected virtual void Update()
    {
        // ScrollContentUIの更新処理
        if (isItemSwitchEnable)
        {
            scrollContentUI.DoUpdate();
        }

        //スクロール速度の制限処理
        ClampScrollSpeed();

        // スクロールアイテムの位置補正処理
        AutoFit();

        // スクロールのカーブ更新
        UpdateScrollCurve();
    }

    //--------------------------------------------------
    // public
    //--------------------------------------------------
    /// <summary>
    /// 作成済みのスクロールUIを削除します
    /// </summary>
    public void Clear()
    {
        // 作成済みのスクロールアイテム削除
        ScrollItemParents.Where(c => c != null).ForEach(c => c.DestroyGameObject());
        ScrollItemParents.Clear();
        ScrollItemTable.Clear();

        // 移動量を0にします
        scrollRect.StopMovement();
        // フィット完了フラグ
        IsFitDone = true;

        // content
        scrollContentUI.Clear();

        // 複製するUIを非アクティブ
        prefab.TryChangeActive(false);
    }

    /// <summary>
    /// 構築②
    /// </summary>
    /// <param name="dataCount">データ数</param>
    /// <param name="onComplete">構築完了コールバック</param>
    public virtual void Create(int dataCount, int startIndex, Action onComplete, bool forceScrollEnable = false)
    {
        createRequestId++;
        cannotUpdateScrollItem = true;
        // LayoutGroupリサイズするので1フレ待ちます
        CoroutineManager.CallWaitForOneFrame(() =>
        {
            Create(startIndex, dataCount, forceScrollEnable, () =>
            {
                //↓Createの中でUpdateをするようにしたので1フレ待たなくてもよくなったのでコメントアウト
                //ポジション変更がある場合にすぐに反映されないのでここでも1フレ待つ
                //CoroutineManager.CallWaitForOneFrame(onComplete);
                onComplete.Call();
            });
        });
    }

    /// <summary>
    /// 構築①
    /// </summary>
    /// <param name="dataCount">データ数</param>
    /// <param name="onComplete">構築完了コールバック</param>
    public virtual void Create(int dataCount, Action onComplete)
    {
        Create(dataCount, 0, onComplete);
    }

    /// <summary>
    /// 構築せずアイテム初期化プロセスだけを走る
    /// </summary>
    public void InitializeScrollItem()
    {
        ScrollItemParents.ForEach(parent =>
        {
            ScrollItemTable[parent.InstanceId].ForEach(scrollItem => OnInitializeItem(scrollItem));
        });
    }

    /// <summary>
    /// foreach
    /// </summary>
    public void ForEachScrollItem(Action<T> action)
    {
        ScrollItemParents.ForEach(parent => { ScrollItemTable[parent.InstanceId].ForEach(action); });
    }

    /// <summary>
    /// スクロールの速度を設定します
    /// </summary>
    /// <param name="velocity">スクロール速度</param>
    /// <param name="direction">スクロール補正方向</param>
    public void SetVelocity(float velocity, ScrollDirection direction = ScrollDirection.NONE)
    {
        // 加速度設定
        var velocityTmp = scrollRect.velocity;
        if (IsHorizontal) velocityTmp.x = velocity;
        else velocityTmp.y = velocity;
        scrollRect.velocity = velocityTmp;

        //移動方向が指定されていたら適用
        if (direction != ScrollDirection.NONE) scrollDirection = direction;
        // フィット完了フラグ
        IsFitDone = false;
    }

    /// <summary>
    /// スクロールのドラッグをキャンセル
    /// </summary>
    public void CancelDrag()
    {
        scrollRect.CancelDrag();
    }

    /// <summary>
    /// ドラッグ強制終了
    /// CancelDragだとタップを離すまでEndが呼ばれない
    /// </summary>
    public void ForceDragEnd()
    {
        scrollRect.ForceDragEnd();
    }

    /// <summary>
    /// 指定されたピクセルだけスクロールする
    /// </summary>
    public void ScrollAsPixel(float pixel)
    {
        scrollContentUI.AddAnchoredPosition(-pixel);
        fitPrevAnchorPos = scrollContentUI.AnchoredDirPosition;
    }

    /// <summary>
    /// 指定されたアイテム数だけスクロールする
    /// </summary>
    /// <param name="itemNum"></param>
    public void ScrollAsItemNum(int itemNum)
    {
        var pos = itemSize * itemNum;
        scrollContentUI.AddAnchoredPosition(-pos);
        fitPrevAnchorPos = scrollContentUI.AnchoredDirPosition;
    }

    /// <summary>
    /// 指定されたインデックスに合わせる
    /// </summary>
    public void SetIndexPosition(int index)
    {
        scrollContentUI.SetAnchoredPosition(0, 0);
        ScrollAsItemNum(index);
    }

    /// <summary>
    /// 指定アイテムを先頭に合わせるようにスクロール
    /// </summary>
    public void ScrollItemToHead(int itemIndex)
    {
        if (itemIndex < 0 || itemIndex >= dataCount)
        {
            DebugUtils.Warning("Invalid item-index to scroll-to-head: {0}", itemIndex);
            return;
        }

        // 表示範囲
        var viewSize = GetViewSize();

        // 全アイテム領域が表示範囲より狭い -> 最先頭へスクロール
        if (viewSize >= itemSize * dataCount)
        {
            SetNormalizedPosition(1f);
            return;
        }

        // 前半(先頭未満): 指定アイテムを含まない
        var firstHalfCount = itemIndex;

        // 前半がない -> 最先頭へ
        if (firstHalfCount <= 0)
        {
            SetNormalizedPosition(1f);
            return;
        }

        // 後半(先頭から): 指定アイテムを含む
        var secondHalfCount = dataCount - firstHalfCount;

        // 後半領域が表示範囲より狭い -> 最後尾へスクロール
        if (viewSize >= itemSize * secondHalfCount)
        {
            SetNormalizedPosition(0f);
            return;
        }

        // 最先頭から後ろへスクロール
        SetNormalizedPosition(1f);
        ScrollAsItemNum(firstHalfCount);
    }

    /// <summary>
    /// 指定アイテムを尻尾に合わせるようにスクロール
    /// </summary>
    public void ScrollItemToTail(int itemIndex)
    {
        if (itemIndex < 0 || itemIndex >= dataCount)
        {
            DebugUtils.Warning("Invalid item-index to scroll-to-tail: {0}", itemIndex);
            return;
        }

        var viewSize = GetViewSize();

        // 全アイテム領域が表示範囲より狭い -> 最先頭へスクロール
        if (viewSize >= itemSize * dataCount)
        {
            SetNormalizedPosition(1f);
            return;
        }

        // 前半(尻尾まで): 指定アイテムを含む
        var firstHalfCount = itemIndex + 1;

        // 前半領域が表示範囲より狭い -> 最先頭へスクロール
        if (viewSize >= itemSize * firstHalfCount)
        {
            SetNormalizedPosition(1f);
            return;
        }

        // 後半(尻尾以後): 指定アイテムを含まない
        var secondHalfCount = dataCount - firstHalfCount;

        // 後半がない -> 最後尾へスクロール
        if (secondHalfCount <= 0)
        {
            SetNormalizedPosition(0f);
            return;
        }

        // 最後尾から前へスクロール
        SetNormalizedPosition(0f);
        ScrollAsItemNum(-secondHalfCount);
    }

    /// <summary>
    /// 指定アイテムを表示範囲に入るようにスクロール
    /// </summary>
    public void ScrollItemToView(int itemIndex)
    {
        int minIndex = dataCount;
        int maxIndex = 0;

        ForEachScrollItem(item =>
        {
            if (!item.gameObject.activeInHierarchy)
            {
                return;
            }

            if (minIndex > item.Index)
            {
                minIndex = item.Index;
            }

            if (maxIndex < item.Index)
            {
                maxIndex = item.Index;
            }
        });

        if (itemIndex <= minIndex)
        {
            ScrollItemToHead(itemIndex);
            return;
        }

        if (itemIndex >= maxIndex)
        {
            ScrollItemToTail(itemIndex);
        }
    }

    /// <summary>
    /// 指定 index のアイテムを取得してみる. スクロール範囲外のアイテムは取得できない.
    /// </summary>
    public T FetchItem(int itemIndex)
    {
        return ScrollItemParents
            .SelectMany(n => ScrollItemTable[n.InstanceId])
            .Where(n => n)
            .Where(n => n.Index == itemIndex)
            .FirstOrDefault();
    }

    /// <summary>
    /// 現在のスクロールしているページ位置の取得
    /// </summary>
    public int GetScrollPage()
    {
        var scrollNum = IsHorizontal ? scrollContentUI.anchoredPosition.x : scrollContentUI.anchoredPosition.y;
        var page = scrollNum / ItemSize % DataCount;
        var pageAbs = Math.Abs(page);
        var pageAbsInt = (int)pageAbs;
        if ((pageAbs - (float)pageAbsInt) > fitChangePersentCache)
        {
            var pageAbsCeilToInt = Mathf.CeilToInt(pageAbs);
            //ループの場合
            if (isLoop)
            {
                //ループの場合は上方向にスクロールするとpageが-1になるのでDataCountからの差分でIndexを取得
                if (page < 0)
                {
                    if (IsHorizontal)
                    {
                        return (int)Mathf.Repeat(pageAbsCeilToInt, DataCount);
                    }

                    return (int)Mathf.Repeat(DataCount - pageAbsCeilToInt, DataCount);
                }
                else
                {
                    if (IsHorizontal)
                    {
                        return (int)Mathf.Repeat(DataCount - pageAbsCeilToInt, DataCount);
                    }

                    return (int)Mathf.Repeat(pageAbsCeilToInt, DataCount);
                }
            }

            return pageAbsCeilToInt;
        }

        //ループの場合
        if (isLoop)
        {
            //ループの場合は上方向にスクロールするとpageが-1になるのでDataCountからの差分でIndexを取得
            if (page < 0)
            {
                if (IsHorizontal)
                {
                    return (int)Mathf.Repeat(pageAbsInt, DataCount);
                }

                return (int)Mathf.Repeat(DataCount - pageAbsInt, DataCount);
            }
            else
            {
                if (IsHorizontal)
                {
                    return (int)Mathf.Repeat(DataCount - pageAbsInt, DataCount);
                }

                return (int)Mathf.Repeat(pageAbsInt, DataCount);
            }
        }

        return pageAbsInt;
    }

    /// <summary>
    /// スクロールエリアに表示される最大数
    /// </summary>
    /// <returns></returns>
    public int GetViewItemCount()
    {
        var viewSize = GetViewSize();
        return (int)(viewSize / itemSize);
    }

    /// <summary>
    /// 表示サイズ取得
    /// </summary>
    /// <returns></returns>
    public float GetViewSize()
    {
        var viewSize = scrollRect.IsHorizontal ? rectTransform.rect.width : rectTransform.rect.height;
        return viewSize;
    }

    /// <summary>
    /// 垂直方向のスクロール位置を設定
    /// </summary>
    public void SetVerticalNormalizedPosition(float verticalNormalizedPosition)
    {
        scrollRect.verticalNormalizedPosition = verticalNormalizedPosition;
    }

    /// <summary>
    /// 水平方向のスクロール位置を設定
    /// </summary>
    public void SetHorizontalNormalizedPosition(float horizontalNormalizedPosition)
    {
        scrollRect.horizontalNormalizedPosition = horizontalNormalizedPosition;
    }

    /// <summary>
    /// 水平,垂直のスクロール位置を設定
    /// </summary>
    public void SetNormalizedPosition(Vector2 normalizedPosition)
    {
        scrollRect.normalizedPosition = normalizedPosition;
    }

    /// <summary>
    /// 水平/垂直のスクロール位置を設定
    /// </summary>
    public void SetNormalizedPosition(float normalizedPosition)
    {
        if (scrollRect.horizontal && scrollRect.vertical)
        {
            scrollRect.normalizedPosition = Vector2.one * normalizedPosition;
            return;
        }

        if (scrollRect.horizontal)
        {
            scrollRect.horizontalNormalizedPosition = normalizedPosition;
        }
        else
        {
            scrollRect.verticalNormalizedPosition = normalizedPosition;
        }
    }

    /// <summary>
    /// データなし時の表示テキスト設定
    /// </summary>
    public void SetEmptyDataText(string text)
    {
        //emptyDataText.TrySetText(text);
    }

    //--------------------------------------------------
    // protected
    //--------------------------------------------------
    /// <summary>
    /// UI作成処理③
    /// </summary>
    private void Create(int startIndex, int dataCount, bool forceScrollEnable = false, Action onComplete = null)
    {
        // 作成済みのスクロールUIを削除します
        Clear();

        // ID count-up
        createImplementId++;
        if (createImplementId != createRequestId)
        {
            //更新
            Update();

            onComplete.Call();
            return;
        }

        // 複製するUIを非アクティブ
        prefab.gameObject.SetActive(false);

        // ScrollItemParentのオリジナル作成
        if (scrollItemIconParent == null)
        {
            scrollItemIconParent =
                ScrollItemIconParent.Create(rectTransform, scrollContentUI, prefab.pivot, IsHorizontal);
        }

        scrollItemIconParent.SetActive(false);

        // パラメータ設定
        this.dataCount = dataCount;
        gridCount = Math.Max(1, grid); // 1未満は1に調整
        gridDataCount = Mathf.CeilToInt((float)dataCount / gridCount);
        halfItemSize = itemSize / 2;
        forceScroll = forceScrollEnable;

        var itemPivot = IsHorizontal ? prefab.pivot.x : prefab.pivot.y;
        pivotOffsetPos = IsHorizontal
            ? new Vector2(itemPivot * itemSize, 0)
            : new Vector2(0, (itemPivot - 1) * itemSize);

        // データが無ければ処理しない
        if (dataCount == 0)
        {
            scrollRect.SetEnabled(false);
            scrollRect.SetActive(false);
            emptyDataText.TrySetActive(true);

            //更新
            Update();

            onComplete.Call();
            return;
        }

        scrollRect.SetActive(true);
        emptyDataText.TrySetActive(false);

        // Contentの初期化処理
        scrollContentUI.OnUpdateScrollIndex = OnUpdateIndex;
        scrollContentUI.Init(scrollRect.IsHorizontal, itemSize, contentOffsetPos);

        cannotUpdateScrollItem = false;

        // スクロール内のUI作成
        CreateScrollItem();

        // スクロールの位置をリセットします
        scrollContentUI.ResetAnchoredPosition();

        // UIとデータの数が一致してればスクロールさせない
        // 強制スクロールフラグで可能（ガチャメニュー用）
        var enabledScroll = isLoop || existsMargin || forceScroll;
        scrollRect.SetEnabled(enabledScroll);

        if (!enabledScroll)
        {
            //更新
            Update();

            onComplete.Call();
            return;
        }

        // 開始位置を合わせます
        SetIndexPosition(startIndex);

        //更新
        Update();

        onComplete.Call();
    }

    /// <summary>
    /// データカウントを変更
    /// </summary>
    /// <param name="dataCount"></param>
    public void SetDataCount(int dataCount)
    {
        this.dataCount = dataCount;
        gridCount = Math.Max(1, grid); // 1未満は1に調整
        gridDataCount = Mathf.CeilToInt((float)dataCount / gridCount);

        // スクロールの挙動、範囲、Pivot設定
        var rectSize = isLoop ? scrollContentUI.DefaultSize : itemSize * (float)gridDataCount;
        //オフセットがある場合サイズをずらす
        rectSize += contentOffsetPos > 0
            ? contentOffsetPos + (GetViewSize() - contentOffsetPos - itemSize)
            : 0;
        scrollContentUI.SetSize(rectSize);
    }

    /// <summary>
    /// 移動量を0にする
    /// </summary>
    public void StopMovement()
    {
        // 移動量を0にします
        scrollRect.StopMovement();
    }

    /// <summary>
    /// フィットしているスクロールアイテムを返す
    /// </summary>
    public T GetFitItem()
    {
        if (!isAutoFit)
        {
            return null;
        }

        //フィットしているアイテムも返す
        var distance = float.MaxValue;
        T item = default;
        var startPos = scrollContentUI.AnchoredDirPosition - contentOffsetPos;
        ForEachScrollItem(n =>
        {
            var d = startPos + (IsHorizontal
                ? n.GetParent().GetRectTransform().anchoredPosition.x
                : n.GetParent().GetRectTransform().anchoredPosition.y * -1f);
            if (d > 0 && distance > d)
            {
                distance = d;
                item = n;
            }
        });
        return item;
    }

    /// <summary>
    /// オフセットを設定
    /// </summary>
    /// <param name="offsetPos"></param>
    protected void SetContentOffsetPos(float offsetPos)
    {
        contentOffsetPos = offsetPos;
    }
    //--------------------------------------------------
    // private
    //--------------------------------------------------
    /// <summary>
    /// スクロール内のUIを作成します
    /// </summary>
    private void CreateScrollItem()
    {
        // スクロールの挙動、範囲、Pivot設定
        var rectSize = isLoop ? scrollContentUI.DefaultSize : itemSize * (float)gridDataCount;
        //オフセットがある場合サイズをずらす
        rectSize += contentOffsetPos > 0
            ? contentOffsetPos + (GetViewSize() - contentOffsetPos - itemSize)
            : 0;
        scrollContentUI.SetSize(rectSize);

        if (isAutoContentPivot)
        {   // コンテンツのPivotはスクロールの方向に合わせる
            var contentPivot = IsHorizontal ? new Vector2(0f, 0.5f) : new Vector2(0.5f, 1f);
            scrollContentUI.SetPivot(contentPivot);
        }
        else
        {   // ContentPivotに合わせて調整
            var contentRect = scrollContentUI.GetRectTransform();
            // アイテムのpivotは0.5を想定している
            pivotOffsetPos -= IsHorizontal
                ? new Vector2(contentRect.pivot.x * contentRect.rect.width, 0)
                : new Vector2(0, (contentRect.pivot.y - 1) * contentRect.rect.height);
        }

        // スクロールの移動設定
        // ループの時のみUnrestrictedにしないといけない
        if (isLoop)
        {
            scrollRect.movementType = ScrollRect.MovementType.Unrestricted;
        }
        else
        {
            scrollRect.movementType = ScrollRect.MovementType.Clamped;
        }

        // スクロールさせるUIの数取得
        var itemCount = CalcScrollItemUICount(rectSize);
        var offsetPos = IsHorizontal ? new Vector2(itemSize, 0) : new Vector2(0, -itemSize);
        var lcontentOffsetPos =
            IsHorizontal ? new Vector2(contentOffsetPos, 0) : new Vector2(0, -contentOffsetPos);

        for (int index = 0; index < itemCount; index++)
        {
            // スクロールアイテムの親作成
            var lscrollItemParent = Instantiate(scrollItemIconParent, scrollContentUI.rectTransform, false)
                .GetComponent<ScrollItemIconParent>();
            lscrollItemParent.name = "ScrollItem" + index;
            ScrollItemParents.Add(lscrollItemParent);

            // スクロールアイテムを作成してテーブルに登録
            var scrollItems = CreateScrollItem(lscrollItemParent);
            ScrollItemTable.Add(lscrollItemParent.InstanceId, scrollItems.ToArray());

            var pos = pivotOffsetPos + lcontentOffsetPos + (offsetPos * index);
            InitializeScrollItemParent(lscrollItemParent, index, pos);
        }

        // LayoutGroup反映
        var prefabRect = prefab.rectTransform.rect;
        var prefabSize = new Vector2(prefabRect.width, prefabRect.height);
        var totalPrefabSize = (int)(IsHorizontal ? prefabSize.y : prefabSize.x) * gridCount;
        ScrollItemParents.ForEach(c => c.ApplyLayoutGroup(gridCount, totalPrefabSize));
    }

    /// <summary>
    /// スクロール内のUI作成
    /// </summary>
    private IEnumerable<T> CreateScrollItem(ScrollItemIconParent scrollItemParent)
    {
        // スクロールアイテム作成
        for (int j = 0; j < gridCount; j++)
        {
            var scrollItem = Instantiate(prefab).GetComponent<T>();
            scrollItem.SetIndex(-1);
            scrollItem.SetAnchorCenter();
            scrollItem.SetParent(scrollItemParent, false);
            scrollItem.ResetAnchoredPosition();
            scrollItem.SetActive(true);
            yield return scrollItem;
        }
    }

    /// <summary>
    /// 作成するスクロールアイテムの数を計算します
    /// </summary>
    private int CalcScrollItemUICount(float rectSize)
    {
        // ループ or スクロール範囲を超える場合はマージン付きで返す
        var viewSize = GetViewSize();

        existsMargin = isLoop || rectSize >= viewSize;
        //強制スクロールの場合はマージン付きで返す
        if (existsMargin || forceScroll)
        {
            return (int)(viewSize / itemSize) + Margin;
        }

        // スクロール範囲内に収まるのでデータ数を返す
        return gridDataCount;
    }

    /// <summary>
    /// スクロールのインデックス値が更新時に呼ばれる処理
    /// </summary>
    /// <param name="isUnder">		true:末尾の要素を更新、 false：先頭の要素を更新		</param>
    /// <param name="scrollIndex">	更新番号											</param>
    private void OnUpdateIndex(bool isUnder, long scrollIndex)
    {
        var count = ItemCount;
        if (count <= 0)
        {
            return;
        }

        var pos = isUnder ? itemSize * scrollIndex : itemSize * count + itemSize * scrollIndex;
        var parent = isUnder ? ScrollItemParents.PopLast() : ScrollItemParents.PopFirst();

        if (isUnder)
        {
            // 末尾の要素を先頭に移動
            ScrollItemParents.Insert(0, parent);
        }
        else
        {
            // 先頭の要素を末尾に移動
            ScrollItemParents.Add(parent);

            // インデックスを末尾基準にします
            scrollIndex += ItemCount;
        }

        // UI初期化
        var anchoredPosition = IsHorizontal ? new Vector2(pos, 0) : new Vector2(0, -pos);
        var contentOffsetPos =
            IsHorizontal ? new Vector2(this.contentOffsetPos, 0) : new Vector2(0, - this.contentOffsetPos);
        InitializeScrollItemParent(parent, scrollIndex, pivotOffsetPos + contentOffsetPos + anchoredPosition,
            true, isUnder);
    }

    /// <summary>
    /// ScrollItemParentを初期化します
    /// </summary>
    private void InitializeScrollItemParent(ScrollItemIconParent scrollItemParent, long scrollIndex,
        Vector2 anchoredPosition, bool isUpdate = false, bool isUnder = true)
    {
        Action scrollSetAct = () =>
        {
            // インデックスと位置設定
            var index = ToIndex(scrollIndex);
            scrollItemParent.SetIndex(index);
            scrollItemParent.SetAnchoredPosition(anchoredPosition);

            // アクティブ設定
            var isActive = isLoop || (gridDataCount > scrollIndex && scrollIndex >= 0);
            scrollItemParent.SetActive(isActive);

            //DebugUtils.Log("InitializeScrollItemParent {0},{1},{2}", scrollIndex, gridDataCount, isActive);

            // 非アクティブだったらScrollItem初期化する必要無いので処理終わる
            if (!isActive)
            {
                return;
            }

            InitializeScrollItemUI(index, scrollItemParent);
        };

        //Updateで呼ばれた時だけ処理する
        //スクロールUI更新前に処理をさせたいもの
        if (isUpdate && OnUpdateItemParent != null)
        {
            OnUpdateItemParent.Call((int)scrollIndex, isUnder, scrollSetAct);
            return;
        }

        scrollSetAct.Call();
    }

    /// <summary>
    /// ScrollItemParentの子のScrollItemBaseを初期化します
    /// </summary>
    private void InitializeScrollItemUI(int parentIndex, ScrollItemIconParent scrollItemParent)
    {
        if (cannotUpdateScrollItem)
        {
            return;
        }

        var startIndex = parentIndex * gridCount; // scrollItemParent.Index * gridCount;
        var scrollItems = ScrollItemTable[scrollItemParent.InstanceId];

        for (int i = 0; i < gridCount; i++)
        {
            var scrollItem = scrollItems[i];
            var index = startIndex + i;
            var isActive = dataCount > index;

            scrollItem.SetActive(isActive);
            if (!isActive)
            {
                continue;
            }

            scrollItem.SetIndex(index);
            OnInitializeItem.Call(scrollItem);
            OnInitializeItemParent.Call();
        }
    }

    /// <summary>
    /// 一定以上の速度にならないように制限する処理
    /// </summary>
    private void ClampScrollSpeed()
    {
        //もし制限速度が０の場合は何もしない
        if (maxScrollSpeed == 0) return;

        //指定速度を超えていないならここまで
        if (scrollRect.Velocity < maxScrollSpeed) return;

        //絶対値化されていないvelocityの取得
        var velocity = scrollRect.velocity;
        //現在のスクロール速度取得
        var speed = IsHorizontal ? velocity.x : velocity.y;
        //clamp処理
        speed = Mathf.Clamp(speed, -maxScrollSpeed, maxScrollSpeed);
        //速度制限適用
        SetVelocity(speed);
    }

    /// <summary>
    /// スクロールアイテムの位置補正
    /// </summary>
    private void AutoFit()
    {
        // 位置補正機能がオフだったら処理しない
        if (!isAutoFit)
        {
            return;
        }

        // センタリング中は処理しない
        if (IsCentering)
        {
            return;
        }

        //最上最下に来た時には位置調整できる状態にするため止める
        if (
            !isLoop && //ループ以外
            scrollRect.Velocity != 0f && //移動がしている時
                                           //最上最下に来た時
            (scrollRect.NormalizedPosition <= 0f || scrollRect.NormalizedPosition >= 1f)
        )
        {
            StopMovement();
        }

        // 位置補正出来る状態なのか確認します
        if (
            scrollRect.IsElasticMove || // 弾力的な動きしている時は処理しない
            scrollRect.IsDrag || // ドラッグ中は処理しない
            scrollRect.Velocity > thresholdVelocity // 加速度が一定値まで落ちるまで処理しない
        )
        {
            return;
        }

        // スクロールが終わっていたら何もしない
        if (!IsScrolling && IsFitDone)
        {
            return;
        }

        //フィット方向の取得
        SetScrollDirectionToFit();
        //取得した情報を元にEnum->int変換
        var dirNum = (int)scrollDirection;
        //速度の付与
        SetVelocity(thresholdVelocity * dirNum);

        //フィット処理ができるか確認
        //誤差がでるため処理変更
        //var offset = scrollContentUI.AnchoredDirPosition % itemSize;
        var fmod = Mathf.Repeat(Mathf.Floor(scrollContentUI.AnchoredDirPosition), Mathf.Floor(itemSize));
        var offset = scrollContentUI.AnchoredDirPosition < 0f ? -fmod : fmod;
        var offsetAbs = Mathf.Abs(offset);
        var anchoredPos = -offset * moveScale;
        if (IsFittable(offsetAbs, anchoredPos) == false)
        {
            return;
        }

        // 加速度残ってるので消す
        SetVelocity(0);
        //座標のズレ分補正
        var addPixel = Mathf.Repeat(scrollContentUI.AnchoredDirPosition, itemSize);
        if (IsHorizontal)
        {
            if (scrollDirection == ScrollDirection.RIGHT)
            {
                addPixel = (itemSize - addPixel) * -1f;
            }
        }
        else
        {
            if (scrollDirection == ScrollDirection.LEFT)
            {
                addPixel = (itemSize - addPixel) * -1f;
            }
        }

        if (isLoop == false)
        {
            var normalizedPosition = scrollRect.NormalizedPosition;
            if (normalizedPosition == 0f || normalizedPosition == 1f)
            {
                SetNormalizedPosition(scrollRect.NormalizedPosition);
                addPixel = 0f;
            }
        }

        ScrollAsPixel(addPixel);
        // フィット完了フラグ
        scrollDirection = ScrollDirection.NONE;
        IsFitDone = true;
        // フィット完了コールバック
        var page = GetScrollPage();
        OnFitItem.Call(page);
    }

    /// <summary>
    /// フィットする方向の設定
    /// </summary>
    private void SetScrollDirectionToFit()
    {
        //まだスクロール方向が決定していないならスクロール方向を確定
        if (scrollDirection != ScrollDirection.NONE)
        {
            return;
        }

        //スクロールの値を取得
        var contentPosition = scrollContentUI.AnchoredDirPosition;
        var scrollNum = Mathf.Abs(contentPosition) / ItemSize;
        //判定には小数点だけで良いので整数切り捨て
        var scrollFraction = scrollNum - (int)scrollNum;
        //スクロール座標が負の数の場合は数値を反転
        //if (contentPosition < 0) scrollFraction = 1.0f - scrollFraction;
        //スクロール方向取得
        //var isMoveRight = scrollFraction > fitChangePersentCache;
        var isMoveRight = false;

        //上or右方向にスクロールしているか判定
        var isRight = contentPosition - fitPrevAnchorPos > 0f;
        if (isRight)
        {
            //スクロール座標が負の数の場合は数値を反転
            if (contentPosition < 0) scrollFraction = 1.0f - scrollFraction;
        }
        else
        {
            //スクロール座標が正の数の場合は数値を反転
            if (contentPosition > 0) scrollFraction = 1.0f - scrollFraction;
        }

        //フィットさせるかどうか
        var isFit = scrollFraction > fitChangePersentCache;
        if (isFit)
        {
            //横スクロールと縦スクロールで違う
            isMoveRight = IsHorizontal ? isRight : !isRight;
        }
        else
        {
            //横スクロールと縦スクロールで違う
            isMoveRight = IsHorizontal ? !isRight : isRight;
        }

        scrollDirection = isMoveRight ? ScrollDirection.RIGHT : ScrollDirection.LEFT;
    }

    /// <summary>
    /// フィット処理ができるかの確認
    /// </summary>
    /// <returns></returns>
    private bool IsFittable(float length, float anchoredPos)
    {
        if (IsHorizontal)
        {
            length = scrollDirection == ScrollDirection.RIGHT ? itemSize - length : length;
        }
        else
        {
            length = scrollDirection == ScrollDirection.LEFT ? itemSize - length : length;
        }

        // フィットまでの距離が遠い
        var isFar = length > fitLength;
        // スクロールの最大値になっている
        var isScrollMax = IsScrollMax();
        if (isFar && isScrollMax == false) return false;

        return true;
    }

    /// <summary>
    /// スクロールが最大になっているか
    /// </summary>
    protected bool IsScrollMax()
    {
        //ループが有効なら最大にはならない
        if (isLoop) return false;

        //スクロールの数値
        var normalizedPosition = IsHorizontal
            ? scrollRect.horizontalNormalizedPosition
            : scrollRect.verticalNormalizedPosition;
        //スクロールが最大値か
        //0.9999999対策で0.99998で判定
        var isScrollMax = normalizedPosition >= 0.99998f || normalizedPosition <= 0.00002f;
        return isScrollMax;
    }

    /// <summary>
    /// データの件数のインデックス値を返します
    /// </summary>
    private int ToIndex(long scrollNumber)
    {
        var index = (int)(scrollNumber % gridDataCount);
        return index >= 0 ? index : gridDataCount - Mathf.Abs(index);
    }

    /// <summary>
    /// スクロールのカーブ更新
    /// </summary>
    private void UpdateScrollCurve()
    {
        if (
            !isScrollCurveX && !isScrollCurveY
            || (isScrollCurveX && (scrollCurveX == null || scrollCurveValueX == 0f))
            || (isScrollCurveY && (scrollCurveY == null || scrollCurveValueY == 0f))
        )
        {
            return;
        }

        var corners = new Vector3[4];
        rectTransform.GetWorldCorners(corners);
        var topLeft = corners[0];
        var scaledSize = new Vector2(rectTransform.lossyScale.x * rectTransform.rect.size.x,
            rectTransform.lossyScale.y * rectTransform.rect.size.y);
        var rect = new Rect(topLeft, scaledSize);
        ScrollItemParents.ForEach((parent, index) =>
        {
            var items = ScrollItemTable[parent.InstanceId];

            var worldPos = parent.worldPosition;
            var moveValue = 0f;
            var t = 0f;
            if (IsHorizontal)
            {
                t = (worldPos.x - rect.x) / rect.width;
            }
            else
            {
                t = (worldPos.y - rect.y) / rect.height;
            }

            //カーブの現在値とカーブ値で移動させる
            if (IsHorizontal)
            {
                if (isScrollCurveY)
                {
                    moveValue = scrollCurveY.Evaluate(t) *scrollCurveValueY;
                    parent.SetAnchoredPositionY(moveValue);
                }

                if (isScrollCurveX)
                {
                    //ScrollItemを移動させる
                    moveValue = scrollCurveX.Evaluate(t) * scrollCurveValueX;
                    items.ForEach(n => n.SetAnchoredPositionX(moveValue));
                }
            }
            else
            {
                if (isScrollCurveX)
                {
                    moveValue = scrollCurveX.Evaluate(t) * scrollCurveValueX;
                    parent.SetAnchoredPositionX(moveValue);
                }

                if (isScrollCurveY)
                {
                    //ScrollItemを移動させる
                    moveValue = scrollCurveY.Evaluate(t) * scrollCurveValueY;
                    items.ForEach(n => n.SetAnchoredPositionY(moveValue));
                }
            }
        });
    }
}

