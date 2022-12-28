using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[Serializable]
[CreateAssetMenu(fileName = "ItemData", menuName = "Create_ItemData")]
public class ItemData : ScriptableObject
{
    /// <summary> �A�C�e���̎�� </summary>
    public enum ITEM_TYPE
    {
        SHOPITEM,   //�V���b�v
        FISH,       //��
        DIG         //�@��
    }

    /// <summary> ���A���e�B </summary>
    public enum ITEM_RARITY
    {
        N = 0, R = 1, SR = 2, SSR = 3, UR = 4
    }

    /// <summary> �A�C�e���̎�� </summary>
    [SerializeField] [Header("�A�C�e���̎��")]
    private ITEM_TYPE itemType;

    /// <summary> �A�C�R���摜 </summary>
    [SerializeField]
    [Header("�A�C�R���摜")]
    private Sprite iconImage;

    /// <summary> �A�C�e���摜 </summary>
    [SerializeField][Header("�A�C�e���摜")]
    private Sprite itemSprite;

    /// <summary> ���O </summary>
    [SerializeField][Header("�A�C�e����")]
    private string itemName;

    /// <summary> ���O </summary>
    [SerializeField]
    [Header("���A�x")]
    private ITEM_RARITY rarity;

    /// <summary> ��� </summary>
    [SerializeField][Header("�A�C�e���̐���")]
    [TextArea(1, 6)]
    private string itemInfo;

    /// <summary> �ő及���� </summary>
    [SerializeField][Header("�ő及����")]
    private int maxHaveIndex;

    /// <summary> �������Ƃ��o���� </summary>
    [SerializeField][Header("�������Ƃ��o����")]
    private bool isBuy;

    /// <summary> ���邱�Ƃ��o���� </summary>
    [SerializeField][Header("���邱�Ƃ��o����")]
    private bool isSell;

    /// <summary> ���l </summary>
    [SerializeField][Header("���l")]
    private int price;

    /// <summary> ���l </summary>
    [SerializeField][Header("���l")]
    private int sellPrice;

    /// <summary> �R���N�V�����A�C�e�����ǂ��� </summary>
    [SerializeField][Header("�R���N�V�����A�C�e�����ǂ���")]
    private bool isCollection;

    /// <summary> ���� </summary>
    [SerializeField][Header("����")]
    private ItemAbility itemAbility;

    public ITEM_TYPE ItemType => itemType;
    public Sprite IconImage => iconImage;
    public Sprite ItemSprite => itemSprite;
    public string ItemName => itemName;
    public ITEM_RARITY Rarity => rarity;
    public string ItemInfo => itemInfo;
    public int MaxHaveIndex => maxHaveIndex;
    public bool IsSell => isSell;
    public bool IsBuy => isBuy;
    public int Price => price;
    public int SellPrice => sellPrice;
    public bool IsCollection => isCollection;
    public bool IsAbility => itemAbility.IsHaveAbility;


    /// <summary> ������ </summary>
    private void Awake()
    {
        if (!isBuy) price = 0;
        if (!isSell || isCollection) sellPrice = 0;
        if (!IsAbility) itemAbility.StatusClear();
    }

    /// <summary>
    /// �A�C�e���̌���
    /// </summary>
    [Serializable]
    sealed class ItemAbility
    {
        /// <summary> ���ʂ������Ă��邩 </summary>
        [SerializeField]
        private bool isHaveAbility;

        /*
         �̗͌��Z�l
        �L�݌��Z�l
        ���ʎ���
         
         */
        [SerializeField]
        [Header("���@_���A���A�b�v")]
        private int digRareRateUp;

        public bool IsHaveAbility => isHaveAbility;

        public int DigRareRateUp => digRareRateUp;
        /// <summary>
        /// �X�e�[�^�X�̃N���A
        /// </summary>
        public void StatusClear()
        {

        }

    }
}


