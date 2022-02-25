using NUnit.Framework;

public class DNITest8
{
    [Test]
    public void DNITest8SimplePasses()
    {
        Assert.AreEqual(expected: DrawNoteInput.DrawNoteAction.Clear, actual: (DrawNoteInput.DrawNoteAction)8); 
    }
}
