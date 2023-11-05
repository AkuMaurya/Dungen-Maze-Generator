using NUnit.Framework;

public class CehckGenerateMaze
{
    
    [Test]
    public void CheckGenerate_Maze()
    {
        bool mzeCheck = Maze.Generate_Maze();
        Assert.AreEqual(true,mzeCheck);
    }
}
