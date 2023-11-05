using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// // public class Set
// //     {
// //         public List<Cell> Cells = new List<Cell>();        
// //     }
// //     public class Cell
// //     {
// //         public Set Set;                                     
// //         public bool HasRightWall;                           
// //         public bool HasBottomWall;                          
// //     }
public class test : MonoBehaviour
{
    [System.Serializable]
    public struct Module
    {
        public GameObject prefab;// storing prefab
        public Vector3 rotation; // storing rotation value
    }

    public Module UpperBorder;
    public Module LRBorder;
    public Module BottomWall;
    public Module RightWall;
    public Module Corner;
    public Module Plane;
//     [System.Serializable]
//     public struct GameObject
//     {
//         public GameObject prefab;// storing prefab
//         public Vector3 rotation; // storing rotation value
//     }
//     public GameObject objectToSpawn;
    void Start()
    {
        // test Test = new test();
        GameObject UB = Instantiate(UpperBorder.prefab,new Vector3(0,0,0),Quaternion.identity);
        UB.transform.Rotate(LRBorder.rotation);
        GameObject LR = Instantiate(LRBorder.prefab,new Vector3(20,0,0),Quaternion.identity);
        LR.transform.Rotate(LRBorder.rotation);
        GameObject BW = Instantiate(BottomWall.prefab,new Vector3(40,0,0),Quaternion.identity);
        BW.transform.Rotate(LRBorder.rotation);
        GameObject RW = Instantiate(RightWall.prefab,new Vector3(60,0,0),Quaternion.identity);
        RW.transform.Rotate(LRBorder.rotation);
        GameObject CR = Instantiate(Corner.prefab,new Vector3(80,0,0),Quaternion.identity);
        CR.transform.Rotate(LRBorder.rotation);
        GameObject PL = Instantiate(Plane.prefab,new Vector3(100,0,0),Quaternion.identity);
        PL.transform.Rotate(LRBorder.rotation);
    }

//     void GenerateMaze(int width, int height)
//         {
//             Debug.Log("Fku");
//             GameObject cube = Instantiate(objectToSpawn.prefab,new Vector3(width,0,height),Quaternion.identity);
//         }

//     // Update is called once per frame
//     void Update()
//     {
        
//     }
}
