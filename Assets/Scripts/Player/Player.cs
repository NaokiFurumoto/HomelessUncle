using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

/// <summary>
/// プレイヤー本体
/// </summary>
public partial class Player : MonoBehaviour
{
    [SerializeField]
    protected PlayerEquip equip;

    /// <summary> プレイヤーの初期化完了フラグ <summary>
    private bool isInitialized;

    public PlayerEquip Equip => equip;
    public bool IsInitialized => isInitialized;


    void Awake()
    {
        isInitialized = false;
        StateAwake();
        MoveAwake();

        //後で削除
        AwakeHoldItemes();
    }

    /// <summary>
    /// コルーチン化する
    /// </summary>
    async void Start()
    {
        //ゲームコントローラーがシステムロードまち
        //そのあとプレイヤー

        //各種ロード完了待ち？？

        //whileでload完了待ち？？
        //yield return null;
        equip ??= GameObject.FindGameObjectWithTag("Equip")
                            .GetComponent<PlayerEquip>();
        //ステートに関する初期化
        StateStart();
        //移動に関する初期化
        MoveStart();
        //ステータス初期化
        playerStatus.SetInitializeStatus();
        //ステータス反映
        await playerStatus.SetLoadedStatus();
        isInitialized = true;
    }

    // Update is called once per frame
    void Update()
    {
        //ステート更新
        StateUpdate();
    }

    void LateUpdate()
    {
        
        //移動更新
        MoveUpdate();
    }

    //衝突
    private void OnCollisionEnter(Collision colliion)
    {
        // ChangeState(stateIdle);
    }
}
