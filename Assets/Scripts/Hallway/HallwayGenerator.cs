using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class HallwayGenerator : MonoBehaviour
{
    [SerializeField]
    public HallwayRules[] hallways;
    public Vector2 offset;

    public class Cell
    {
        public bool visited = false;

        public bool[] openDirections = new bool[4]; // 0-Up 1-Down 2-Right 3-Left
    }

    [System.Serializable]
    public class HallwayRules
    {
        public GameObject hallway;
        public Vector2Int minPos;
        public Vector2Int maxPos;

        public bool alwaysSpawn;
        public bool spawnedIn = false;
        public int ProbabilityToSpawn(int x, int y)
        {
            //0- cannot spawn at pos//1- can spawn at pos//2- has to Spawn

            if (x >= minPos.x && x <= maxPos.x && y >= minPos.y && y <= maxPos.y)
            {
                if (alwaysSpawn && !spawnedIn)
                {
                    return 2;
                }
                else if(!alwaysSpawn)
                    return 1;

            }
            return 0;
        }
    }

    public Vector2 size;

    public int startPos = 0;
    
    public List<Cell> board;

    public List<NavMeshSurface> newHalls = new List<NavMeshSurface>();

    // Start is called before the first frame update
    void Start()
    {
        MazeGenerator();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateHalls()
    {

        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                Cell currentCell = board[Mathf.FloorToInt(x + y * size.x)];
               
                   int index = -1;//index that deceides what hallway to spawn in
                   List<int> avaibleHallways = new List<int>();//will hold all types hallways that can spawn at this location
                    
                    for(int h = 0; h < hallways.Length; h++)
                    {
                        int p = hallways[h].ProbabilityToSpawn(x, y);
                        
                        if(p == 2)//hallway needs to spawn at this location
                        {
                            index = h;
                            hallways[index].spawnedIn = true;
                            break;
                        }else if(p == 1)//doesnt have to spawn but can...if you want too
                        {
                            avaibleHallways.Add(h);
                        }
                    }

                    if(index == -1)
                    {
                        if(avaibleHallways.Count > 0)
                        {
                            index = avaibleHallways[Random.Range(0, avaibleHallways.Count)];
                            Debug.Log("Spawning Default" + index);
                        }
                        else
                        {
                            index = 0;
                            
                        }
                    }

               var newHall = Instantiate(hallways[index].hallway, new Vector3(x * offset.x, 0, -y * offset.y), Quaternion.identity, transform).GetComponent<HallManager>();
                
                newHall.UpdateWalls(currentCell.openDirections);

                newHalls.Add(newHall.ShareMyNavmesh());

               newHall.name += ": " + x + "x " + y + "y";
                    
                 
          

            }
        }

        foreach (NavMeshSurface surface in newHalls) surface.BuildNavMesh();

        // Call Leeman's baking script
        //BakeNavMesh.Bake();
    }

    void MazeGenerator()
    {
        board = new List<Cell>();

        for(int x = 0; x < size.x; x++)
        {
            for(int y = 0; y < size.y; y++)
            {
                board.Add(new Cell());
            }
        }

        int currentCell = startPos;

        Stack<int> path = new Stack<int>();

        int loop = 0;//prevents while loop from going indefinite
        while(loop < 1000)//increase depending on size of maze, for us 1000 should be enough
        {
            loop++;

            board[currentCell].visited = true;

            /*if (currentCell == board.Count - 1)//helps with runtime//adds more interesting shape to maze
            {
                break;
            }*/


            //check Neighbors
            List<int> neighbors = CheckNeighbors(currentCell);

            if(neighbors.Count == 0)//no avaible neighbors so check the current path
            {
                if(path.Count == 0)
                {
                    break;//reached last cell in path break from loop
                }
                else
                {
                    currentCell = path.Pop();//go back to previous cell in same path
                }
            }
            else
            {
                path.Push(currentCell);//add current cell to path

                //choose random neighbor
                int newCell = neighbors[Random.Range(0, neighbors.Count)];
                
                //check direction of cell
                if (newCell > currentCell)
                {
                    //going right
                    if(newCell - 1 == currentCell)
                    {
                        board[currentCell].openDirections[2] = true;//since it is going right that means right is open, and so it is assigned
                        currentCell = newCell;
                        board[currentCell].openDirections[3] = true;//since we came from the right the left of the new cell is open
                    }
                    //going down
                    else
                    {
                        board[currentCell].openDirections[1] = true;
                        currentCell = newCell;
                        board[currentCell].openDirections[0] = true;
                    }
                }
                else
                {
                    //going Left
                    if (newCell + 1 == currentCell)
                    {
                        board[currentCell].openDirections[3] = true;
                        currentCell = newCell;
                        board[currentCell].openDirections[2] = true;
                    }
                    //going Up
                    else
                    {
                        board[currentCell].openDirections[0] = true;
                        currentCell = newCell;
                        board[currentCell].openDirections[1] = true;
                    }
                }
            }
        }

        GenerateHalls();
    }

    List<int> CheckNeighbors(int cellPos)
    {
        List<int> neighbors = new List<int>();

        //up neighbor
        if(cellPos - size.x >= 0 && !board[Mathf.FloorToInt(cellPos - size.x)].visited)
        {
            neighbors.Add(Mathf.FloorToInt(cellPos - size.x));
        }
        //down neighbor
        if (cellPos + size.x < board.Count && !board[Mathf.FloorToInt(cellPos + size.x)].visited)
        {
            neighbors.Add(Mathf.FloorToInt(cellPos + size.x));
        }
        //right neighbor
        if ((cellPos+1) % size.x != 0 && !board[Mathf.FloorToInt(cellPos + 1)].visited)
        {
            neighbors.Add(Mathf.FloorToInt(cellPos + 1));
        }
        //left neighbor
        if (cellPos % size.x != 0 && !board[Mathf.FloorToInt(cellPos - 1)].visited)
        {
            neighbors.Add(Mathf.FloorToInt(cellPos - 1));
        }

        return neighbors;
    }

}
