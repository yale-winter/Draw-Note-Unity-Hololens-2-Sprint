using NUnit.Framework;

public class DNITest0
{
    [Test]
    public void DNITest0SimplePasses()
    {
        Assert.AreEqual(expected: DrawNoteInput.DrawNoteAction.None, actual: (DrawNoteInput.DrawNoteAction)0); 
    }
}
