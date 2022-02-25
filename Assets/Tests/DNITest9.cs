using NUnit.Framework;

public class DNITest9
{
    [Test]
    public void DNITest1SimplePasses()
    {
        Assert.AreEqual(expected: DrawNoteInput.DrawNoteAction.Mode, actual: (DrawNoteInput.DrawNoteAction)9); 
    }
}
