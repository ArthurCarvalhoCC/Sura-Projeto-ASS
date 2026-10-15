public class DurableItem : BaseItem, IDurableItem
{
    private int durability;
    private int toolDamage;
    public int getDurability()
    {
        return durability;
    }

    public int getToolDamage()
    {
        return toolDamage;
    }

    protected void changeDurability(int durability)
    {
        this.durability = durability;
    }
    protected void changeToolDamage(int toolDamage)
    {
        this.toolDamage = toolDamage;
    }
}