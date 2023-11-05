using NUnit.Framework;

public class CreatDownWall
{
    [Test]
    public void CheckCreate_DownWalls()
    {
        bool mzeCheck = Maze.Create_DownWalls();
        Assert.AreEqual(true,mzeCheck);
    }
}
