using NUnit.Framework;

public class DNITest7
{
    [Test]
    public void DNITest7SimplePasses()
    {
        Assert.AreEqual(expected: DrawNoteInput.DrawNoteAction.Undo, actual: (DrawNoteInput.DrawNoteAction)7); 
    }
}
