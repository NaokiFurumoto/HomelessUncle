using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �G�t�F�N�g���
/// </summary>
public class EffectStatus : MonoBehaviour
{
    [SerializeField]
    private EffectManager.EFFECT_TYPE effType;

    /// <summary> �\�������ʒu�␳ <summary>
    [SerializeField]
    private Vector2 diffPos;

    public EffectManager.EFFECT_TYPE EffType => effType;
    public Vector2 DiffPos => diffPos;
    
}
