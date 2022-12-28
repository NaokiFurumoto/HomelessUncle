using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerEffectRoot : MonoBehaviour
{
   public enum ROOT_TYPE
    {
        NONE,
        TOP,
        CENTER,
        UNDER,
        RIGHT,
        LEFT
    }

    public static PlayerEffectRoot Instance;

    private void Awake()
    {
        Instance ??= this;
    }

    [SerializeField]
    private List<PLEffectPosType> effectRootPosList = new List<PLEffectPosType>();

    /// <summary>
    /// 指定したエフェクトの親を取得
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public Transform GetEffectRoot(ROOT_TYPE type)
    {
        if (effectRootPosList?.Count > 0)
        {
            return effectRootPosList.FirstOrDefault(trans => trans.EffPosType == type).transform;
        }

        return default;
    }

}
