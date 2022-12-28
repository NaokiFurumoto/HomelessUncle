using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Carbon;
using TMPro;
/// <summary>
/// �A�C�R���̏����Z�b�g
/// </summary>
public class ItemIcon : ItemIconBase
{
    /// <summary> �C���[�W </summary>
    [SerializeField]
    private Image itemImage;

    /// <summary> �{�^�� </summary>
    [SerializeField]
    private ButtonEventSystem button;

    /// <summary> TextMeshUI </summary>
    [SerializeField]
    private TextMeshProUGUI txt_itemName;

    /// <summary> TextMeshUI </summary>
    [SerializeField]
    private TextMeshProUGUI txt_holdIndex;

    /// <summary> TextMeshUI </summary>
    [SerializeField]
    private ItemBGData bgData;

    /// <summary> �f�[�^ </summary>
    [SerializeField]
    private ItemData data;

    //�����o�ϐ�
    /// <summary> ���O </summary>
    private string itemName;

    /// <summary> ������ </summary>
    private int holdIndex;

    /// <summary> �w�i�C���[�W </summary>
    [SerializeField]
    private Image bgImage;


    //�v���p�e�B
    public string ItemName => itemName;
    public int HoldIndex => holdIndex;
    public Image ItemImage => itemImage;
    public ItemData Data => data;
   

    public void Setup(ItemIconData iconData)
    {
        itemImage.sprite = iconData.ItemImage;
        itemName = iconData.ItemName;
        holdIndex = iconData.HoldIndex;
        this.data = iconData.Data;
        bgImage.sprite = bgData.BGImagesList[(int)this.data.Rarity];

        txt_itemName.text = itemName.ToString();
        txt_holdIndex.text = holdIndex.ToString();
    }

}
