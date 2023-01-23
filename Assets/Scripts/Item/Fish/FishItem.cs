using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishItem : MonoBehaviour
{
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
