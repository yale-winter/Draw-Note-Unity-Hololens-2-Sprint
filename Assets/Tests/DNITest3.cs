using NUnit.Framework;

public class DNITest3
{
    [Test]
    public void DNITest3SimplePasses()
    {
        Assert.AreEqual(expected: DrawNoteInput.DrawNoteAction.White, actual: (DrawNoteInput.DrawNoteAction)3); 
    }
}
