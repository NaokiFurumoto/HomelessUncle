using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// ���v�N���X�B
/// ���݂̎��ԂƓ����B�������玞�Ԃ��擾
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
        //�C�x���g�`�F�b�N
        delay = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        minute = DateTime.Now.Minute;
        longTrans.localEulerAngles = new Vector3(0, 0, -360 / 60.0f * minute);

        hour = DateTime.Now.Hour;
        Debug.Log(hour);
        float HHA = -((360 / 12.0f * hour) + (30 / 60.0f * minute));
        shortTrans.localEulerAngles = new Vector3(0, 0, HHA);
    }

    /// <summary>
    /// 1����1�x�C�x���g���s�`�F�b�N
    /// </summary>
    private void LateUpdate()
    {
        //1�����ƂɃC�x���g�`�F�b�N
        delay += Time.unscaledDeltaTime;
        if(delay > 60.0f)
        {
            //�C�x���g�`�F�b�N
            delay = 0.0f;
        }
    }

    /// <summary>
    /// ���Ԃ��擾
    /// </summary>
    /// <returns></returns>
    public (int,int) GetClockTime()
    {
        return (hour, minute);
    }
}
