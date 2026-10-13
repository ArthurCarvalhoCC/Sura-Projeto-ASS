using Godot;

public class DialogLine
{
    public DialogSpeed Speed;
    public string ContentTranslationKey;
    public Texture2D Texture;

    protected DialogLine(
        DialogSpeed speed,
        string contentTranslationKey,
        Texture2D texture
    )
    {
        this.Speed = speed;
        this.ContentTranslationKey = contentTranslationKey;
        this.Texture = texture;
    }

    public static DialogLine of(
        DialogSpeed speed,
        string contentTranslationKey,
        Texture2D texture
    )
    {
        return new(speed, contentTranslationKey, texture); 
    }

    public static DialogLine withPathToTexture(
        DialogSpeed speed,
        string contentTranslationKey,
        string pathToTexture
    )
    {
        return new(speed, contentTranslationKey, GD.Load<Texture2D>(pathToTexture)); 
    }
    public static DialogLine withImage(
        DialogSpeed speed,
        string contentTranslationKey,
        Image image
    )
    {
        if (image.IsCompressed()) image.Decompress();
        var texture = ImageTexture.CreateFromImage(image);
        return new(speed, contentTranslationKey, texture); 
    }
}