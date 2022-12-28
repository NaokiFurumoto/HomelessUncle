using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 掘り出されたアイテムに関するクラス
/// </summary>
public class DigItem : MonoBehaviour
{
    private Animator anim;
    private SpriteRenderer sprite;


    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    public void SetImage(ItemData data)
    {
        if (data == null)
            return;
        this.sprite.sprite = data.ItemSprite;
    }

}
