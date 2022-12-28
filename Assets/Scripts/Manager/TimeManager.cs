using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static GlobalValue;

public class TimeManager : SingletonMonoBehaviour<TimeManager>
{
    
    //FPS
    public int FPS_NORMAL = 30;
    public int FPS_LOW = 20;

    public float FPS_60 = 1.0f / 60.0f;
    public float FPS_30 = 1.0f / 30.0f;
    public float FPS_20 = 1.0f / 20.0f;

    //�����p�����[�^�[
    [SerializeField] private bool m_Burdening = false;
    [SerializeField] private bool m_FixFrameRate = false;
    [SerializeField] private float m_SpeedRate = 1.0f;
    [SerializeField] private int m_FrameRate = 30;

    [SerializeField] private float m_DeltaTimeSystem = 0.0f;
    [SerializeField] private float m_DeltaTime = 0.0f;
    [SerializeField] private float m_UnscaledDeltaTime = 0.0f;
    private float m_Fps = 1.0f / 60.0f;

    private float m_LocalSpeedRate = 1.0f;

    private float m_SlowNowTime = 0.0f;
    private float m_SlowTime = 0.0f;
    private float m_SlowTime_S = 0.0f;
    private float m_SlowTime_E = 0.0f;
    private float m_SlowRate = 0.0f;

    [SerializeField] private float m_Time = 0.0f;
    [SerializeField] private float m_SinceTime = 0.0f;

    private long m_FrameCount = 0;
    private long m_FrameStartCount = 0;

    // �v���p�e�B
    #region �v���p�e�B

    // �t���[���J�E���g�̎擾
    public long FrameCount
    {
        get { return Instance.m_FrameCount; }
    }
    public long FrameDeltaCount
    {
        get { return Instance.m_FrameCount - Instance.m_FrameStartCount; }
    }

    // �����������[�g�̎擾
    public float DeltaFall
    {
        get { return Instance.m_DeltaTime * (float)Instance.m_FrameRate; }
    }

    // �t���[�����[�g�̎擾
    public int FrameRate
    {
        get { return Instance.m_FrameRate == -1 ? 60 : Instance.m_FrameRate; }
        set { Instance.m_FrameRate = value; }
    }

    // FPS�̎擾
    public float Fps
    {
        get { return Instance.m_Fps; }
    }

    // �ύX�V���Ԃ̎擾(���x���[�g�̉e�����󂯂Ȃ�)
    public float DeltaTimeSystem
    {
        get { return Instance.m_DeltaTimeSystem; }
    }

    // �ύX�V���Ԃ̎擾(���x���[�g�̉e�����󂯂�)
    public float DeltaTime
    {
        get { return Instance.m_DeltaTime; }
    }
    public float UnscaledDeltaTime
    {
        get { return Instance.m_UnscaledDeltaTime; }
    }

    // �Q�[���N������̌o�ߎ���
    public float Time
    {
        get { return Instance.m_Time; }
    }

    // ���x���[�g
    public float SpeedRate
    {
        set { Instance.m_SpeedRate = value; }
        get { return Instance.m_SpeedRate; }
    }

    // ���[�J�����x���[�g
    public float LocalSpeedRate
    {
        set { Instance.m_LocalSpeedRate = value; }
        get { return Instance.m_LocalSpeedRate; }
    }

    // ���x�����[�h����̌o�ߎ���
    public float SinceTime
    {
        get { return Instance.m_SinceTime; }
    }

    public bool isActiveSlow
    {
        get { return Instance.m_SlowTime > 0; }
    }
    #endregion



    /// <summary>
    /// ������
    /// </summary>
    private void Start()
    {
        Application.targetFrameRate = -1;
    }

    //�X�V
    private void Update()
    {
        float rate = 1.0f;

        //�t���[���J�E���g�ۑ�
        m_FrameCount = UnityEngine.Time.frameCount;

        // �t���[���J�n����
        m_Time += m_DeltaTime;

        if (m_SlowTime > 0)
        {
            m_SlowNowTime += UnityEngine.Time.unscaledDeltaTime;
            if (m_SlowNowTime >= m_SlowTime)
            {
                m_SlowTime = 0.0f;
            }
            else
            {
                float def = rate;
                rate = m_SlowRate;

                if (m_SlowNowTime < m_SlowTime_S)
                {
                    float t = m_SlowNowTime / m_SlowTime_S;
                    rate = def + (m_SlowRate - def) * t;
                }
                else if (m_SlowNowTime > m_SlowTime_E)
                {
                    float t = (m_SlowNowTime - m_SlowTime_E) / (m_SlowTime - m_SlowTime_E);
                    rate = m_SlowRate + (def - m_SlowRate) * t;
                }
            }
        }


        if (Application.targetFrameRate != m_FrameRate)
        {
            if (m_FrameRate != -1)
            {
                QualitySettings.vSyncCount = 0;
            }
            else
            {
                QualitySettings.vSyncCount = 1;
            }
            Application.targetFrameRate = m_FrameRate;
        }

        if (Application.targetFrameRate != -1)
        {
            m_Fps = 1.0f / (float)Application.targetFrameRate;
        }

        if (m_FixFrameRate == false)
        {
            // �ώ���
            m_DeltaTimeSystem =
            m_DeltaTime = UnityEngine.Time.deltaTime;
            m_UnscaledDeltaTime = UnityEngine.Time.timeScale == 0 ? UnityEngine.Time.fixedDeltaTime : UnityEngine.Time.deltaTime / UnityEngine.Time.timeScale;
            m_SinceTime = UnityEngine.Time.timeSinceLevelLoad;
        }
        else
        {
            // �Œ莞��
            m_DeltaTimeSystem =
            m_DeltaTime = m_Fps * m_SpeedRate * m_LocalSpeedRate * rate;
            m_UnscaledDeltaTime = m_Fps;
        }

        // �[���������׃��[�h
        if (m_Burdening && m_FrameRate > 0)
        {
            Application.targetFrameRate = (int)(m_FrameRate + (float)m_FrameRate * UnityEngine.Random.Range(-0.5f, 0.0f));
        }

        // ���x���[�g���|����
        UnityEngine.Time.timeScale = m_SpeedRate * m_LocalSpeedRate * rate;
    }

    //=========================================================================
    //. �X���[
    //=========================================================================
    #region �X���[

    /// ***********************************************************************
    /// <summary>
    /// �X���[�J�n
    /// </summary>
    /// <param name="time">�X���[�ő厞��</param>
    /// <param name="time_s">�X���[�J�n����</param>
    /// <param name="time_e">�X���[�߂莞��</param>
    /// <param name="rate">�X���[���[�g</param>
    /// <returns></returns>
    /// <remarks></remarks>
    /// ***********************************************************************
    public void SetSlow(float time, float time_s, float time_e, float rate)
    {
        m_SlowNowTime = 0.0f;
        m_SlowTime = time;
        m_SlowTime_S = time_s;
        m_SlowTime_E = time_e;
        m_SlowRate = rate;
    }

    public void SetSlow(float time, float rate)
    {
        SetSlow(time, 0.0f, time, rate);
    }

    public void SetSlow(float time, float time_e, float rate)
    {
        SetSlow(time, 0.0f, time_e, rate);
    }

    /// ***********************************************************************
    /// <summary>
    /// �X���[����
    /// </summary>
    /// ***********************************************************************
    public void ResetSlow()
    {
        m_SlowTime = 0;
    }
    #endregion

    //=========================================================================
    //. �ݒ�/�擾
    //=========================================================================
    #region �ݒ�/�擾

    /// ***********************************************************************
    /// <summary>
    /// �t���[���J�E���g�̏����l��ݒ�
    /// </summary>
    /// ***********************************************************************
    public void SetFrameStartCount()
    {
        TimeManager inst = TimeManager.Instance;

        inst.m_FrameStartCount = inst.m_FrameCount;
    }

    /// ***********************************************************************
    /// <summary>
    /// �W���𑬓x�{���ɍ��킹�ĕ␳
    /// </summary>
    /// ***********************************************************************
    public float CalcValueSpeedRate(ref float value, float rate)
    {
        TimeManager inst = TimeManager.Instance;

        value *= rate;
        float result = value;
        if (inst != null)
        {
            float speed = inst.m_SpeedRate * inst.m_LocalSpeedRate;
            for (int i = 1; i < speed; ++i)
            {
                value *= rate;
                result += value;
            }
        }

        return result;
    }

    #endregion

    /// <summary>
    /// ���݂̓������擾
    /// </summary>
    /// <returns></returns>
    public DateTime GetDayTime()
    {
        return DateTime.Now;
    }

    /// <summary>
    /// ���݂̓��t��Long�^�̐����l�ɕϊ�
    /// </summary>
    /// <returns></returns>
    public long GetDayTimeInteger()
    {
        var now = GetDayTime();
        return now.Year * 10000000000 + 
               now.Month * 100000000 + 
               now.Day * 1000000 + 
               now.Hour * 10000 + 
               now.Minute * 100 + 
               now.Second;
    }

}
