using Godot;

public interface IBaseItem
{
    string getId();
    Texture2D getTexture2D();
    void onUse();
    void onCollect();
    void onDrop();
    void onEquip();
    void onUnequip();
}