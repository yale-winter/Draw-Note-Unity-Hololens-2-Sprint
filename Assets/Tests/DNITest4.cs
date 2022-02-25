using NUnit.Framework;

public class DNITest4
{
    [Test]
    public void DNITest4SimplePasses()
    {
        Assert.AreEqual(expected: DrawNoteInput.DrawNoteAction.Red, actual: (DrawNoteInput.DrawNoteAction)4); 
    }
}
