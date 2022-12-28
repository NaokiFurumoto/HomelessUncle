using UnityEngine;

namespace Carbon
{
	/// <summary>
	/// 空中継ノード (データ中継を中断するノード)
	/// </summary>
	[DisallowMultipleComponent, ComponentOrder(0)]
	public sealed class VoidDataRelayNode : CarbonBehaviour, IDataRelayNode
	{
		// empty
	}
}
