using Shock.UI;
using Carbon;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Shock
{
    /// <summary>
    /// スクロールの基底クラス
    /// </summary>
    [DisallowMultipleComponent]
    public abstract class ScrollBase<T> : RectTransformBehaviour where T : ScrollItemBase
    {
        //==================================================
        // 定数
        //==================================================
        private const int Margin = 2;

        //==================================================
        // メンバー変数(SerializeField)
        //==================================================
        [SerializeField] protected ScrollRectUI m_scrollRect = null;
        [SerializeField] protected ScrollContent m_scrollContent = null;
        [SerializeField] private ScrollItemBase m_prefab = null; // 複製して使用するスクロールさせるUI
        [SerializeField] private float m_itemSize = 0f; // スクロールさせるUIのサイズ(間隔込み)
        [SerializeField] private bool m_isAutoFit = false; // ドラッグ後にスクロールアイテムの位置補正をするか
        [SerializeField] private int m_maxScrollSpeed = 0; // スクロールの最大速度
        [SerializeField] protected float m_thresholdVelocity = 0f; // 加速度のしきい値(この値以下になるとスクロールの停止処理が開始します)
        [SerializeField] protected float m_fitLength = 0f; // 位置補正時に補正するまでの距離
        [SerializeField] protected float m_moveScale = 0f; // スクロール停止時の速度に掛けるスケール
        [SerializeField] protected bool m_isLoop = false; // UIをループ表示させるか
        [SerializeField] protected bool m_isAutoContentPivot = true; // m_scrollContentのPivotを自動で変更するか
        [SerializeField] private int m_grid = 0; // グリッド
        [SerializeField] [Range(0, 100)] private int m_fitChangePersent = 50; //フィット方向を切り替えるパーセント
        [SerializeField] private TextMeshUI m_emptyDataText = null;
        [SerializeField] private float m_minVelocity = 0.01f; // velocity最小値判定。float E対策
        [SerializeField] private bool m_isItemSwitchEnable = true; // スクロールによってitemを位置を交換するか（ガチャメニューではfalse）
        [SerializeField] private float m_contentOffsetPos = 0f; // Contentsの位置をずらすためのオフセット
        [SerializeField] private bool m_IsScrollCurveX = false; // スクロールのカーブを有効にするかどうか
        [SerializeField] private AnimationCurve m_ScrollCurveX = null; // スクロールのカーブ
        [SerializeField] private float m_ScrollCurveValueX = 0f; // スクロールカーブの値
        [SerializeField] private bool m_IsScrollCurveY = false; // スクロールのカーブを有効にするかどうか
        [SerializeField] private AnimationCurve m_ScrollCurveY = null; // スクロールのカーブ
        [SerializeField] private float m_ScrollCurveValueY = 0f; // スクロールカーブの値

        //==================================================
        // メンバー変数
        //==================================================
        private int m_CreateRequestId = 0;
        private int m_CreateImplementId = 0;
        private bool m_CannotUpdateScrollItem = true;

        private int m_dataCount;
        private int m_gridCount;
        private int m_gridDataCount;

        private float m_halfItemSize;
        private float m_fitChangePersentCache;
        private Vector2 m_pivotOffsetPos;
        private bool m_isDrag;
        private bool m_existsMargin;
        private bool m_forceScroll = false;
        private float m_fitPrevAnchorPos = 0f;

        private ScrollDirection m_scrollDirection;
        private ScrollItemParent m_scrollItemParent; // ScrollItemParentのオリジナル

        private bool m_isCentering = false;
        //==================================================
        // イベント
        //==================================================
        public Action<T> OnInitializeItem { private get; set; }
        public Action OnInitializeItemParent { private get; set; }
        public Action<int> OnFitItem { protected get; set; }
        public Action<int, bool, Action> OnUpdateItemParent { private get; set; } = null;

        //==================================================
        // プロパティ
        //==================================================
        private int ItemCount
        {
            get { return ScrollItemParents.Count; }
        } // スクロール内のUIの数

        public bool IsHorizontal
        {
            get { return m_scrollRect.IsHorizontal; }
        } // 横方向にスクロールするのか

        public bool IsScrolling
        {
            get { return m_scrollRect.Velocity > m_minVelocity; }
        } // スクロール中かの判定

        public bool IsFitDone { get; protected set; } // Fit処理が完了しているか

        protected bool IsScrollActive
        {
            set { m_scrollRect.enabled = value; }
            get { return m_scrollRect.enabled; }
        } // スクロールの有効設定

        protected List<ScrollItemParent> ScrollItemParents { get; } = new List<ScrollItemParent>();
        protected Dictionary<int, IList<T>> ScrollItemTable { get; } = new Dictionary<int, IList<T>>();

        protected Vector2 RectSize => rectTransform.rect.size;

        public float ItemSize
        {
            get { return m_itemSize; }
            set { m_itemSize = value; }
        }

        public Vector2 NormalizedPosition
        {
            get { return m_scrollRect.normalizedPosition; }
        }

        public float VerticalNormalizedPosition
        {
            get { return m_scrollRect.verticalNormalizedPosition; }
        }

        public float HorizontalNormalizedPosition
        {
            get { return m_scrollRect.horizontalNormalizedPosition; }
        }

        public TextMeshUI EmptyDataText
        {
            get { return m_emptyDataText; }
        }

        public int DataCount
        {
            get { return m_dataCount; }
        } // データの数

        protected bool IsCentering
        {
            get { return m_isCentering; }
            set { m_isCentering = value; }
        }

        protected ScrollDirection ScrollDirection
        {
            get { return m_scrollDirection; }
            set { m_scrollDirection = value; }
        }

        //--------------------------------------------------
        // Monobehaviour
        //--------------------------------------------------
        private void Awake()
        {
            //AutoFit補間用
            m_scrollDirection = ScrollDirection.NONE;
            IsFitDone = true;
            m_scrollRect.BeginDrag = () => IsFitDone = false;
            m_fitChangePersentCache = m_fitChangePersent / 100f;
        }

        /// <summary>
        /// Reset処理
        /// </summary>
        private void Reset()
        {
            // 良い感じのパラメータ入れておく
            m_thresholdVelocity = 300f;
            m_moveScale = 0.8f;
            m_fitLength = 50f;

            // コンポーネント設定
            m_scrollRect = GetComponentInChildren<ScrollRectUI>();
            m_scrollContent = GetComponentInChildren<ScrollContent>();

            // prefab設定
            m_prefab = gameObject.GetComponentInChildren<ScrollItemBase>();
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
            // ScrollContentの更新処理
            if (m_isItemSwitchEnable)
            {
                m_scrollContent.DoUpdate();
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
            m_scrollRect.StopMovement();
            // フィット完了フラグ
            IsFitDone = true;

            // content
            m_scrollContent.Clear();

            // 複製するUIを非アクティブ
            m_prefab.TryChangeActive(false);
        }

        /// <summary>
        /// 構築
        /// </summary>
        /// <param name="dataCount">データ数</param>
        /// <param name="onComplete">構築完了コールバック</param>
        public virtual void Create(int dataCount, int startIndex, Action onComplete, bool forceScrollEnable = false)
        {
            m_CreateRequestId++;
            m_CannotUpdateScrollItem = true;
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
        /// 構築
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
            var velocityTmp = m_scrollRect.velocity;
            if (IsHorizontal) velocityTmp.x = velocity;
            else velocityTmp.y = velocity;
            m_scrollRect.velocity = velocityTmp;

            //移動方向が指定されていたら適用
            if (direction != ScrollDirection.NONE) m_scrollDirection = direction;
            // フィット完了フラグ
            IsFitDone = false;
        }

        /// <summary>
        /// スクロールのドラッグをキャンセル
        /// </summary>
        public void CancelDrag()
        {
            m_scrollRect.CancelDrag();
        }

        /// <summary>
        /// ドラッグ強制終了
        /// CancelDragだとタップを離すまでEndが呼ばれない
        /// </summary>
        public void ForceDragEnd()
        {
            m_scrollRect.ForceDragEnd();
        }

        /// <summary>
        /// 指定されたピクセルだけスクロールする
        /// </summary>
        public void ScrollAsPixel(float pixel)
        {
            m_scrollContent.AddAnchoredPosition(-pixel);
            m_fitPrevAnchorPos = m_scrollContent.AnchoredDirPosition;
        }

        /// <summary>
        /// 指定されたアイテム数だけスクロールする
        /// </summary>
        /// <param name="itemNum"></param>
        public void ScrollAsItemNum(int itemNum)
        {
            var pos = m_itemSize * itemNum;
            m_scrollContent.AddAnchoredPosition(-pos);
            m_fitPrevAnchorPos = m_scrollContent.AnchoredDirPosition;
        }

        /// <summary>
        /// 指定されたインデックスに合わせる
        /// </summary>
        public void SetIndexPosition(int index)
        {
            m_scrollContent.SetAnchoredPosition(0, 0);
            ScrollAsItemNum(index);
        }

        /// <summary>
        /// 指定アイテムを先頭に合わせるようにスクロール
        /// </summary>
        public void ScrollItemToHead(int itemIndex)
        {
            if (itemIndex < 0 || itemIndex >= m_dataCount)
            {
                DebugUtils.Warning("Invalid item-index to scroll-to-head: {0}", itemIndex);
                return;
            }

            // 表示範囲
            var viewSize = GetViewSize();

            // 全アイテム領域が表示範囲より狭い -> 最先頭へスクロール
            if (viewSize >= m_itemSize * m_dataCount)
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
            var secondHalfCount = m_dataCount - firstHalfCount;

            // 後半領域が表示範囲より狭い -> 最後尾へスクロール
            if (viewSize >= m_itemSize * secondHalfCount)
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
            if (itemIndex < 0 || itemIndex >= m_dataCount)
            {
                DebugUtils.Warning("Invalid item-index to scroll-to-tail: {0}", itemIndex);
                return;
            }

            var viewSize = GetViewSize();

            // 全アイテム領域が表示範囲より狭い -> 最先頭へスクロール
            if (viewSize >= m_itemSize * m_dataCount)
            {
                SetNormalizedPosition(1f);
                return;
            }

            // 前半(尻尾まで): 指定アイテムを含む
            var firstHalfCount = itemIndex + 1;

            // 前半領域が表示範囲より狭い -> 最先頭へスクロール
            if (viewSize >= m_itemSize * firstHalfCount)
            {
                SetNormalizedPosition(1f);
                return;
            }

            // 後半(尻尾以後): 指定アイテムを含まない
            var secondHalfCount = m_dataCount - firstHalfCount;

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
            int minIndex = m_dataCount;
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
            var scrollNum = IsHorizontal ? m_scrollContent.anchoredPosition.x : m_scrollContent.anchoredPosition.y;
            var page = scrollNum / ItemSize % DataCount;
            var pageAbs = Math.Abs(page);
            var pageAbsInt = (int)pageAbs;
            if ((pageAbs - (float)pageAbsInt) > m_fitChangePersentCache)
            {
                var pageAbsCeilToInt = Mathf.CeilToInt(pageAbs);
                //ループの場合
                if (m_isLoop)
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
            if (m_isLoop)
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
            return (int)(viewSize / m_itemSize);
        }

        /// <summary>
        /// 表示サイズ取得
        /// </summary>
        /// <returns></returns>
        public float GetViewSize()
        {
            var viewSize = m_scrollRect.IsHorizontal ? rectTransform.rect.width : rectTransform.rect.height;
            return viewSize;
        }

        /// <summary>
        /// 垂直方向のスクロール位置を設定
        /// </summary>
        public void SetVerticalNormalizedPosition(float verticalNormalizedPosition)
        {
            m_scrollRect.verticalNormalizedPosition = verticalNormalizedPosition;
        }

        /// <summary>
        /// 水平方向のスクロール位置を設定
        /// </summary>
        public void SetHorizontalNormalizedPosition(float horizontalNormalizedPosition)
        {
            m_scrollRect.horizontalNormalizedPosition = horizontalNormalizedPosition;
        }

        /// <summary>
        /// 水平,垂直のスクロール位置を設定
        /// </summary>
        public void SetNormalizedPosition(Vector2 normalizedPosition)
        {
            m_scrollRect.normalizedPosition = normalizedPosition;
        }

        /// <summary>
        /// 水平/垂直のスクロール位置を設定
        /// </summary>
        public void SetNormalizedPosition(float normalizedPosition)
        {
            if (m_scrollRect.horizontal && m_scrollRect.vertical)
            {
                m_scrollRect.normalizedPosition = Vector2.one * normalizedPosition;
                return;
            }

            if (m_scrollRect.horizontal)
            {
                m_scrollRect.horizontalNormalizedPosition = normalizedPosition;
            }
            else
            {
                m_scrollRect.verticalNormalizedPosition = normalizedPosition;
            }
        }

        /// <summary>
        /// データなし時の表示テキスト設定
        /// </summary>
        public void SetEmptyDataText(string text)
        {
            m_emptyDataText.TrySetText(text);
        }

        //--------------------------------------------------
        // protected
        //--------------------------------------------------
        /// <summary>
        /// UI作成処理
        /// </summary>
        private void Create(int startIndex, int dataCount, bool forceScrollEnable = false, Action onComplete = null)
        {
            // 作成済みのスクロールUIを削除します
            Clear();

            // ID count-up
            m_CreateImplementId++;
            if (m_CreateImplementId != m_CreateRequestId)
            {
                //更新
                Update();

                onComplete.Call();
                return;
            }

            // 複製するUIを非アクティブ
            m_prefab.gameObject.SetActive(false);

            // ScrollItemParentのオリジナル作成
            if (m_scrollItemParent == null)
            {
                m_scrollItemParent =
                    ScrollItemParent.Create(rectTransform, m_scrollContent, m_prefab.pivot, IsHorizontal);
            }

            m_scrollItemParent.SetActive(false);

            // パラメータ設定
            m_dataCount = dataCount;
            m_gridCount = Math.Max(1, m_grid); // 1未満は1に調整
            m_gridDataCount = Mathf.CeilToInt((float)m_dataCount / m_gridCount);
            m_halfItemSize = m_itemSize / 2;
            m_forceScroll = forceScrollEnable;

            var itemPivot = IsHorizontal ? m_prefab.pivot.x : m_prefab.pivot.y;
            m_pivotOffsetPos = IsHorizontal
                ? new Vector2(itemPivot * m_itemSize, 0)
                : new Vector2(0, (itemPivot - 1) * m_itemSize);

            // データが無ければ処理しない
            if (dataCount == 0)
            {
                m_scrollRect.SetEnabled(false);
                m_scrollRect.SetActive(false);
                m_emptyDataText.TrySetActive(true);

                //更新
                Update();

                onComplete.Call();
                return;
            }

            m_scrollRect.SetActive(true);
            m_emptyDataText.TrySetActive(false);

            // Contentの初期化処理
            m_scrollContent.OnUpdateScrollIndex = OnUpdateIndex;
            m_scrollContent.Init(m_scrollRect.IsHorizontal, m_itemSize, m_contentOffsetPos);

            m_CannotUpdateScrollItem = false;

            // スクロール内のUI作成
            CreateScrollItem();

            // スクロールの位置をリセットします
            m_scrollContent.ResetAnchoredPosition();

            // UIとデータの数が一致してればスクロールさせない
            // 強制スクロールフラグで可能（ガチャメニュー用）
            var enabledScroll = m_isLoop || m_existsMargin || m_forceScroll;
            m_scrollRect.SetEnabled(enabledScroll);

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
            m_dataCount = dataCount;
            m_gridCount = Math.Max(1, m_grid); // 1未満は1に調整
            m_gridDataCount = Mathf.CeilToInt((float)m_dataCount / m_gridCount);

            // スクロールの挙動、範囲、Pivot設定
            var rectSize = m_isLoop ? m_scrollContent.DefaultSize : m_itemSize * (float)m_gridDataCount;
            //オフセットがある場合サイズをずらす
            rectSize += m_contentOffsetPos > 0
                ? m_contentOffsetPos + (GetViewSize() - m_contentOffsetPos - m_itemSize)
                : 0;
            m_scrollContent.SetSize(rectSize);
        }

        /// <summary>
        /// 移動量を0にする
        /// </summary>
        public void StopMovement()
        {
            // 移動量を0にします
            m_scrollRect.StopMovement();
        }

        /// <summary>
        /// フィットしているスクロールアイテムを返す
        /// </summary>
        public T GetFitItem()
        {
            if (!m_isAutoFit)
            {
                return null;
            }

            //フィットしているアイテムも返す
            var distance = float.MaxValue;
            T item = default;
            var startPos = m_scrollContent.AnchoredDirPosition - m_contentOffsetPos;
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
            m_contentOffsetPos = offsetPos;
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
            var rectSize = m_isLoop ? m_scrollContent.DefaultSize : m_itemSize * (float)m_gridDataCount;
            //オフセットがある場合サイズをずらす
            rectSize += m_contentOffsetPos > 0
                ? m_contentOffsetPos + (GetViewSize() - m_contentOffsetPos - m_itemSize)
                : 0;
            m_scrollContent.SetSize(rectSize);

            if (m_isAutoContentPivot)
            {   // コンテンツのPivotはスクロールの方向に合わせる
                var contentPivot = IsHorizontal ? new Vector2(0f, 0.5f) : new Vector2(0.5f, 1f);
                m_scrollContent.SetPivot(contentPivot);
            }
            else
            {   // ContentPivotに合わせて調整
                var contentRect = m_scrollContent.GetRectTransform();
                // アイテムのpivotは0.5を想定している
                m_pivotOffsetPos -= IsHorizontal
                    ? new Vector2(contentRect.pivot.x * contentRect.rect.width, 0)
                    : new Vector2(0, (contentRect.pivot.y - 1) * contentRect.rect.height);
            }

            // スクロールの移動設定
            // ループの時のみUnrestrictedにしないといけない
            if (m_isLoop)
            {
                m_scrollRect.movementType = ScrollRect.MovementType.Unrestricted;
            }
            else
            {
                m_scrollRect.movementType = ScrollRect.MovementType.Clamped;
            }

            // スクロールさせるUIの数取得
            var itemCount = CalcScrollItemUICount(rectSize);
            var offsetPos = IsHorizontal ? new Vector2(m_itemSize, 0) : new Vector2(0, -m_itemSize);
            var contentOffsetPos =
                IsHorizontal ? new Vector2(m_contentOffsetPos, 0) : new Vector2(0, -m_contentOffsetPos);

            for (int index = 0; index < itemCount; index++)
            {
                // スクロールアイテムの親作成
                var scrollItemParent = Instantiate(m_scrollItemParent, m_scrollContent.rectTransform, false)
                    .GetComponent<ScrollItemParent>();
                scrollItemParent.name = "ScrollItem" + index;
                ScrollItemParents.Add(scrollItemParent);

                // スクロールアイテムを作成してテーブルに登録
                var scrollItems = CreateScrollItem(scrollItemParent);
                ScrollItemTable.Add(scrollItemParent.InstanceId, scrollItems.ToArray());

                var pos = m_pivotOffsetPos + contentOffsetPos + (offsetPos * index);
                InitializeScrollItemParent(scrollItemParent, index, pos);
            }

            // LayoutGroup反映
            var prefabRect = m_prefab.rectTransform.rect;
            var prefabSize = new Vector2(prefabRect.width, prefabRect.height);
            var totalPrefabSize = (int)(IsHorizontal ? prefabSize.y : prefabSize.x) * m_gridCount;
            ScrollItemParents.ForEach(c => c.ApplyLayoutGroup(m_gridCount, totalPrefabSize));
        }

        /// <summary>
        /// スクロール内のUI作成
        /// </summary>
        private IEnumerable<T> CreateScrollItem(ScrollItemParent scrollItemParent)
        {
            // スクロールアイテム作成
            for (int j = 0; j < m_gridCount; j++)
            {
                var scrollItem = Instantiate(m_prefab).GetComponent<T>();
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

            m_existsMargin = m_isLoop || rectSize >= viewSize;
            //強制スクロールの場合はマージン付きで返す
            if (m_existsMargin || m_forceScroll)
            {
                return (int)(viewSize / m_itemSize) + Margin;
            }

            // スクロール範囲内に収まるのでデータ数を返す
            return m_gridDataCount;
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

            var pos = isUnder ? m_itemSize * scrollIndex : m_itemSize * count + m_itemSize * scrollIndex;
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
                IsHorizontal ? new Vector2(m_contentOffsetPos, 0) : new Vector2(0, -m_contentOffsetPos);
            InitializeScrollItemParent(parent, scrollIndex, m_pivotOffsetPos + contentOffsetPos + anchoredPosition,
                true, isUnder);
        }

        /// <summary>
        /// ScrollItemParentを初期化します
        /// </summary>
        private void InitializeScrollItemParent(ScrollItemParent scrollItemParent, long scrollIndex,
            Vector2 anchoredPosition, bool isUpdate = false, bool isUnder = true)
        {
            //// インデックスと位置設定
            //var index = ToIndex( scrollIndex );
            //scrollItemParent.SetIndex(index);
            //scrollItemParent.SetAnchoredPosition(anchoredPosition);

            //// アクティブ設定
            //var isActive = m_isLoop || (m_gridDataCount > scrollIndex && scrollIndex >= 0);
            //scrollItemParent.SetActive(isActive);

            //// 非アクティブだったらScrollItem初期化する必要無いので処理終わる
            //if (!isActive)
            //{
            //	return;
            //}

            //if(isUpdate)
            //{
            //	//Updateで呼ばれた時だけ処理する
            //	//スクロールUI更新前に処理をさせたいもの
            //	if(OnUpdateItemParent != null)
            //	{
            //		OnUpdateItemParent.Call(scrollItemParent.Index, isUnder, () =>
            //		{
            //			InitializeScrollItemUI(index, scrollItemParent);
            //		});
            //		return;
            //	}
            //}

            //InitializeScrollItemUI(index, scrollItemParent);


            Action scrollSetAct = () =>
            {
                // インデックスと位置設定
                var index = ToIndex(scrollIndex);
                scrollItemParent.SetIndex(index);
                scrollItemParent.SetAnchoredPosition(anchoredPosition);

                // アクティブ設定
                var isActive = m_isLoop || (m_gridDataCount > scrollIndex && scrollIndex >= 0);
                scrollItemParent.SetActive(isActive);

                //DebugUtils.Log("InitializeScrollItemParent {0},{1},{2}", scrollIndex, m_gridDataCount, isActive);

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
        private void InitializeScrollItemUI(int parentIndex, ScrollItemParent scrollItemParent)
        {
            if (m_CannotUpdateScrollItem)
            {
                return;
            }

            var startIndex = parentIndex * m_gridCount; // scrollItemParent.Index * m_gridCount;
            var scrollItems = ScrollItemTable[scrollItemParent.InstanceId];

            for (int i = 0; i < m_gridCount; i++)
            {
                var scrollItem = scrollItems[i];
                var index = startIndex + i;
                var isActive = m_dataCount > index;

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
            if (m_maxScrollSpeed == 0) return;

            //指定速度を超えていないならここまで
            if (m_scrollRect.Velocity < m_maxScrollSpeed) return;

            //絶対値化されていないvelocityの取得
            var velocity = m_scrollRect.velocity;
            //現在のスクロール速度取得
            var speed = IsHorizontal ? velocity.x : velocity.y;
            //clamp処理
            speed = Mathf.Clamp(speed, -m_maxScrollSpeed, m_maxScrollSpeed);
            //速度制限適用
            SetVelocity(speed);
        }

        /// <summary>
        /// スクロールアイテムの位置補正
        /// </summary>
        private void AutoFit()
        {
            // 位置補正機能がオフだったら処理しない
            if (!m_isAutoFit)
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
                !m_isLoop && //ループ以外
                m_scrollRect.Velocity != 0f && //移動がしている時
                //最上最下に来た時
                (m_scrollRect.NormalizedPosition <= 0f || m_scrollRect.NormalizedPosition >= 1f)
            )
            {
                StopMovement();
            }

            // 位置補正出来る状態なのか確認します
            if (
                m_scrollRect.IsElasticMove || // 弾力的な動きしている時は処理しない
                m_scrollRect.IsDrag || // ドラッグ中は処理しない
                m_scrollRect.Velocity > m_thresholdVelocity // 加速度が一定値まで落ちるまで処理しない
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
            var dirNum = (int)m_scrollDirection;
            //速度の付与
            SetVelocity(m_thresholdVelocity * dirNum);

            //フィット処理ができるか確認
            //誤差がでるため処理変更
            //var offset = m_scrollContent.AnchoredDirPosition % m_itemSize;
            var fmod = Mathf.Repeat(Mathf.Floor(m_scrollContent.AnchoredDirPosition), Mathf.Floor(m_itemSize));
            var offset = m_scrollContent.AnchoredDirPosition < 0f ? -fmod : fmod;
            var offsetAbs = Mathf.Abs(offset);
            var anchoredPos = -offset * m_moveScale;
            if (IsFittable(offsetAbs, anchoredPos) == false)
            {
                return;
            }

            // 加速度残ってるので消す
            SetVelocity(0);
            //座標のズレ分補正
            var addPixel = Mathf.Repeat(m_scrollContent.AnchoredDirPosition, m_itemSize);
            if (IsHorizontal)
            {
                if (m_scrollDirection == ScrollDirection.RIGHT)
                {
                    addPixel = (m_itemSize - addPixel) * -1f;
                }
            }
            else
            {
                if (m_scrollDirection == ScrollDirection.LEFT)
                {
                    addPixel = (m_itemSize - addPixel) * -1f;
                }
            }

            if (m_isLoop == false)
            {
                var normalizedPosition = m_scrollRect.NormalizedPosition;
                if (normalizedPosition == 0f || normalizedPosition == 1f)
                {
                    SetNormalizedPosition(m_scrollRect.NormalizedPosition);
                    addPixel = 0f;
                }
            }

            ScrollAsPixel(addPixel);
            // フィット完了フラグ
            m_scrollDirection = ScrollDirection.NONE;
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
            if (m_scrollDirection != ScrollDirection.NONE)
            {
                return;
            }

            //スクロールの値を取得
            var contentPosition = m_scrollContent.AnchoredDirPosition;
            var scrollNum = Mathf.Abs(contentPosition) / ItemSize;
            //判定には小数点だけで良いので整数切り捨て
            var scrollFraction = scrollNum - (int)scrollNum;
            //スクロール座標が負の数の場合は数値を反転
            //if (contentPosition < 0) scrollFraction = 1.0f - scrollFraction;
            //スクロール方向取得
            //var isMoveRight = scrollFraction > m_fitChangePersentCache;
            var isMoveRight = false;

            //上or右方向にスクロールしているか判定
            var isRight = contentPosition - m_fitPrevAnchorPos > 0f;
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
            var isFit = scrollFraction > m_fitChangePersentCache;
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

            m_scrollDirection = isMoveRight ? ScrollDirection.RIGHT : ScrollDirection.LEFT;
        }

        /// <summary>
        /// フィット処理ができるかの確認
        /// </summary>
        /// <returns></returns>
        private bool IsFittable(float length, float anchoredPos)
        {
            if (IsHorizontal)
            {
                length = m_scrollDirection == ScrollDirection.RIGHT ? m_itemSize - length : length;
            }
            else
            {
                length = m_scrollDirection == ScrollDirection.LEFT ? m_itemSize - length : length;
            }

            // フィットまでの距離が遠い
            var isFar = length > m_fitLength;
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
            if (m_isLoop) return false;

            //スクロールの数値
            var normalizedPosition = IsHorizontal
                ? m_scrollRect.horizontalNormalizedPosition
                : m_scrollRect.verticalNormalizedPosition;
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
            var index = (int)(scrollNumber % m_gridDataCount);
            return index >= 0 ? index : m_gridDataCount - Mathf.Abs(index);
        }

        /// <summary>
        /// スクロールのカーブ更新
        /// </summary>
        private void UpdateScrollCurve()
        {
            if (
                !m_IsScrollCurveX && !m_IsScrollCurveY
                || (m_IsScrollCurveX && (m_ScrollCurveX == null || m_ScrollCurveValueX == 0f))
                || (m_IsScrollCurveY && (m_ScrollCurveY == null || m_ScrollCurveValueY == 0f))
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
                    if (m_IsScrollCurveY)
                    {
                        moveValue = m_ScrollCurveY.Evaluate(t) * m_ScrollCurveValueY;
                        parent.SetAnchoredPositionY(moveValue);
                    }

                    if (m_IsScrollCurveX)
                    {
                        //ScrollItemを移動させる
                        moveValue = m_ScrollCurveX.Evaluate(t) * m_ScrollCurveValueX;
                        items.ForEach(n => n.SetAnchoredPositionX(moveValue));
                    }
                }
                else
                {
                    if (m_IsScrollCurveX)
                    {
                        moveValue = m_ScrollCurveX.Evaluate(t) * m_ScrollCurveValueX;
                        parent.SetAnchoredPositionX(moveValue);
                    }

                    if (m_IsScrollCurveY)
                    {
                        //ScrollItemを移動させる
                        moveValue = m_ScrollCurveY.Evaluate(t) * m_ScrollCurveValueY;
                        items.ForEach(n => n.SetAnchoredPositionY(moveValue));
                    }
                }
            });
        }
    }
}
