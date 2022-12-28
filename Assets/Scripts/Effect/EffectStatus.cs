using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// エフェクト状態
/// </summary>
public class EffectStatus : MonoBehaviour
{
    [SerializeField]
    private EffectManager.EFFECT_TYPE effType;

    /// <summary> 表示差分位置補正 <summary>
    [SerializeField]
    private Vector2 diffPos;

    public EffectManager.EFFECT_TYPE EffType => effType;
    public Vector2 DiffPos => diffPos;
    
}
