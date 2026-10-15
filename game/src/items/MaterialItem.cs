using Godot;


/// Item simples e empilhável, definido apenas por id, textura e tamanho de stack.
public class MaterialItem : StackableItem
{
    public MaterialItem(string id, string texturePath, int maxStackSize = 64)
    {
        changeIdTo(id);
        changeItemTextureToPathOf(texturePath);
        changeMaxStackSize(maxStackSize);
    }

    public override void onUse()
    {
        GD.Print("usou", getId());
    }
}
