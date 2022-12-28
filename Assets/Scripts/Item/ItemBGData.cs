using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ItemBGData", menuName = "CreateItemBGData")]
public class ItemBGData : ScriptableObject
{
    /// <summary> アイコン画像 </summary>
    [SerializeField]
    [Header("背景画像")]
    private List<Sprite> bgImages;

    public List<Sprite> BGImagesList => bgImages;

}
