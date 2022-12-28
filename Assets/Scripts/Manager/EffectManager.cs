using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
/// <summary>
/// エフェクトの管理
/// </summary>
public class EffectManager : MonoBehaviour
{
    /// <summary> エフェクトの種類:作成後追加 <summary>
    public enum EFFECT_TYPE
    {
        NONE,
        DIG_SCOP,
    }

    public static EffectManager Instance;

    private PlayerEffectRoot effRoot;

    [SerializeField]
    private List<EffectStatus> effectList = new List<EffectStatus>();

    private void Awake()
    {
        Instance ??= this;
    }

    private void Start()
    {
        effRoot = PlayerEffectRoot.Instance;
    }

    /// <summary>
    /// プレイヤーに関するエフェクト表示
    /// </summary>
    /// <param name="effType"></param>
    /// <param name="rootType"></param>
    public void PlayEffectPlayer(EFFECT_TYPE effType,PlayerEffectRoot.ROOT_TYPE rootType)
    {
        if (!(effectList?.Count > 0))
            return;

        EffectStatus selectObj = effectList.FirstOrDefault(eff => eff.EffType == effType);
        Vector2 diff = selectObj.DiffPos;

        Transform parent = effRoot?.GetEffectRoot(rootType);

        var obj = Instantiate(selectObj.gameObject, Vector2.zero, Quaternion.identity, parent);
        obj.transform.localPosition = new Vector2(obj.transform.position.x + diff.x, obj.transform.position.y + diff.y);
    }
}
