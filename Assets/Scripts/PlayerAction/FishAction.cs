using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static GlobalValue;
using UnityEngine.EventSystems;

/// <summary>
/// 釣りアクション：基本共通クラスが欲しい
/// </summary>
public class FishAction : GameAction
{
    /// <summary>
    /// ウキのアクション状態
    /// </summary>
    private enum UKIACTION
    {
        HIT = 0,
        PIKU = 1,
        MOVE = 2
    }

    /// <summary>
    /// ウキのアニメーショントリガー名
    /// </summary>
    private readonly string[] ukiTriggerName = { "Hit", "Piku", "Move" };

    [SerializeField]
    private GameObject uki;
    private Animator ukiAnim;
    private EventTrigger ukiEventTrigger;
    private bool isTapUki;

    private Animator playerAnim;

    protected override void Initialize()
    {
        base.Initialize();
        uki ??= transform.Find("Uki").gameObject;
        ukiAnim = uki?.transform.Find("Body").GetComponent<Animator>();
        SetUkiEvent(uki);
        isTapUki = false;
        DataList = ItemManager.Instance.FishItemDataLists;
        playerAnim = player.PlayerAnim;
    }

    /// <summary>
    /// ウキアクションコールバックの設定
    /// </summary>
    /// <param name="uki"></param>
    private void SetUkiEvent(GameObject uki)
    {
        if (uki == null)
            return;

        ukiEventTrigger = uki.GetComponent<EventTrigger>();
        if (ukiEventTrigger == null)
        {
            Debug.Log("ウキが取得出来ていません！！");
            return;
        }

        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerDown;
        entry.callback.AddListener((data) => { TapUki(); });

        ukiEventTrigger?.triggers.Add(entry);
    }

    /// <summary>
    /// ウキをタップした時のアクション
    /// </summary>
    public void TapUki()
    {
        if (isTapUki)
            return;

        //後でリセット
        isTapUki = true;

        //プレイヤーのアニメーション
        playerAnim.SetTrigger("FishPlay");
        //ウキヒット中アニメーション
        ukiAnim.SetTrigger("Catch");

        StartCoroutine(UkiAction());
    }

    /// <summary>
    /// ウキのアクション制御
    /// </summary>
    /// <returns></returns>
    private IEnumerator UkiAction()
    {
        //タップした時のアニメーションの状態の取得
        AnimatorStateInfo currentInfo = ukiAnim.GetCurrentAnimatorStateInfo(0);
        //if (currentInfo.IsName("Uki_Hit"))
        //{
        //    //魚が釣れる
        //    var fishItem = GetItem();
        //    yield return new WaitForSeconds(FISH_GETACTIVE_TIME);
        //    SetActiveUki(false);
        //    FishItemGenerate(fishItem);

        //    //アイテムの保存
        //    player.SetHoldPlayerItem(fishItem, 1);
        //}
        //else
        //{
        //    //魚が釣れない
        //    yield return new WaitForSeconds(FISH_GETACTIVE_TIME);
        //    SetActiveUki(false);
        //}

        //魚が釣れる:Debug用コード
        var fishItem = GetItem();
        yield return new WaitForSeconds(FISH_GETACTIVE_TIME);
        SetActiveUki(false);
        FishItemGenerate(fishItem);

        //アイテムの保存
        player.SetHoldPlayerItem(fishItem, 1);

        //秒後にプレイヤーのエンドを呼ぶ
        yield return new WaitForSeconds(FISH_END_TIME);
        player.Fishing.FishingEnd();
    }

    /// <summary>
    /// ウキの表示切替
    /// </summary>
    /// <param name="enable"></param>
    public void SetActiveUki(bool enable)
    {
        uki.SetActive(enable);
    }

    /// <summary>
    /// 釣り開始
    /// </summary>
    public void FishingStart()
    {
        isTapUki = false;
        StartCoroutine(Fishing());
    }

    /// <summary>
    /// 釣りのアクション
    /// </summary>
    /// <returns></returns>
    private IEnumerator Fishing()
    {
        yield return new WaitForSeconds(FISH_UKIACTIVE_TIME);
        SetActiveUki(true);

        float idleValue = Random.Range(FISH_IDLEMINTIMES, FISH_IDLEMAXTIMES);
        yield return new WaitForSeconds(idleValue);

        var hit = false;
        while (!hit)
        {
            //待機
            float waitValue = Random.Range(FISH_MINTIMES, FISH_MAXTIMES);
            yield return new WaitForSeconds(waitValue);

            int actionNum = Random.Range(0, FISH_ACTIONINDEX);
            string actionName = ukiTriggerName[actionNum];
            ukiAnim.SetTrigger(actionName);

            if (actionName == "Hit")
            {
                hit = true;
                yield break;
            }
        }
    }

    /// <summary>
    /// 釣りアイテムを表示
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    private void FishItemGenerate(ItemData data)
    {
        GameObject itemObj = (GameObject)Resources.Load("Prefabs/FishItem");
        var parent = PlayerEffectRoot.Instance.GetEffectRoot(PlayerEffectRoot.ROOT_TYPE.RIGHT);
        GameObject instace = Instantiate(itemObj, parent.position, Quaternion.Euler(0, 0, -90), parent);

        //var status = instace?.GetComponent<FishItem>();
        //status?.SetImage(data);
    }

}


