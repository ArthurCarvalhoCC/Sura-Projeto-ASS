using System.Collections.Generic;

/// Registro central de itens. Para adicionar um item novo, basta registrá-lo aqui.
public static class ItemDatabase
{
    private static readonly Dictionary<string, BaseItem> Items = new()
    {
        { "godot", new MaterialItem("godot", "res://assets/sprites/ui/icons/icon.svg", 8) },
    };

    public static BaseItem get(string id)
        => Items.TryGetValue(id, out BaseItem item) ? item : null;

    public static void register(BaseItem item)
        => Items[item.getId()] = item;
}
