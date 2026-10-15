using Godot;

public partial class DialogBox : Control
{
    [Export] private Label _DialogName;
    [Export] private RichTextLabel _DialogText;
    [Export] private TextureRect _DialogPortrait;

    [Export(PropertyHint.Range, "1,120,1")]
    private float _charactersPerSecond = 40f;

    private double _elapsedTime;
    private bool _isWriting;

    public bool IsWriting => _isWriting;

    public override void _Ready()
    {
        //Hide();
    }

    public override void _Process(double delta)
    {
        if (!_isWriting)
            return;

        _elapsedTime += delta;

        int visibleCharacters =
            (int)(_elapsedTime * _charactersPerSecond);

        if (visibleCharacters >= _DialogText.GetTotalCharacterCount())
        {
            FinishWriting();
            return;
        }

        _DialogText.VisibleCharacters = visibleCharacters;
    }

    public void ShowDialogue(
        Texture2D portrait,
        DialogSpeed dialogSpeed,
        string speakerName, 
        string dialogue
    )
    {
        _DialogPortrait.Texture = portrait;
        _DialogPortrait.Visible = portrait != null;

        _charactersPerSecond = (int) dialogSpeed;

        _DialogName.Text = speakerName;
        _DialogText.Text = dialogue;

        _elapsedTime = 0;
        _DialogText.VisibleCharacters = 0;
        _isWriting = true;

        Show();
    }

    public void FinishWriting()
    {
        _DialogText.VisibleCharacters = -1;
        _isWriting = false;
    }

    public void HideDialogue()
    {
        _isWriting = false;
        Hide();
    }
}