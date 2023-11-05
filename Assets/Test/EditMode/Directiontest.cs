using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class NewTestScript
{
    // A Test behaves as an ordinary method
    [Test]
    public void CheckMaze()
    {
        bool mzeCheck = Maze.Print_Maze();
        Assert.AreEqual(true,mzeCheck);
    }

    
    

   

    
}
