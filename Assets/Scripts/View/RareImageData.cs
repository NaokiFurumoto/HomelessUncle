
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "RareImageData", menuName = "CreateRareImageData")]
public class RareImageData : ScriptableObject
{
    /// <summary> アイコン画像 </summary>
    [SerializeField]
    [Header("レアリティ画像")]
    private List<Sprite> rareImages;

    public List<Sprite> RareImagesList => rareImages;

}
