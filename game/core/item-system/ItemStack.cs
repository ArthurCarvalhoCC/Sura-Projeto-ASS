using System;

public class ItemStack
{
    protected BaseItem item;
    protected int quantity;

    protected ItemStack(BaseItem item, int quantity)
    {
        this.item = item;
        this.quantity = quantity;
    }

    public static ItemStack of(BaseItem item, int quantity = 1)
    {
        if (item is IDurableItem durableItem)
        {
            if (durableItem is IStackableItem)
            {
                throw new Exception("Item durável não pode ser estocável!");
            }
            if (quantity != 1)
            {
                throw new Exception("Item durável não pode ter stack maior que 1");;
            }
        }
        if (item is IStackableItem stackableItem)
        {
            if (quantity > stackableItem.getMaxStackSize())
            {
                throw new Exception("Quantidade maior que stack máximo!");
            }
        }
        return new ItemStack(item, quantity);
    }

    public int getQuantity()
    {
        return this.quantity;
    }
    public void setQuantity(int value)
    {
        this.quantity = value;
    }
    public BaseItem getItem()
    {
        return this.item;
    }
    public bool canStackWith(ItemStack other)
    {
        return getItem().getId() == other.getItem().getId()
            && componentsEqual(other);
    }
    public bool componentsEqual(ItemStack other)
    {
        return true; // TODO
    }

}