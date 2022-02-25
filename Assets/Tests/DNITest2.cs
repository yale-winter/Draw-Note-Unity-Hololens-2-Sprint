using NUnit.Framework;

public class DNITest2
{
    [Test]
    public void DNITest2SimplePasses()
    {
        Assert.AreEqual(expected: DrawNoteInput.DrawNoteAction.StopDraw, actual: (DrawNoteInput.DrawNoteAction)2); 
    }
}
