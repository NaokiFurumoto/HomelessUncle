using Carbon;
/// <summary>
/// アイテムアイコンの基底クラス
/// </summary>
public abstract class ItemIconBase : RectTransformBehaviour
{
    public int Index { get; private set; } = 0;
    
    public void SetIndex(int index) 
    {
        Index = index;
    }
}
