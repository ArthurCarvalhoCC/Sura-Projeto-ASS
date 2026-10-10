using Godot;

public partial class DialogBox : Control
{
    [Export] private Label _DialogName;
    [Export] private RichTextLabel _DialogText;

    [Export(PropertyHint.Range, "1,120,1")]
    private float _charactersPerSecond = 40f;

    private double _elapsedTime;
    private bool _isWriting;

    public bool IsWriting => _isWriting;

    public override void _Ready()
    {
        Hide();
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

    public void ShowDialogue(string speakerName, string dialogue)
    {
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