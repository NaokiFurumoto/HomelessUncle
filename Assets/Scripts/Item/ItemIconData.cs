using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// �A�C�e���A�C�R���̃f�[�^
/// </summary>
public struct ItemIconData
{
    /// <summary> �A�C�R���C���[�W </summary>
    public Sprite ItemImage;

    /// <summary> ���A���e�B </summary>
    public ItemData.ITEM_RARITY Rarity;

    /// <summary> �A�C�R���f�[�^ </summary>
    public ItemData Data;

    /// <summary> ���O </summary>
    public string ItemName;

    /// <summary> ������ </summary>
    public int HoldIndex;

    /// <summary>
    /// �f�[�^�̐ݒ�
    /// </summary>
    /// <param name="data"></param>
    /// <param name="index"></param>
    public void SetIconsData(ItemData�@data,int index)
    {
        ItemImage = data.IconImage;
        Rarity = data.Rarity;
        ItemName = data.ItemName;
        HoldIndex = index;
        Data = data;
    }
}
