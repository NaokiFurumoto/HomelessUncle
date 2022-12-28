using Carbon;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
//using Shock;

/// <summary>
/// �X�N���[���̕���Enum
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
/// �X�N���[�����
/// </summary>
[DisallowMultipleComponent]
public abstract class ScrollSystemBase<T> : RectTransformBehaviour where T : ItemIconBase
{
    ///�萔
    private const int Margin = 2;

    //�����o�ϐ�:SerializeField
    [SerializeField] protected ScrollRectSystem scrollRect = null;
    [SerializeField] protected ScrollContentUI scrollContentUI = null;
    [SerializeField] private ItemIconBase prefab = null; // �������Ďg�p����X�N���[��������UI
    [SerializeField] private float itemSize = 0f; // �X�N���[��������UI�̃T�C�Y(�Ԋu����)
    [SerializeField] private bool isAutoFit = false; // �h���b�O��ɃX�N���[���A�C�e���̈ʒu�␳�����邩
    [SerializeField] private int maxScrollSpeed = 0; // �X�N���[���̍ő呬�x
    [SerializeField] protected float thresholdVelocity = 0f; // �����x�̂������l(���̒l�ȉ��ɂȂ�ƃX�N���[���̒�~�������J�n���܂�)
    [SerializeField] protected float fitLength = 0f; // �ʒu�␳���ɕ␳����܂ł̋���
    [SerializeField] protected float moveScale = 0f; // �X�N���[����~���̑��x�Ɋ|����X�P�[��
    [SerializeField] protected bool isLoop = false; // UI�����[�v�\�������邩
    [SerializeField] protected bool isAutoContentPivot = true; // scrollContentUI��Pivot�������ŕύX���邩
    [SerializeField] private int grid = 0; // �O���b�h
    [SerializeField] [Range(0, 100)] private int fitChangePersent = 50; //�t�B�b�g������؂�ւ���p�[�Z���g
    [SerializeField] private TextMeshProUGUI emptyDataText = null;
    [SerializeField] private float minVelocity = 0.01f; // velocity�ŏ��l����Bfloat E�΍�
    [SerializeField] private bool isItemSwitchEnable = true; // �X�N���[���ɂ����item���ʒu���������邩�i�K�`�����j���[�ł�false�j
    [SerializeField] private float contentOffsetPos = 0f; // Contents�̈ʒu�����炷���߂̃I�t�Z�b�g
    [SerializeField] private bool isScrollCurveX = false; // �X�N���[���̃J�[�u��L���ɂ��邩�ǂ���
    [SerializeField] private AnimationCurve scrollCurveX = null; // �X�N���[���̃J�[�u
    [SerializeField] private float scrollCurveValueX = 0f; // �X�N���[���J�[�u�̒l
    [SerializeField] private bool isScrollCurveY = false; // �X�N���[���̃J�[�u��L���ɂ��邩�ǂ���
    [SerializeField] private AnimationCurve scrollCurveY = null; // �X�N���[���̃J�[�u
    [SerializeField] private float scrollCurveValueY = 0f; // �X�N���[���J�[�u�̒l

    //�����o�ϐ�
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
    private ScrollItemIconParent scrollItemIconParent; // ScrollItemParent�̃I���W�i��

    private bool isCentering = false;

    //�B�x���g:�f���Q�[�g(�߂�l�Ȃ��j
    public Action<T> OnInitializeItem { private get; set; }
    public Action OnInitializeItemParent { private get; set; }
    public Action<int> OnFitItem { protected get; set; }
    public Action<int, bool, Action> OnUpdateItemParent { private get; set; } = null;

    //==================================================
    // �v���p�e�B
    //==================================================
   
    private int ItemCount => ScrollItemParents.Count;  // �X�N���[������UI�̐�
    public bool IsHorizontal => scrollRect.IsHorizontal; // �������ɃX�N���[������̂�
    public bool IsScrolling => scrollRect.Velocity > minVelocity;// �X�N���[�������̔���
    public bool IsFitDone { get; protected set; } // Fit�������������Ă��邩
    protected bool IsScrollActive// �X�N���[���̗L���ݒ�
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
    public int DataCount => dataCount;// �f�[�^�̐�
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
        //AutoFit��ԗp
        scrollDirection = ScrollDirection.NONE;
        IsFitDone = true;
        scrollRect.BeginDrag = () => IsFitDone = false;
        fitChangePersentCache = fitChangePersent / 100f;
    }

    // <summary>
    /// Reset����
    /// </summary>
    private void Reset()
    {
        // �ǂ������̃p�����[�^����Ă���
        thresholdVelocity = 300f;
        moveScale = 0.8f;
        fitLength = 50f;

        // �R���|�[�l���g�ݒ�
        scrollRect = GetComponentInChildren<ScrollRectSystem>();
        scrollContentUI = GetComponentInChildren<ScrollContentUI>();

        // prefab�ݒ�
        prefab = gameObject.GetComponentInChildren<ItemIconBase>();
    }

    /// <summary>
    /// �j������
    /// </summary>
    protected virtual void OnDestroy()
    {
        OnInitializeItem = null;
        OnInitializeItemParent = null;
        OnFitItem = null;
        OnUpdateItemParent = null;
    }

    /// <summary>
    /// ���t���[���X�V����
    /// </summary>
    protected virtual void Update()
    {
        // ScrollContentUI�̍X�V����
        if (isItemSwitchEnable)
        {
            scrollContentUI.DoUpdate();
        }

        //�X�N���[�����x�̐�������
        ClampScrollSpeed();

        // �X�N���[���A�C�e���̈ʒu�␳����
        AutoFit();

        // �X�N���[���̃J�[�u�X�V
        UpdateScrollCurve();
    }

    //--------------------------------------------------
    // public
    //--------------------------------------------------
    /// <summary>
    /// �쐬�ς݂̃X�N���[��UI���폜���܂�
    /// </summary>
    public void Clear()
    {
        // �쐬�ς݂̃X�N���[���A�C�e���폜
        ScrollItemParents.Where(c => c != null).ForEach(c => c.DestroyGameObject());
        ScrollItemParents.Clear();
        ScrollItemTable.Clear();

        // �ړ��ʂ�0�ɂ��܂�
        scrollRect.StopMovement();
        // �t�B�b�g�����t���O
        IsFitDone = true;

        // content
        scrollContentUI.Clear();

        // ��������UI���A�N�e�B�u
        prefab.TryChangeActive(false);
    }

    /// <summary>
    /// �\�z�A
    /// </summary>
    /// <param name="dataCount">�f�[�^��</param>
    /// <param name="onComplete">�\�z�����R�[���o�b�N</param>
    public virtual void Create(int dataCount, int startIndex, Action onComplete, bool forceScrollEnable = false)
    {
        createRequestId++;
        cannotUpdateScrollItem = true;
        // LayoutGroup���T�C�Y����̂�1�t���҂��܂�
        CoroutineManager.CallWaitForOneFrame(() =>
        {
            Create(startIndex, dataCount, forceScrollEnable, () =>
            {
                //��Create�̒���Update������悤�ɂ����̂�1�t���҂��Ȃ��Ă��悭�Ȃ����̂ŃR�����g�A�E�g
                //�|�W�V�����ύX������ꍇ�ɂ����ɔ��f����Ȃ��̂ł����ł�1�t���҂�
                //CoroutineManager.CallWaitForOneFrame(onComplete);
                onComplete.Call();
            });
        });
    }

    /// <summary>
    /// �\�z�@
    /// </summary>
    /// <param name="dataCount">�f�[�^��</param>
    /// <param name="onComplete">�\�z�����R�[���o�b�N</param>
    public virtual void Create(int dataCount, Action onComplete)
    {
        Create(dataCount, 0, onComplete);
    }

    /// <summary>
    /// �\�z�����A�C�e���������v���Z�X�����𑖂�
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
    /// �X�N���[���̑��x��ݒ肵�܂�
    /// </summary>
    /// <param name="velocity">�X�N���[�����x</param>
    /// <param name="direction">�X�N���[���␳����</param>
    public void SetVelocity(float velocity, ScrollDirection direction = ScrollDirection.NONE)
    {
        // �����x�ݒ�
        var velocityTmp = scrollRect.velocity;
        if (IsHorizontal) velocityTmp.x = velocity;
        else velocityTmp.y = velocity;
        scrollRect.velocity = velocityTmp;

        //�ړ��������w�肳��Ă�����K�p
        if (direction != ScrollDirection.NONE) scrollDirection = direction;
        // �t�B�b�g�����t���O
        IsFitDone = false;
    }

    /// <summary>
    /// �X�N���[���̃h���b�O���L�����Z��
    /// </summary>
    public void CancelDrag()
    {
        scrollRect.CancelDrag();
    }

    /// <summary>
    /// �h���b�O�����I��
    /// CancelDrag���ƃ^�b�v�𗣂��܂�End���Ă΂�Ȃ�
    /// </summary>
    public void ForceDragEnd()
    {
        scrollRect.ForceDragEnd();
    }

    /// <summary>
    /// �w�肳�ꂽ�s�N�Z�������X�N���[������
    /// </summary>
    public void ScrollAsPixel(float pixel)
    {
        scrollContentUI.AddAnchoredPosition(-pixel);
        fitPrevAnchorPos = scrollContentUI.AnchoredDirPosition;
    }

    /// <summary>
    /// �w�肳�ꂽ�A�C�e���������X�N���[������
    /// </summary>
    /// <param name="itemNum"></param>
    public void ScrollAsItemNum(int itemNum)
    {
        var pos = itemSize * itemNum;
        scrollContentUI.AddAnchoredPosition(-pos);
        fitPrevAnchorPos = scrollContentUI.AnchoredDirPosition;
    }

    /// <summary>
    /// �w�肳�ꂽ�C���f�b�N�X�ɍ��킹��
    /// </summary>
    public void SetIndexPosition(int index)
    {
        scrollContentUI.SetAnchoredPosition(0, 0);
        ScrollAsItemNum(index);
    }

    /// <summary>
    /// �w��A�C�e����擪�ɍ��킹��悤�ɃX�N���[��
    /// </summary>
    public void ScrollItemToHead(int itemIndex)
    {
        if (itemIndex < 0 || itemIndex >= dataCount)
        {
            DebugUtils.Warning("Invalid item-index to scroll-to-head: {0}", itemIndex);
            return;
        }

        // �\���͈�
        var viewSize = GetViewSize();

        // �S�A�C�e���̈悪�\���͈͂�苷�� -> �Ő擪�փX�N���[��
        if (viewSize >= itemSize * dataCount)
        {
            SetNormalizedPosition(1f);
            return;
        }

        // �O��(�擪����): �w��A�C�e�����܂܂Ȃ�
        var firstHalfCount = itemIndex;

        // �O�����Ȃ� -> �Ő擪��
        if (firstHalfCount <= 0)
        {
            SetNormalizedPosition(1f);
            return;
        }

        // �㔼(�擪����): �w��A�C�e�����܂�
        var secondHalfCount = dataCount - firstHalfCount;

        // �㔼�̈悪�\���͈͂�苷�� -> �Ō���փX�N���[��
        if (viewSize >= itemSize * secondHalfCount)
        {
            SetNormalizedPosition(0f);
            return;
        }

        // �Ő擪������փX�N���[��
        SetNormalizedPosition(1f);
        ScrollAsItemNum(firstHalfCount);
    }

    /// <summary>
    /// �w��A�C�e����K���ɍ��킹��悤�ɃX�N���[��
    /// </summary>
    public void ScrollItemToTail(int itemIndex)
    {
        if (itemIndex < 0 || itemIndex >= dataCount)
        {
            DebugUtils.Warning("Invalid item-index to scroll-to-tail: {0}", itemIndex);
            return;
        }

        var viewSize = GetViewSize();

        // �S�A�C�e���̈悪�\���͈͂�苷�� -> �Ő擪�փX�N���[��
        if (viewSize >= itemSize * dataCount)
        {
            SetNormalizedPosition(1f);
            return;
        }

        // �O��(�K���܂�): �w��A�C�e�����܂�
        var firstHalfCount = itemIndex + 1;

        // �O���̈悪�\���͈͂�苷�� -> �Ő擪�փX�N���[��
        if (viewSize >= itemSize * firstHalfCount)
        {
            SetNormalizedPosition(1f);
            return;
        }

        // �㔼(�K���Ȍ�): �w��A�C�e�����܂܂Ȃ�
        var secondHalfCount = dataCount - firstHalfCount;

        // �㔼���Ȃ� -> �Ō���փX�N���[��
        if (secondHalfCount <= 0)
        {
            SetNormalizedPosition(0f);
            return;
        }

        // �Ō������O�փX�N���[��
        SetNormalizedPosition(0f);
        ScrollAsItemNum(-secondHalfCount);
    }

    /// <summary>
    /// �w��A�C�e����\���͈͂ɓ���悤�ɃX�N���[��
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
    /// �w�� index �̃A�C�e�����擾���Ă݂�. �X�N���[���͈͊O�̃A�C�e���͎擾�ł��Ȃ�.
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
    /// ���݂̃X�N���[�����Ă���y�[�W�ʒu�̎擾
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
            //���[�v�̏ꍇ
            if (isLoop)
            {
                //���[�v�̏ꍇ�͏�����ɃX�N���[�������page��-1�ɂȂ�̂�DataCount����̍�����Index���擾
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

        //���[�v�̏ꍇ
        if (isLoop)
        {
            //���[�v�̏ꍇ�͏�����ɃX�N���[�������page��-1�ɂȂ�̂�DataCount����̍�����Index���擾
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
    /// �X�N���[���G���A�ɕ\�������ő吔
    /// </summary>
    /// <returns></returns>
    public int GetViewItemCount()
    {
        var viewSize = GetViewSize();
        return (int)(viewSize / itemSize);
    }

    /// <summary>
    /// �\���T�C�Y�擾
    /// </summary>
    /// <returns></returns>
    public float GetViewSize()
    {
        var viewSize = scrollRect.IsHorizontal ? rectTransform.rect.width : rectTransform.rect.height;
        return viewSize;
    }

    /// <summary>
    /// ���������̃X�N���[���ʒu��ݒ�
    /// </summary>
    public void SetVerticalNormalizedPosition(float verticalNormalizedPosition)
    {
        scrollRect.verticalNormalizedPosition = verticalNormalizedPosition;
    }

    /// <summary>
    /// ���������̃X�N���[���ʒu��ݒ�
    /// </summary>
    public void SetHorizontalNormalizedPosition(float horizontalNormalizedPosition)
    {
        scrollRect.horizontalNormalizedPosition = horizontalNormalizedPosition;
    }

    /// <summary>
    /// ����,�����̃X�N���[���ʒu��ݒ�
    /// </summary>
    public void SetNormalizedPosition(Vector2 normalizedPosition)
    {
        scrollRect.normalizedPosition = normalizedPosition;
    }

    /// <summary>
    /// ����/�����̃X�N���[���ʒu��ݒ�
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
    /// �f�[�^�Ȃ����̕\���e�L�X�g�ݒ�
    /// </summary>
    public void SetEmptyDataText(string text)
    {
        //emptyDataText.TrySetText(text);
    }

    //--------------------------------------------------
    // protected
    //--------------------------------------------------
    /// <summary>
    /// UI�쐬�����B
    /// </summary>
    private void Create(int startIndex, int dataCount, bool forceScrollEnable = false, Action onComplete = null)
    {
        // �쐬�ς݂̃X�N���[��UI���폜���܂�
        Clear();

        // ID count-up
        createImplementId++;
        if (createImplementId != createRequestId)
        {
            //�X�V
            Update();

            onComplete.Call();
            return;
        }

        // ��������UI���A�N�e�B�u
        prefab.gameObject.SetActive(false);

        // ScrollItemParent�̃I���W�i���쐬
        if (scrollItemIconParent == null)
        {
            scrollItemIconParent =
                ScrollItemIconParent.Create(rectTransform, scrollContentUI, prefab.pivot, IsHorizontal);
        }

        scrollItemIconParent.SetActive(false);

        // �p�����[�^�ݒ�
        this.dataCount = dataCount;
        gridCount = Math.Max(1, grid); // 1������1�ɒ���
        gridDataCount = Mathf.CeilToInt((float)dataCount / gridCount);
        halfItemSize = itemSize / 2;
        forceScroll = forceScrollEnable;

        var itemPivot = IsHorizontal ? prefab.pivot.x : prefab.pivot.y;
        pivotOffsetPos = IsHorizontal
            ? new Vector2(itemPivot * itemSize, 0)
            : new Vector2(0, (itemPivot - 1) * itemSize);

        // �f�[�^��������Ώ������Ȃ�
        if (dataCount == 0)
        {
            scrollRect.SetEnabled(false);
            scrollRect.SetActive(false);
            emptyDataText.TrySetActive(true);

            //�X�V
            Update();

            onComplete.Call();
            return;
        }

        scrollRect.SetActive(true);
        emptyDataText.TrySetActive(false);

        // Content�̏���������
        scrollContentUI.OnUpdateScrollIndex = OnUpdateIndex;
        scrollContentUI.Init(scrollRect.IsHorizontal, itemSize, contentOffsetPos);

        cannotUpdateScrollItem = false;

        // �X�N���[������UI�쐬
        CreateScrollItem();

        // �X�N���[���̈ʒu�����Z�b�g���܂�
        scrollContentUI.ResetAnchoredPosition();

        // UI�ƃf�[�^�̐�����v���Ă�΃X�N���[�������Ȃ�
        // �����X�N���[���t���O�ŉ\�i�K�`�����j���[�p�j
        var enabledScroll = isLoop || existsMargin || forceScroll;
        scrollRect.SetEnabled(enabledScroll);

        if (!enabledScroll)
        {
            //�X�V
            Update();

            onComplete.Call();
            return;
        }

        // �J�n�ʒu�����킹�܂�
        SetIndexPosition(startIndex);

        //�X�V
        Update();

        onComplete.Call();
    }

    /// <summary>
    /// �f�[�^�J�E���g��ύX
    /// </summary>
    /// <param name="dataCount"></param>
    public void SetDataCount(int dataCount)
    {
        this.dataCount = dataCount;
        gridCount = Math.Max(1, grid); // 1������1�ɒ���
        gridDataCount = Mathf.CeilToInt((float)dataCount / gridCount);

        // �X�N���[���̋����A�͈́APivot�ݒ�
        var rectSize = isLoop ? scrollContentUI.DefaultSize : itemSize * (float)gridDataCount;
        //�I�t�Z�b�g������ꍇ�T�C�Y�����炷
        rectSize += contentOffsetPos > 0
            ? contentOffsetPos + (GetViewSize() - contentOffsetPos - itemSize)
            : 0;
        scrollContentUI.SetSize(rectSize);
    }

    /// <summary>
    /// �ړ��ʂ�0�ɂ���
    /// </summary>
    public void StopMovement()
    {
        // �ړ��ʂ�0�ɂ��܂�
        scrollRect.StopMovement();
    }

    /// <summary>
    /// �t�B�b�g���Ă���X�N���[���A�C�e����Ԃ�
    /// </summary>
    public T GetFitItem()
    {
        if (!isAutoFit)
        {
            return null;
        }

        //�t�B�b�g���Ă���A�C�e�����Ԃ�
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
    /// �I�t�Z�b�g��ݒ�
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
    /// �X�N���[������UI���쐬���܂�
    /// </summary>
    private void CreateScrollItem()
    {
        // �X�N���[���̋����A�͈́APivot�ݒ�
        var rectSize = isLoop ? scrollContentUI.DefaultSize : itemSize * (float)gridDataCount;
        //�I�t�Z�b�g������ꍇ�T�C�Y�����炷
        rectSize += contentOffsetPos > 0
            ? contentOffsetPos + (GetViewSize() - contentOffsetPos - itemSize)
            : 0;
        scrollContentUI.SetSize(rectSize);

        if (isAutoContentPivot)
        {   // �R���e���c��Pivot�̓X�N���[���̕����ɍ��킹��
            var contentPivot = IsHorizontal ? new Vector2(0f, 0.5f) : new Vector2(0.5f, 1f);
            scrollContentUI.SetPivot(contentPivot);
        }
        else
        {   // ContentPivot�ɍ��킹�Ē���
            var contentRect = scrollContentUI.GetRectTransform();
            // �A�C�e����pivot��0.5��z�肵�Ă���
            pivotOffsetPos -= IsHorizontal
                ? new Vector2(contentRect.pivot.x * contentRect.rect.width, 0)
                : new Vector2(0, (contentRect.pivot.y - 1) * contentRect.rect.height);
        }

        // �X�N���[���̈ړ��ݒ�
        // ���[�v�̎��̂�Unrestricted�ɂ��Ȃ��Ƃ����Ȃ�
        if (isLoop)
        {
            scrollRect.movementType = ScrollRect.MovementType.Unrestricted;
        }
        else
        {
            scrollRect.movementType = ScrollRect.MovementType.Clamped;
        }

        // �X�N���[��������UI�̐��擾
        var itemCount = CalcScrollItemUICount(rectSize);
        var offsetPos = IsHorizontal ? new Vector2(itemSize, 0) : new Vector2(0, -itemSize);
        var lcontentOffsetPos =
            IsHorizontal ? new Vector2(contentOffsetPos, 0) : new Vector2(0, -contentOffsetPos);

        for (int index = 0; index < itemCount; index++)
        {
            // �X�N���[���A�C�e���̐e�쐬
            var lscrollItemParent = Instantiate(scrollItemIconParent, scrollContentUI.rectTransform, false)
                .GetComponent<ScrollItemIconParent>();
            lscrollItemParent.name = "ScrollItem" + index;
            ScrollItemParents.Add(lscrollItemParent);

            // �X�N���[���A�C�e�����쐬���ăe�[�u���ɓo�^
            var scrollItems = CreateScrollItem(lscrollItemParent);
            ScrollItemTable.Add(lscrollItemParent.InstanceId, scrollItems.ToArray());

            var pos = pivotOffsetPos + lcontentOffsetPos + (offsetPos * index);
            InitializeScrollItemParent(lscrollItemParent, index, pos);
        }

        // LayoutGroup���f
        var prefabRect = prefab.rectTransform.rect;
        var prefabSize = new Vector2(prefabRect.width, prefabRect.height);
        var totalPrefabSize = (int)(IsHorizontal ? prefabSize.y : prefabSize.x) * gridCount;
        ScrollItemParents.ForEach(c => c.ApplyLayoutGroup(gridCount, totalPrefabSize));
    }

    /// <summary>
    /// �X�N���[������UI�쐬
    /// </summary>
    private IEnumerable<T> CreateScrollItem(ScrollItemIconParent scrollItemParent)
    {
        // �X�N���[���A�C�e���쐬
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
    /// �쐬����X�N���[���A�C�e���̐����v�Z���܂�
    /// </summary>
    private int CalcScrollItemUICount(float rectSize)
    {
        // ���[�v or �X�N���[���͈͂𒴂���ꍇ�̓}�[�W���t���ŕԂ�
        var viewSize = GetViewSize();

        existsMargin = isLoop || rectSize >= viewSize;
        //�����X�N���[���̏ꍇ�̓}�[�W���t���ŕԂ�
        if (existsMargin || forceScroll)
        {
            return (int)(viewSize / itemSize) + Margin;
        }

        // �X�N���[���͈͓��Ɏ��܂�̂Ńf�[�^����Ԃ�
        return gridDataCount;
    }

    /// <summary>
    /// �X�N���[���̃C���f�b�N�X�l���X�V���ɌĂ΂�鏈��
    /// </summary>
    /// <param name="isUnder">		true:�����̗v�f���X�V�A false�F�擪�̗v�f���X�V		</param>
    /// <param name="scrollIndex">	�X�V�ԍ�											</param>
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
            // �����̗v�f��擪�Ɉړ�
            ScrollItemParents.Insert(0, parent);
        }
        else
        {
            // �擪�̗v�f�𖖔��Ɉړ�
            ScrollItemParents.Add(parent);

            // �C���f�b�N�X�𖖔���ɂ��܂�
            scrollIndex += ItemCount;
        }

        // UI������
        var anchoredPosition = IsHorizontal ? new Vector2(pos, 0) : new Vector2(0, -pos);
        var contentOffsetPos =
            IsHorizontal ? new Vector2(this.contentOffsetPos, 0) : new Vector2(0, - this.contentOffsetPos);
        InitializeScrollItemParent(parent, scrollIndex, pivotOffsetPos + contentOffsetPos + anchoredPosition,
            true, isUnder);
    }

    /// <summary>
    /// ScrollItemParent�����������܂�
    /// </summary>
    private void InitializeScrollItemParent(ScrollItemIconParent scrollItemParent, long scrollIndex,
        Vector2 anchoredPosition, bool isUpdate = false, bool isUnder = true)
    {
        Action scrollSetAct = () =>
        {
            // �C���f�b�N�X�ƈʒu�ݒ�
            var index = ToIndex(scrollIndex);
            scrollItemParent.SetIndex(index);
            scrollItemParent.SetAnchoredPosition(anchoredPosition);

            // �A�N�e�B�u�ݒ�
            var isActive = isLoop || (gridDataCount > scrollIndex && scrollIndex >= 0);
            scrollItemParent.SetActive(isActive);

            //DebugUtils.Log("InitializeScrollItemParent {0},{1},{2}", scrollIndex, gridDataCount, isActive);

            // ��A�N�e�B�u��������ScrollItem����������K�v�����̂ŏ����I���
            if (!isActive)
            {
                return;
            }

            InitializeScrollItemUI(index, scrollItemParent);
        };

        //Update�ŌĂ΂ꂽ��������������
        //�X�N���[��UI�X�V�O�ɏ�����������������
        if (isUpdate && OnUpdateItemParent != null)
        {
            OnUpdateItemParent.Call((int)scrollIndex, isUnder, scrollSetAct);
            return;
        }

        scrollSetAct.Call();
    }

    /// <summary>
    /// ScrollItemParent�̎q��ScrollItemBase�����������܂�
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
    /// ���ȏ�̑��x�ɂȂ�Ȃ��悤�ɐ������鏈��
    /// </summary>
    private void ClampScrollSpeed()
    {
        //�����������x���O�̏ꍇ�͉������Ȃ�
        if (maxScrollSpeed == 0) return;

        //�w�葬�x�𒴂��Ă��Ȃ��Ȃ炱���܂�
        if (scrollRect.Velocity < maxScrollSpeed) return;

        //��Βl������Ă��Ȃ�velocity�̎擾
        var velocity = scrollRect.velocity;
        //���݂̃X�N���[�����x�擾
        var speed = IsHorizontal ? velocity.x : velocity.y;
        //clamp����
        speed = Mathf.Clamp(speed, -maxScrollSpeed, maxScrollSpeed);
        //���x�����K�p
        SetVelocity(speed);
    }

    /// <summary>
    /// �X�N���[���A�C�e���̈ʒu�␳
    /// </summary>
    private void AutoFit()
    {
        // �ʒu�␳�@�\���I�t�������珈�����Ȃ�
        if (!isAutoFit)
        {
            return;
        }

        // �Z���^�����O���͏������Ȃ�
        if (IsCentering)
        {
            return;
        }

        //�ŏ�ŉ��ɗ������ɂ͈ʒu�����ł����Ԃɂ��邽�ߎ~�߂�
        if (
            !isLoop && //���[�v�ȊO
            scrollRect.Velocity != 0f && //�ړ������Ă��鎞
                                           //�ŏ�ŉ��ɗ�����
            (scrollRect.NormalizedPosition <= 0f || scrollRect.NormalizedPosition >= 1f)
        )
        {
            StopMovement();
        }

        // �ʒu�␳�o�����ԂȂ̂��m�F���܂�
        if (
            scrollRect.IsElasticMove || // �e�͓I�ȓ������Ă��鎞�͏������Ȃ�
            scrollRect.IsDrag || // �h���b�O���͏������Ȃ�
            scrollRect.Velocity > thresholdVelocity // �����x�����l�܂ŗ�����܂ŏ������Ȃ�
        )
        {
            return;
        }

        // �X�N���[�����I����Ă����牽�����Ȃ�
        if (!IsScrolling && IsFitDone)
        {
            return;
        }

        //�t�B�b�g�����̎擾
        SetScrollDirectionToFit();
        //�擾������������Enum->int�ϊ�
        var dirNum = (int)scrollDirection;
        //���x�̕t�^
        SetVelocity(thresholdVelocity * dirNum);

        //�t�B�b�g�������ł��邩�m�F
        //�덷���ł邽�ߏ����ύX
        //var offset = scrollContentUI.AnchoredDirPosition % itemSize;
        var fmod = Mathf.Repeat(Mathf.Floor(scrollContentUI.AnchoredDirPosition), Mathf.Floor(itemSize));
        var offset = scrollContentUI.AnchoredDirPosition < 0f ? -fmod : fmod;
        var offsetAbs = Mathf.Abs(offset);
        var anchoredPos = -offset * moveScale;
        if (IsFittable(offsetAbs, anchoredPos) == false)
        {
            return;
        }

        // �����x�c���Ă�̂ŏ���
        SetVelocity(0);
        //���W�̃Y�����␳
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
        // �t�B�b�g�����t���O
        scrollDirection = ScrollDirection.NONE;
        IsFitDone = true;
        // �t�B�b�g�����R�[���o�b�N
        var page = GetScrollPage();
        OnFitItem.Call(page);
    }

    /// <summary>
    /// �t�B�b�g��������̐ݒ�
    /// </summary>
    private void SetScrollDirectionToFit()
    {
        //�܂��X�N���[�����������肵�Ă��Ȃ��Ȃ�X�N���[���������m��
        if (scrollDirection != ScrollDirection.NONE)
        {
            return;
        }

        //�X�N���[���̒l���擾
        var contentPosition = scrollContentUI.AnchoredDirPosition;
        var scrollNum = Mathf.Abs(contentPosition) / ItemSize;
        //����ɂ͏����_�����ŗǂ��̂Ő����؂�̂�
        var scrollFraction = scrollNum - (int)scrollNum;
        //�X�N���[�����W�����̐��̏ꍇ�͐��l�𔽓]
        //if (contentPosition < 0) scrollFraction = 1.0f - scrollFraction;
        //�X�N���[�������擾
        //var isMoveRight = scrollFraction > fitChangePersentCache;
        var isMoveRight = false;

        //��or�E�����ɃX�N���[�����Ă��邩����
        var isRight = contentPosition - fitPrevAnchorPos > 0f;
        if (isRight)
        {
            //�X�N���[�����W�����̐��̏ꍇ�͐��l�𔽓]
            if (contentPosition < 0) scrollFraction = 1.0f - scrollFraction;
        }
        else
        {
            //�X�N���[�����W�����̐��̏ꍇ�͐��l�𔽓]
            if (contentPosition > 0) scrollFraction = 1.0f - scrollFraction;
        }

        //�t�B�b�g�����邩�ǂ���
        var isFit = scrollFraction > fitChangePersentCache;
        if (isFit)
        {
            //���X�N���[���Əc�X�N���[���ňႤ
            isMoveRight = IsHorizontal ? isRight : !isRight;
        }
        else
        {
            //���X�N���[���Əc�X�N���[���ňႤ
            isMoveRight = IsHorizontal ? !isRight : isRight;
        }

        scrollDirection = isMoveRight ? ScrollDirection.RIGHT : ScrollDirection.LEFT;
    }

    /// <summary>
    /// �t�B�b�g�������ł��邩�̊m�F
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

        // �t�B�b�g�܂ł̋���������
        var isFar = length > fitLength;
        // �X�N���[���̍ő�l�ɂȂ��Ă���
        var isScrollMax = IsScrollMax();
        if (isFar && isScrollMax == false) return false;

        return true;
    }

    /// <summary>
    /// �X�N���[�����ő�ɂȂ��Ă��邩
    /// </summary>
    protected bool IsScrollMax()
    {
        //���[�v���L���Ȃ�ő�ɂ͂Ȃ�Ȃ�
        if (isLoop) return false;

        //�X�N���[���̐��l
        var normalizedPosition = IsHorizontal
            ? scrollRect.horizontalNormalizedPosition
            : scrollRect.verticalNormalizedPosition;
        //�X�N���[�����ő�l��
        //0.9999999�΍��0.99998�Ŕ���
        var isScrollMax = normalizedPosition >= 0.99998f || normalizedPosition <= 0.00002f;
        return isScrollMax;
    }

    /// <summary>
    /// �f�[�^�̌����̃C���f�b�N�X�l��Ԃ��܂�
    /// </summary>
    private int ToIndex(long scrollNumber)
    {
        var index = (int)(scrollNumber % gridDataCount);
        return index >= 0 ? index : gridDataCount - Mathf.Abs(index);
    }

    /// <summary>
    /// �X�N���[���̃J�[�u�X�V
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

            //�J�[�u�̌��ݒl�ƃJ�[�u�l�ňړ�������
            if (IsHorizontal)
            {
                if (isScrollCurveY)
                {
                    moveValue = scrollCurveY.Evaluate(t) *scrollCurveValueY;
                    parent.SetAnchoredPositionY(moveValue);
                }

                if (isScrollCurveX)
                {
                    //ScrollItem���ړ�������
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
                    //ScrollItem���ړ�������
                    moveValue = scrollCurveY.Evaluate(t) * scrollCurveValueY;
                    items.ForEach(n => n.SetAnchoredPositionY(moveValue));
                }
            }
        });
    }
}

