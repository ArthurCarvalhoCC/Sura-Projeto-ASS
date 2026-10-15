using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Godot.Collections;

#nullable enable
public sealed class Inventory
{
    private int slots;
    public int getSlots()
    {
        return slots;
    }
    public void setSlots(int value)
    {
        this.slots = value;
    }
    private readonly List<ItemStack> stacks = new();
    public List<ItemStack> getStacks()
    {
        return new List<ItemStack>([..stacks]);
    }

    public Inventory(List<ItemStack> stacks) {
        this.stacks = stacks;
    }
    public static Inventory empty()
    {
        return new Inventory([]);
    }
    public ItemStack? getSlot(int slot)
    {
        if (slot > slots)
            throw new System.Exception("Slot fora de alcance");
        if (slot >= this.stacks.Count)
            return null;
        return this.stacks.ElementAtOrDefault(slot);
    }

    public int giveItemStack(ItemStack incoming)
    {
        int remaining = incoming.getQuantity();

        int maxStackSize = incoming.getItem() is IStackableItem stackable
            ? stackable.getMaxStackSize()
            : 1;

        if (maxStackSize < 1)
            throw new InvalidOperationException("MaxStackSize inválido.");

        // Preencher stacks existentes
        foreach (ItemStack? stack in stacks)
        {
            if (remaining <= 0)
                break;

            if (stack == null || stack.getItem().getId() != incoming.getItem().getId())
                continue;

            int available = maxStackSize - stack.getQuantity();
            int amount = Math.Min(Math.Max(available, 0), remaining);

            stack.setQuantity(stack.getQuantity() + amount);
            remaining -= amount;
        }

        // Preencher slots vazios
        for (int i = 0; i < stacks.Count && remaining > 0; i++)
        {
            if (stacks.ElementAt(i) != null)
                continue;

            int amount = Math.Min(maxStackSize, remaining);

            stacks[i] = ItemStack.of(incoming.getItem(), amount);
            remaining -= amount;
        }

        return remaining;
    }

    public static Inventory ofSlots(int slots)
    {
        var list = new List<ItemStack>();
        for (int i = 0; i < slots; i++) list.Add(null!);
        Inventory inventory = new Inventory(list);
        inventory.setSlots(slots);
        return inventory;
    }

    public void setSlot(int slot, ItemStack? stack)
    {
        ensureSlot(slot);
        stacks[slot] = stack!;
    }
    public void clearSlot(int slot)
    {
        ensureSlot(slot);
        stacks[slot] = null!;
    }
    public ItemStack? removeFromSlot(int slot, int amount)
    {
        ItemStack? stack = getSlot(slot);
        if (stack == null || amount <= 0) return null;

        int removed = Math.Min(amount, stack.getQuantity());
        ItemStack result = ItemStack.of(stack.getItem(), removed);
        if (removed >= stack.getQuantity()) clearSlot(slot);
        else stack.setQuantity(stack.getQuantity() - removed);
        return result;
    }
    public void swapSlots(int first, int second)
    {
        ensureSlot(first);
        ensureSlot(second);
        (stacks[first], stacks[second]) = (stacks[second], stacks[first]);
    }
    private void ensureSlot(int slot)
    {
        if (slot < 0 || slot >= stacks.Count)
            throw new ArgumentOutOfRangeException(nameof(slot), "Slot fora de alcance");
    }
}