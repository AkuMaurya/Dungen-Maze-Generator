using NUnit.Framework;

public class cechkRowIntoMaze
{
    [Test]
    public void CheckRowIntoMaze()
    {
        bool mzeCheck = Maze.WriteRow_IntoMaze();
        Assert.AreEqual(true,mzeCheck);
    }
}
