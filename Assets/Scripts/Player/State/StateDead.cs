using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 各ステートごとに記載
/// </summary>
public partial class Player
{
    /// <summary>
    /// 死亡
    /// </summary>
    public class StateDead : PlayerStateBase
    {
        public override void OnUpdate(Player player) { }
    }
}
