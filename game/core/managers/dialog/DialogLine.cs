using Godot;

public class DialogLine
{
    private DialogSpeed speed;
    private string contentTranslationKey;
    private Texture2D texture;
    private string speakerName;

    protected DialogLine(
        DialogSpeed speed,
        string contentTranslationKey,
        Texture2D texture,
        string speakerName
    )
    {
        this.speed = speed;
        this.contentTranslationKey = contentTranslationKey;
        this.texture = texture;
        this.speakerName = speakerName;
    }

    public static DialogLine of(
        DialogSpeed speed,
        string contentTranslationKey,
        Texture2D texture,
        string speakerName
    )
    {
        return new(speed, contentTranslationKey, texture, speakerName); 
    }

    public static DialogLine withPathToTexture(
        DialogSpeed speed,
        string contentTranslationKey,
        string pathToTexture,
        string speakerName
    )
    {
        return new(speed, contentTranslationKey, GD.Load<Texture2D>(pathToTexture), speakerName); 
    }
    public static DialogLine withImage(
        DialogSpeed speed,
        string contentTranslationKey,
        Image image,
        string speakerName
    )
    {
        if (image.IsCompressed()) image.Decompress();
        var texture = ImageTexture.CreateFromImage(image);
        return new(speed, contentTranslationKey, texture, speakerName); 
    }

    public Texture2D getTexture()
    {
        return texture;
    }
    public string getContentTranslationKey()
    {
        return contentTranslationKey;
    }
    public DialogSpeed getDialogSpeed()
    {
        return speed;
    }
    public int getDialogSpeedAsInteger()
    {
        return (int) speed;
    }
    public string getSpeakerName()
    {
        return speakerName;
    }
}