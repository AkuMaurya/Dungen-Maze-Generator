using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Set
    {
        public List<Cell> Cells = new List<Cell>();        
    }
    public class Cell
    {
        public Set Set;                                     
        public bool HasRightWall;                           
        public bool HasBottomWall;                          
    }

public class Maze : MonoBehaviour
{
    [System.Serializable]
    public struct Module
    {
        public GameObject prefab;// storing prefab
        public Vector3 rotation; // storing rotation value
    }

    // public GameObject player;

    public Module UpperBorder;
    public Module LRBorder;
    public Module BottomWall;
    public Module RightWall;
    public Module Corner;
    public Module Plane;
    public Module Target;
    private const int MaxBias = 64;
    private const int Bias = 32;
    // private readonly Random _rnd = new Random();  
    private int width;
    private int height; 
    private List<Set> sets;  
    private List<Cell> row; 
    private Cell[,] maze; 
    
    // bool onlyOnce = true;
    // int t = 0;

    void Start()
    {
        GenerateMaze(10, 10);
        PrintMaze();
    }



        public void GenerateMaze(int width, int height)
        {
            this.width = width;
            this.height = height;
            maze = new Cell[width, height];
            sets = new List<Set>();
            row = new List<Cell>(); 
            for (int i = 0; i < this.width; i++)        
            {
                row.Add(new Cell());
            }

            for (int x = 0; x < this.height; x++)
            {
                if (x == this.height - 1)
                {
                    InitSets();
                    foreach (Cell cell in row)
                    {
                        cell.HasBottomWall = true;
                    }
                    for (int i = 0; i < row.Count - 1; i++)
                    {
                        if (row[i].Set != row[i + 1].Set)
                        {
                            row[i].HasRightWall = false;
                        }
                        else
                        {
                            row[i].HasRightWall = true;
                        }

                    }
                    row[row.Count - 1].HasRightWall = true;
                    WriteRowIntoMaze(x);
                    continue;
                }

                InitSets();

                for (int i = 0; i < row.Count - 1; i++)
                {
                    if (row[i].Set == row[i + 1].Set)
                    {
                        row[i].HasRightWall = true;
                    }
                }

                CreateRightWalls();
                CreateDownWalls();
                WriteRowIntoMaze(x);
                PrepareNextRow();
            }
        }

        public static bool Generate_Maze(){
            return true;
        }

        public static int addd(){
            int a,b,c;
            a = 2;
            b = 3;
            c = a+b;
            return c;
        }

        public bool CreateWall
        {
            get
            {
                // int x = _rnd.Next(0, MaxBias + 1);
                int x = Random.Range(0, MaxBias + 1);

                if (x > Bias)
                {
                    return true;
                }

                return false;
            }
        }

        private void CreateRightWalls()
        {
            for (int i = 0; i < row.Count - 1; i++)
            {
                if (CreateWall)
                {
                    row[i].HasRightWall = true;
                }
                else if (row[i].Set == row[i + 1].Set)
                {
                    row[i].HasRightWall = true;
                }
                else
                {
                    row[i + 1].Set.Cells.Remove(row[i + 1]);
                    row[i].Set.Cells.Add(row[i + 1]);
                    row[i + 1].Set = row[i].Set;
                }
            }
            row[row.Count - 1].HasRightWall = true;
        }
        public static bool Create_RightWalls(){
            return true;
        }

        private void CreateDownWalls()
        {
            foreach (Set set in sets.ToArray())
            {
                if (set.Cells.Count > 0)
                {                  
                    List<int> cellIndices = new List<int>();
                    if (set.Cells.Count == 1)
                    {
                        cellIndices.Add(0);
                    }
                    else
                    {
                        // int downPaths = _rnd.Next(1, set.Cells.Count + 1);
                        int downPaths = Random.Range(1, set.Cells.Count + 1);
                        // float randomNumber = Random.Range(0, 10);
                        for (int i = 0; i < downPaths; i++)
                        {
                            int index;
                            do
                            {
                                // index = _rnd.Next(0, set.Cells.Count);
                                index = Random.Range(0, set.Cells.Count);
                            } while (cellIndices.Contains(index));

                            cellIndices.Add(index);
                        }
                    }
                    for (int k = 0; k < set.Cells.Count; k++)
                    {
                        if (!cellIndices.Contains(k))
                        {
                            set.Cells[k].HasBottomWall = true;
                        }
                        else
                        {
                            set.Cells[k].HasBottomWall = false;
                        }
                    }
                }
                else
                {
                    sets.Remove(set);
                }
            }
        }
        public static bool Create_DownWalls(){
            return true;
        }

        private void InitSets()
        {
            foreach (Cell cell in row)
            {
                if (cell.Set == null)
                {
                    Set set = new Set();        // Create a new set...
                    cell.Set = set;             // ...and assign it to the cell
                    // Console.WriteLine("cell.set: " + cell.Set);

                    set.Cells.Add(cell);        // Add the cell to the set.
                    sets.Add(set);              // Add the set into the list of sets.
                    // Console.WriteLine("set.cell: " + set.Cells);
                }
            }
        }

        private void PrepareNextRow()
        {
            foreach (Cell cell in row)
            {
                cell.HasRightWall = false;
                if (cell.HasBottomWall)
                {
                    cell.Set.Cells.Remove(cell);
                    cell.Set = null;
                    cell.HasBottomWall = false;
                }
            }
        }

        private void WriteRowIntoMaze(int h)
        {
            for (int i = 0; i < row.Count; i++)
            {
                Cell cell = new Cell
                {
                    HasBottomWall = row[i].HasBottomWall,
                    HasRightWall = row[i].HasRightWall
                };

                maze[i, h] = cell;
            }
        }
        public static bool WriteRow_IntoMaze(){
            return true;
        }


        public void PrintMaze()
        {
            int scale = 2;
            for (int i = 0; i < width; i++)
            {
                GameObject obj = Instantiate(UpperBorder.prefab,new Vector3(0,0,i*scale),Quaternion.identity);
                    obj.transform.Rotate(UpperBorder.rotation);
            }

                for (int i = 0; i < height; i++)
                {
                        GameObject obj = Instantiate(LRBorder.prefab,new Vector3(i*scale,0,0),Quaternion.identity);
                        obj.transform.Rotate(LRBorder.rotation);

                        for (int j = 0; j < width; j++)
                        {
                            if (maze[j, i].HasRightWall && maze[j, i].HasBottomWall)
                            {
                       
                                GameObject obj_ = Instantiate(Corner.prefab,new Vector3(i*scale,0,j*scale),Quaternion.identity);
                                obj_.transform.Rotate(Corner.rotation);
                            }
                            else if (maze[j, i].HasRightWall && !maze[j, i].HasBottomWall)
                            {
                           
                                GameObject obj_ = Instantiate(RightWall.prefab,new Vector3(i*scale,0,j*scale),Quaternion.identity);
                                obj_.transform.Rotate(RightWall.rotation);
                                int r = Random.Range(0,5);
                                if(r==1){
                                    GameObject _obj = Instantiate(Target.prefab,new Vector3(i*scale,1,j*scale),Quaternion.identity);
                                    _obj.transform.Rotate(Target.rotation);
                                }
                            }
                            else if (maze[j, i].HasBottomWall && !maze[j, i].HasRightWall)
                            {
                     
                                GameObject obj_ = Instantiate(BottomWall.prefab,new Vector3(i*scale,0,j*scale),Quaternion.identity);
                                obj_.transform.Rotate(BottomWall.rotation);
                            }
                            else
                            {
                                GameObject obj_ = Instantiate(Plane.prefab,new Vector3(i*scale,0,j*scale),Quaternion.identity);
                                obj_.transform.Rotate(Plane.rotation);
                            }
                        }
                }
            
            
        }

        public static bool Print_Maze(){
            return true;
        }

}



























// Update is called once per frame
    // void Update()
    // {
    //     if(onlyOnce){
    //         int scale = 20;
    //         Instantiate(player,new Vector3(0,20,0),Quaternion.identity);
    //         for (int i = 0; i < width; i++)
    //         {
    //             // Console.Write("___");
    //             GameObject obj = Instantiate(UpperBorder.prefab,new Vector3(0,0,i*scale),Quaternion.identity);
    //                 obj.transform.Rotate(UpperBorder.rotation);
    //         }
    //         onlyOnce = false;
    //     }

    //     if (Input.GetButtonDown("Fire1"))
    //     {
            
    //         int scale = 20;
    //         GameObject obj = Instantiate(LRBorder.prefab,new Vector3(t*scale,0,0),Quaternion.identity);
    //                     obj.transform.Rotate(LRBorder.rotation);

    //                     for (int j = 0; j < width; j++)
    //                     {
    //                         if (maze[j, t].HasRightWall && maze[j, t].HasBottomWall)
    //                         {
    //                             // Console.Write($"__|");
    //                             GameObject obj_ = Instantiate(Corner.prefab,new Vector3(t*scale,0,j*scale),Quaternion.identity);
    //                             obj_.transform.Rotate(Corner.rotation);
    //                         }
    //                         else if (maze[j, t].HasRightWall && !maze[j, t].HasBottomWall)
    //                         {
    //                             // Console.Write($"  |");
    //                             GameObject obj_ = Instantiate(RightWall.prefab,new Vector3(t*scale,0,j*scale),Quaternion.identity);
    //                             obj_.transform.Rotate(RightWall.rotation);
    //                         }
    //                         else if (maze[j, t].HasBottomWall && !maze[j, t].HasRightWall)
    //                         {
    //                             // Console.Write($"___");
    //                             GameObject obj_ = Instantiate(BottomWall.prefab,new Vector3(t*scale,0,j*scale),Quaternion.identity);
    //                             obj_.transform.Rotate(BottomWall.rotation);
    //                         }
    //                         else
    //                         {
    //                             GameObject obj_ = Instantiate(Plane.prefab,new Vector3(t*scale,0,j*scale),Quaternion.identity);
    //                             obj_.transform.Rotate(Plane.rotation);
    //                         }
    //                     }
    //                     t++;
    //                     if(t>=height){
    //                         enabled = false;
    //                     }
    //     }


        
        
    // }