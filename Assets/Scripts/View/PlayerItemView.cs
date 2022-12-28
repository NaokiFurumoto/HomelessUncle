using Carbon;
using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
/// <summary>
/// �v���C���[�̏����A�C�e�����
/// </summary>
public class PlayerItemView : ViewBase, IUpdateList
{
    /// <summary> ��� <summary>
    private VIEWTYPE type;

    /// <summary> Player <summary>
    [SerializeField] private Player player;

    /// <summary> �\���� <summary>
    [SerializeField] private int index;

    //<summary �X�N���[���@�\ <summary
    [SerializeField] private ScrollSystemBase<ItemIconBase> scroll;

    /// <summary> ����{�^�� <summary>
    [SerializeField] private Button btn_Close;

    //�\������A�C�e���A�C�R��
    private static List<ItemIconData> itemIconsData = new List<ItemIconData>();

    public VIEWTYPE ViewType => type;

    protected override void Awake()
    {
        base.Awake();
        type = VIEWTYPE.ITEMVIEW;
    }

    protected override void OnEnable()
    {
        //���X�g�̍X�V
        UpdateList();
    }

    /// <summary>
    /// �O��������
    /// </summary>
    protected override void Start()
    {
        player ??= GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        index = player.HaveItemsCount;

        btn_Close.onClick.AddListener(() => animator?.SetTrigger("close"));
    }

    /// <summary>
    /// �A�C�e���̐ݒ�
    /// </summary>
    private void SetItemDataList(Action callback)
    {
        //�v���C���[�̃f�[�^�����f�[�^���擾
        var data = player.PlayerHoldItems;
        var itemManager = ItemManager.Instance;
        if (data == null || itemManager == null)
            return;

        itemIconsData.Clear();
        foreach (var (name, index) in data)
        {
            try
            {
                var itemData = itemManager.AllItemTableLists.Single(list => list.ItemName == name);
                var iconData = new ItemIconData();
                iconData.SetIconsData(itemData, index);
                itemIconsData.Add(iconData);
            }
            catch (IndexOutOfRangeException e)
            {
                DebugUtils.Log("�v�f���擾�ł��܂���");
                return;
            }
        }

        callback();
    }

    /// <summary>
    /// �A�C�R���ݒ�
    /// </summary>
    private void SetScrollItemIcon()
    {
        scroll.OnInitializeItem = icon =>
        {
            var itemIcon = icon as ItemIcon;
            if (!itemIcon) return;
            itemIcon.Setup(itemIconsData[icon.Index]);
        };

        scroll.Create(player.HaveItemsCount,null);
    }

    /// <summary>
    /// ���X�g�̍X�V����K�v��������
    /// </summary>
    public void UpdateList() 
    {
        SetItemDataList(SetScrollItemIcon);
    }
}
