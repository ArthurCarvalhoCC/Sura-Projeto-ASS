using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using Godot;

public abstract class BaseItem : IBaseItem
{
    private string id;
    private Texture2D texture;

    protected BaseItem() {}

    public string getId() {return this.id;}
    public Texture2D getTexture2D() {return this.texture;}

    public virtual void onUse() { }

    public virtual void onCollect() { }
    public virtual void onDrop() { }
    
    public virtual void onEquip() { }
    public virtual void onUnequip() {}

    protected void changeItemTextureTo(Texture2D tex)
    {
        this.texture = tex;
    }
    protected void changeItemTextureToPathOf(string path)
    {
        changeItemTextureTo(GD.Load<Texture2D>(path));
    }
    protected void changeIdTo(string id)
    {
        this.id = id;
    }
}