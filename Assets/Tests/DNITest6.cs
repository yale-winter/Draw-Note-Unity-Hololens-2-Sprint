using NUnit.Framework;

public class DNITest6
{
    [Test]
    public void DNITest6SimplePasses()
    {
        Assert.AreEqual(expected: DrawNoteInput.DrawNoteAction.Green, actual: (DrawNoteInput.DrawNoteAction)6); 
    }
}
