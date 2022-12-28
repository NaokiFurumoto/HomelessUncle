using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLEffectPosType : MonoBehaviour
{
    [SerializeField]
    private PlayerEffectRoot.ROOT_TYPE effPosType;
    public PlayerEffectRoot.ROOT_TYPE EffPosType => effPosType;
}
