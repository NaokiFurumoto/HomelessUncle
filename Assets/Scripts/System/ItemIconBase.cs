using Carbon;
/// <summary>
/// �A�C�e���A�C�R���̊��N���X
/// </summary>
public abstract class ItemIconBase : RectTransformBehaviour
{
    public int Index { get; private set; } = 0;
    
    public void SetIndex(int index) 
    {
        Index = index;
    }
}
