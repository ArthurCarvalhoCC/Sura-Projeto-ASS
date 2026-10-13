using System.Collections.Generic;

public class Dialog
{
    public readonly List<DialogLine> dialogLines;
    public int index {get; private set;}

    public Dialog(List<DialogLine> lines)
    {
        this.dialogLines = lines;
    }


    public static Dialog of(params DialogLine[] lines)
    {
        return new Dialog(new List<DialogLine>(lines));
    }

    public bool isFinished()
    {
        return index >= dialogLines.Count;
    }

    public DialogLine getCurrentDialogLine()
    {
        if (index < dialogLines.Count)
            return dialogLines[index];
        else 
            return null;
    }

    public bool next()
    {
        index++;
        return !isFinished();
    }
}