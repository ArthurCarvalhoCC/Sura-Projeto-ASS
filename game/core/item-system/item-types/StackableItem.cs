public class StackableItem : BaseItem, IStackableItem
{
    private int maxStackSize;
    public int getMaxStackSize()
    {
        return maxStackSize;
    }
    protected void changeMaxStackSize(int maxStackSize)
    {
        this.maxStackSize = maxStackSize;
    }
}