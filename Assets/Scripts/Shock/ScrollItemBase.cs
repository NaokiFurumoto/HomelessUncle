using Carbon;

namespace Shock
{
	/// <summary>
	/// スクロールアイテム基底
	/// </summary>
	public abstract class ScrollItemBase : RectTransformBehaviour
	{
		public int Index { get; private set; } = 0;

		public void SetIndex(int index)
		{
			Index = index;
		}
	}
}

//using Carbon;
//using UnityEngine;
//using UnityEngine.UI;

//namespace Shock
//{
//    [DisallowMultipleComponent]
//    public sealed class MultiRewardDialogScrollItem : ScrollItemBase
//    {
//        [SerializeField] private RewardIcon m_rewardIcon = null;
//        [SerializeField] private RewardDataNode m_rewardDataNode = null;
//        [SerializeField] private Image m_getIcon = null;
//        [SerializeField] private TextMeshUI m_descText;

//        public void Setup(RewardData rewardData, bool isGetItem)
//        {
//            m_rewardIcon.Load(rewardData, RewardIcon.Visibility.All, null);
//            m_rewardDataNode.Broadcast(rewardData);
//            m_getIcon.TrySetActive(isGetItem);
//            m_rewardIcon.SetGrayout(isGetItem);
//            if (!rewardData.Reason.IsNullOrEmpty())
//            {
//                m_descText.SetActive(true);
//                m_descText.SetText(rewardData.Reason);
//            }
//            else
//            {
//                m_descText.SetActive(false);
//            }
//        }
//    }
//}