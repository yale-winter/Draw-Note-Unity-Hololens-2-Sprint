using NUnit.Framework;

public class DNITest5
{
    [Test]
    public void DNITest5SimplePasses()
    {
        Assert.AreEqual(expected: DrawNoteInput.DrawNoteAction.Yellow, actual: (DrawNoteInput.DrawNoteAction)5); 
    }
}
