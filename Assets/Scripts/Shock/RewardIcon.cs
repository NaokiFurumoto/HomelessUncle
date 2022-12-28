using Carbon;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Shock
{
    /// <summary>
    /// 報酬アイコン
    /// </summary>
    public sealed class RewardIcon : RectTransformBehaviour
    {
        //    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //    // Definition
        //    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //    /// <summary>
        //    /// 部品
        //    /// </summary>
        //    public enum Part : int
        //    {
        //        /// <summary> 本体 </summary>
        //        Body,

        //        /// <summary> 個数 </summary>
        //        Amount,
        //    }

        //    /// <summary>
        //    /// 表示設定定数クラス
        //    /// </summary>
        //    public static class Visibility
        //    {
        //        /// <summary>
        //        /// デフォルト表示設定
        //        /// </summary>
        //        public static readonly HashSet<Part> Default = new HashSet<Part>
        //        {
        //            Part.Body,
        //            //Part.Amount,
        //        };

        //        /// <summary>
        //        /// ALL 表示設定
        //        /// </summary>
        //        public static readonly HashSet<Part> Core = new HashSet<Part>
        //        {
        //            Part.Body,
        //        };

        //        /// <summary>
        //        /// ALL 表示設定
        //        /// </summary>
        //        public static readonly HashSet<Part> All = new HashSet<Part>
        //        {
        //            Part.Body,
        //            Part.Amount,
        //        };
        //    }

        //    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //    // SerializeField
        //    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //    /// <summary>
        //    /// 下地 Image
        //    /// </summary>
        //    [SerializeField] private RewardIconCore m_Core = null;

        //    /// <summary>
        //    /// 報酬アイコン(Card / SubDeckCardIcon.prefab)
        //    /// </summary>
        //    [SerializeField] private CardIcon m_CardIcon = null;

        //    /// <summary>
        //    /// 個数設置 root
        //    /// </summary>
        //    [SerializeField] private GameObject m_AmountRoot = null;

        //    /// <summary>
        //    /// 個数 Text
        //    /// </summary>
        //    [SerializeField] private TextMeshUI m_AmountText = null;

        //    /// <summary>
        //    /// Loading Mark
        //    /// </summary>
        //    [SerializeField] private GameObject m_LoadingMark = null;

        //    /// <summary>
        //    /// ボタン
        //    /// </summary>
        //    [SerializeField] private ButtonEvent m_Button = null;

        //    /// <summary>
        //    /// グレーアウト
        //    /// </summary>
        //    [SerializeField] private GrayoutUI m_GrayoutUI = null;

        //    /// <summary>
        //    /// アイコン下地
        //    /// </summary>
        //    [SerializeField] private GameObject m_iconBase = null;
        //    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //    // Field
        //    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //    /// <summary>
        //    /// 報酬値
        //    /// </summary>
        //    private uint m_RewardValue = 0;

        //    /// <summary>
        //    /// 報酬タイプ
        //    /// </summary>
        //    private REWARD_TYPE m_RewardType = REWARD_TYPE.NONE;

        //    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //    // Public Method
        //    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //    /// <summary>
        //    /// リソース解放
        //    /// </summary>
        //    public void ReleaseResource()
        //    {
        //        m_Core.ReleaseResource();
        //    }

        //    /// <summary>
        //    /// ロード
        //    /// </summary>
        //    /// <param name="rewardType">報酬タイプ</param>
        //    /// <param name="rewardValue">報酬値</param>
        //    /// <param name="amount">個数</param>
        //    /// <param name="visibility">表示設定</param>
        //    /// <param name="onComplete">ロード完了コールバック</param>
        //    public void Load(REWARD_TYPE rewardType, uint rewardValue, int amount, HashSet<Part> visibility,
        //        Action onComplete)
        //    {
        //        m_RewardType = rewardType;
        //        m_RewardValue = rewardValue;

        //        // 表示設定
        //        SetVisibility(visibility);

        //        // extension ------------------------------------------------------
        //        // 個数設定
        //        SetAmount(amount);

        //        // 下地設定
        //        SetBaseActive(rewardType.IsOrnament());

        //        // core -----------------------------------------------------------
        //        // Loading Mark 表示 && Core 非表示
        //        m_LoadingMark.SetActive(true);
        //        m_Core.SetActive(false);
        //        // card
        //        if (m_CardIcon != null)
        //        {
        //            m_CardIcon.SetActive(false);
        //        }

        //        if (rewardType.IsCard() && m_CardIcon != null)
        //        {
        //            // Card
        //            CardData cardData = CardData.CreateSampleCard(rewardValue);
        //            m_CardIcon.Load(cardData, CardIcon.Visibility.NoParam, () =>
        //            {
        //                m_CardIcon.ChangeActive(true);
        //                m_LoadingMark.SetActive(false);
        //                // ロード完了コールバック
        //                onComplete.Call();
        //            });
        //        }
        //        else if (m_Core != null)
        //        {
        //            // ロード開始
        //            m_Core.Load(m_RewardType, m_RewardValue, () =>
        //            {
        //                // Core 表示 && Loading Mark 非表示
        //                m_Core.SetActive(true);
        //                m_LoadingMark.SetActive(false);
        //                // ロード完了コールバック
        //                onComplete.Call();
        //            });
        //        }
        //    }

        //    /// <summary>
        //    /// ロード
        //    /// </summary>
        //    public void Load(REWARD_TYPE rewardType, uint rewardValue, int amount, Action onComplete)
        //    {
        //        Load(rewardType, rewardValue, amount, Visibility.Default, onComplete);
        //    }

        //    /// <summary>
        //    /// ロード
        //    /// </summary>
        //    public void Load(REWARD_TYPE rewardType, uint rewardValue, Action onComplete)
        //    {
        //        Load(rewardType, rewardValue, 0, Visibility.Default, onComplete);
        //    }

        //    public void Load(RewardData rewardData, HashSet<Part> visibility, Action onComplete)
        //    {
        //        Load(rewardData.RewardType, rewardData.RewardValue, rewardData.RewardAmount, visibility, onComplete);
        //    }

        //    public void Load(RewardData rewardData, Action onComplete)
        //    {
        //        Load(rewardData, Visibility.Default, onComplete);
        //    }

        //    /// <summary>
        //    /// IRewardMst でロード
        //    /// </summary>
        //    public void Load(IRewardMst mst, HashSet<Part> visibility, Action onComplete)
        //    {
        //        Load(mst.Type, mst.Value, (int)mst.Amount, visibility, onComplete);
        //    }

        //    /// <summary>
        //    /// IRewardMst でロード
        //    /// </summary>
        //    public void Load(IRewardMst rewardMst, Action onComplete)
        //    {
        //        Load(rewardMst, Visibility.Default, onComplete);
        //    }

        //    /// <summary>
        //    /// ProtocolData.Reward でロード
        //    /// </summary>
        //    public void Load(ProtocolData.Reward reward, HashSet<Part> visibility, Action onComplete)
        //    {
        //        Load(reward.Type, reward.value, (int)reward.amount, visibility, onComplete);
        //    }

        //    /// <summary>
        //    /// ProtocolData.Reward でロード
        //    /// </summary>
        //    public void Load(ProtocolData.Reward reward, Action onComplete)
        //    {
        //        Load(reward.Type, reward.value, (int)reward.amount, Visibility.Default, onComplete);
        //    }

        //    /// <summary>
        //    /// 表示設定
        //    /// </summary>
        //    public void SetVisibility(HashSet<Part> visibility)
        //    {
        //        SetAmountActive(visibility.Contains(Part.Amount));
        //    }

        //    /// <summary>
        //    /// 個数設定
        //    /// </summary>
        //    public void SetAmount(int amount)
        //    {
        //        if (m_AmountText != null)
        //        {
        //            m_AmountText.SetText(RewardUtils.GetRewardAmountX(m_RewardType, amount));
        //        }
        //    }

        //    /// <summary>
        //    /// 個数文字設定
        //    /// </summary>
        //    public void SetAmount(string amountString)
        //    {
        //        if (m_AmountText != null)
        //        {
        //            m_AmountText.SetText(amountString);
        //        }
        //    }

        //    /// <summary>
        //    /// ボタン有効設定
        //    /// </summary>
        //    public void SetButtonEnable(bool value)
        //    {
        //        m_Button.SetEnable(value);
        //    }

        //    /// <summary>
        //    /// ボタンコールバック設定
        //    /// </summary>
        //    public void SetButtonAction(Action onClick, Action onLongPress)
        //    {
        //        m_Button.OnClick = onClick;
        //        m_Button.OnLongPress = onLongPress;
        //    }

        //    /// <summary>
        //    /// グレーアウト設定
        //    /// </summary>
        //    public void SetGrayout(bool value)
        //    {
        //        m_GrayoutUI.SetGrayout(value);
        //        if (m_CardIcon)
        //        {
        //            m_CardIcon.SetGrayout(value);
        //        }
        //    }

        //    /// <summary>
        //    /// 個数
        //    /// </summary>
        //    private void SetAmountActive(bool value)
        //    {
        //        if (m_AmountRoot != null)
        //        {
        //            m_AmountRoot.TrySetActive(value && m_RewardType.IsCountable());
        //        }
        //    }

        //    /// <summary>
        //    /// 下地セット
        //    /// </summary>
        //    /// <param name="value"></param>
        //    private void SetBaseActive(bool value)
        //    {
        //        m_iconBase.TrySetActive(value);
        //    }

        //    /// <summary>
        //    /// CoreBodyへのマテリアル設定
        //    /// </summary>
        //    public void SetCoreBodyMaterial(bool value)
        //    {
        //        m_Core.SetCompleteMissionMaterial(value);
        //    }
        //}
    }
}
