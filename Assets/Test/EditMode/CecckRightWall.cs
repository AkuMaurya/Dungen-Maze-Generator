using NUnit.Framework;

public class CecckRightWall
{
    [Test]
     public void CheckCreate_RightWalls()
    {
        bool mzeCheck = Maze.Create_RightWalls();
        Assert.AreEqual(true,mzeCheck);
    }
}
