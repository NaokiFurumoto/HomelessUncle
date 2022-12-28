using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// 時計クラス。
/// 現在の時間と同期。ここから時間を取得
/// </summary>
public class ClockController : MonoBehaviour
{
    [SerializeField] private Transform shortTrans;
    [SerializeField] private Transform longTrans;

    private int minute;
    private int hour;
    private float delay;

    public int Minute => minute;
    public int Hour => hour;

    private void Start()
    {
        //イベントチェック
        delay = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        minute = DateTime.Now.Minute;
        longTrans.localEulerAngles = new Vector3(0, 0, -360 / 60.0f * minute);

        hour = DateTime.Now.Hour;
        float HHA = -((360 / 12.0f * hour) + (30 / 60.0f * minute));
        shortTrans.localEulerAngles = new Vector3(0, 0, HHA);
    }

    /// <summary>
    /// 1分に1度イベント発行チェック
    /// </summary>
    private void LateUpdate()
    {
        //1分ごとにイベントチェック
        delay += Time.unscaledDeltaTime;
        if(delay > 60.0f)
        {
            //イベントチェック
            delay = 0.0f;
        }
    }

    /// <summary>
    /// 時間を取得
    /// </summary>
    /// <returns></returns>
    public (int,int) GetClockTime()
    {
        return (hour, minute);
    }
}
