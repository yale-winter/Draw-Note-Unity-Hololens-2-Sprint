using NUnit.Framework;

public class DNITest1
{
    [Test]
    public void DNITest1SimplePasses()
    {
        Assert.AreEqual(expected: DrawNoteInput.DrawNoteAction.Draw, actual: (DrawNoteInput.DrawNoteAction)1); 
    }
}
