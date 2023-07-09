using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullGrid : MonoBehaviour
{
    public LayerMask WallMask;
    public LayerMask AgentMask;
    public Vector2 gridWorldSize;
    public float nodeRadius;
    public float Distance;


    public Node[,] grid;
    public Node[,] zone;
    public Node TLNode;

    int CurrentZone;
    int xLeft;
    bool shrunkL;
    bool shrunkR;

    public List<Node> FinalPath;

    GameObject CurrentAgent;
    public List<GameObject> AgentsList;

    int i = 0;
    bool infinite = true;

    float nodeDiameter;
    int gridSizeX, gridSizeY;

    SetSorter set;

    public void Start()
    {
        set = new SetSorter();
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        //Debug.Log(gridWorldSize.x);
        //Debug.Log(gridSizeX);
        gridSizeY = Mathf.RoundToInt((gridWorldSize.y / nodeDiameter));
        CreateGrid();
        //Decomposition algorithm
        //Decomposition();
        Debug.Log("Passed");
        //Create Agents and puts into a list.
        CreateAgentList();
        //Debug.Log(AgentsList.Count);
        while (AgentsList.Count != i)
        {
            CurrentAgent = AgentsList[0];
            //Debug.Log(CurrentAgent.name);
            CurrentAgent.GetComponent<PathFinding>().ForcedStart();
            //CurrentAgent.Pathing.ForcedStart();                        
            i++;
        }
    }

    List<GameObject> CreateAgentList()
    {
        AgentsList = new List<GameObject>();
        Transform[] AllAgents = GetComponentsInChildren<Transform>();
        AgentsList = new List<GameObject>();
        foreach(Transform Agent in AllAgents)
        {
            AgentsList.Add(Agent.gameObject);
        }
        AgentsList.Remove(AgentsList[0]);
        return AgentsList;
    }

    void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];
        Vector2 newTransform = transform.position;
        Vector2 bottomLeft = newTransform - Vector2.right * gridWorldSize.x / 2 - Vector2.up * gridWorldSize.y / 2;
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = gridSizeY - 1; y > -1; y--)
            {
                Vector2 worldPoint = bottomLeft + Vector2.right * (x * nodeDiameter + nodeRadius) + Vector2.up * (y * nodeDiameter + nodeRadius);
                bool Wall = true;
                int Free = 0;
                bool Agent = false;
                bool Claim = false;

                if (Physics.CheckSphere(worldPoint, nodeRadius, WallMask))
                {
                    Wall = false;
                    Free = -1;
                    
                }

                if (Physics.CheckSphere(worldPoint, nodeRadius, AgentMask))
                {
                    Agent = false;
                }
                if (Free==0)
                {
                    //Debug.Log("Adds");
                    //Debug.Log(x);
                    //Debug.Log(y);
                    set.Add(x, y);
                }

                grid[x, y] = new Node(Wall, Agent, Claim, Free, worldPoint, x, y);
            }
        }
    }

    public void Decomposition()
    {
        Debug.Log("Decompostion");
        //Node[,] zone = new Node[gridSizeX, gridSizeY];
        int x;
        int y;
        List<int> xy = new List<int>();
        //4
        CurrentZone = 1;
        //5
        while (set.CheckEmpty() == false) { 
            /*if (grid[x, y].IsFree != 0)
            {
                continue;
            }*/
            //6
            //Debug.Log(SetSorter.NullCheck());
            //Debug.Log(xy);
            xy = set.GetNextNode();
            //Debug.Log(xy);
            x = xy[0];
            xLeft = x;
            y = xy[1];
            TLNode = grid[x, y];
            //7
            shrunkR = shrunkL = false;
            //8 and 9
            while (infinite == true)
            {
                //Debug.Log(CurrentZone);
                //10
                x = xLeft;
                //Debug.Log(xLeft);
                //11
                //Debug.Log(x);
                //Debug.Log(y);
                grid[x, y].IsFree = CurrentZone;
                set.Remove(x, y);
                //Debug.Log(zone[x, y]);
                //12
                //Debug.Log(grid[x + 1, y]);
                //Debug.Log(grid[x + 1, y - 1]);
                i += 1;
                //Debug.Log(("Run ", i));
                while (grid[x + 1, y].IsFree == 0 && grid[x + 1, y + 1].IsFree != 0)
                {
                    //13
                    x += 1;
                    //14
                    grid[x, y].IsFree = CurrentZone;

                    //if (grid[x,y].IsWall == false)
                    //{
                    set.Remove(x, y);
                    //}
                    //15
                }
                //16 and 17
                if (grid[x + 1, y + 1].IsFree == CurrentZone)
                {
                    //18
                    shrunkR = true;
                }
                //19
                else if (grid[x, y + 1].IsFree != CurrentZone && shrunkR == true)
                {
                    //20 and 21
                    //Debug.Log(x);
                    //Debug.Log(y);
                    while (grid[x, y].IsFree == CurrentZone)
                    {
                        //Debug.Log(x);
                        //Debug.Log(y);
                        //22
                        grid[x, y].IsFree = 0;
                        set.Add(x, y);
                        //23
                        x -= 1;
                        //Debug.Log(grid[x-1,y].IsFree);
                        //24
                    }
                    //25
                    break;
                    //26
                }
                //27 and 28
                /*if (y > 0)
                {
                    (x, y) = (xLeft, y - 1);
                }
                else
                {
                    (x, y) = (xLeft, y);
                }*/
                (x, y) = (xLeft, y - 1);
                //29 and 30
                //Debug.Log(x);
                //Debug.Log(y);
                if (y == 0)
                {
                    break;
                }
                //Debug.Log(CurrentZone);
                //Debug.Log("break");
                //Debug.Log(grid[x, y].IsFree);
                while (grid[x, y].IsFree != 0 && grid[x, y + 1].IsFree == CurrentZone)
                {
                    //31
                    x += 1;
                    //32
                }
                //Debug.Log(x);
                //33 and 34
                if (x > 0)
                {
                    while (grid[x - 1, y].IsFree == 0 && grid[x - 1, y + 1].IsFree != 0)
                    {
                        //35
                        x -= 1;
                        //36
                    }

                    //37 and 38
                    if (grid[x - 1, y + 1].IsFree == CurrentZone)
                    {
                        //39
                        shrunkL = true;
                    }
                    //40
                    else if (grid[x, y + 1].IsFree != CurrentZone && shrunkL == true)
                    {
                        //41
                        break;
                        //42
                    }
                }
            }
            //43 and 44
            //Debug.Log("break - Out of Loop");
            i = 0;
            CurrentZone = CurrentZone + 1;
            
        }
    }

    public Node NodeWorldPosition(Vector2 a_WorldPosition)
    {

        float xpoint = Mathf.Clamp01(((a_WorldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x));
        float ypoint = Mathf.Clamp01(((a_WorldPosition.y + gridWorldSize.y / 2) / gridWorldSize.y));

        int x = Mathf.RoundToInt((gridSizeX - 1) * xpoint);
        int y = Mathf.RoundToInt((gridSizeY - 1) * ypoint);

        return grid[x, y];
    }

    public List<Node> GetNeighbouringNodes(Node a_Node)
    {
        List<Node> NeighbouringNodes = new List<Node>();
        int xCheck;
        int yCheck;

        //Right Side
        xCheck = a_Node.gridX + 1;
        yCheck = a_Node.gridY;
        if(xCheck >= 0 && xCheck < gridSizeX)
        {
            if (yCheck >= 0 && yCheck < gridSizeY)
            {
                NeighbouringNodes.Add(grid[xCheck, yCheck]);
            }
        }

        //Left Side
        xCheck = a_Node.gridX - 1;
        yCheck = a_Node.gridY;
        if (xCheck >= 0 && xCheck < gridSizeX)
        {
            if (yCheck >= 0 && yCheck < gridSizeY)
            {
                NeighbouringNodes.Add(grid[xCheck, yCheck]);
            }
        }

        //Top Side
        xCheck = a_Node.gridX;
        yCheck = a_Node.gridY + 1;
        if (xCheck >= 0 && xCheck < gridSizeX)
        {
            if (yCheck >= 0 && yCheck < gridSizeY)
            {
                NeighbouringNodes.Add(grid[xCheck, yCheck]);
            }
        }

        //Bottom Side
        xCheck = a_Node.gridX;
        yCheck = a_Node.gridY - 1;
        if (xCheck >= 0 && xCheck < gridSizeX)
        {
            if (yCheck >= 0 && yCheck < gridSizeY)
            {
                NeighbouringNodes.Add(grid[xCheck, yCheck]);
            }
        }
        //From here down it becomes Octile, instead of Manhattan.
        //Top Right Side
        /*
        xCheck = a_Node.gridX + 1;
        yCheck = a_Node.gridY + 1;
        if (xCheck >= 0 && xCheck < gridSizeX)
        {
            if (yCheck >= 0 && yCheck < gridSizeY)
            {
                NeighbouringNodes.Add(grid[xCheck, yCheck]);
            }
        }

        //Top Left Side
        xCheck = a_Node.gridX - 1;
        yCheck = a_Node.gridY + 1;
        if (xCheck >= 0 && xCheck < gridSizeX)
        {
            if (yCheck >= 0 && yCheck < gridSizeY)
            {
                NeighbouringNodes.Add(grid[xCheck, yCheck]);
            }
        }

        //Bottom Right Side
        xCheck = a_Node.gridX + 1;
        yCheck = a_Node.gridY - 1;
        if (xCheck >= 0 && xCheck < gridSizeX)
        {
            if (yCheck >= 0 && yCheck < gridSizeY)
            {
                NeighbouringNodes.Add(grid[xCheck, yCheck]);
            }
        }

        //Bottom Left Side
        xCheck = a_Node.gridX - 1;
        yCheck = a_Node.gridY - 1;
        if (xCheck >= 0 && xCheck < gridSizeX)
        {
            if (yCheck >= 0 && yCheck < gridSizeY)
            {
                NeighbouringNodes.Add(grid[xCheck, yCheck]);
            }
        }
        */
        return NeighbouringNodes;
    }
    //Test to see if the line is being drawn properly
    private void OnDrawGizmos()
    {
        Vector2 newTransform = transform.position;
        Gizmos.DrawWireCube(newTransform, new Vector2(gridWorldSize.x, gridWorldSize.y));
        if(grid != null)
        {
            foreach(Node n in grid)
            {
                if (n.IsWall)
                {
                    Gizmos.color = Color.white;
                }
                else
                {
                    Gizmos.color = Color.yellow;
                }

                if (n.IsFree == 0)
                {
                    Gizmos.color = Color.blue;
                }

                if (FinalPath != null)
                {
                    if (FinalPath.Contains(n))
                    {
                        Gizmos.color = Color.red;
                    }
                }
                Gizmos.DrawCube(n.Position, Vector2.one * (nodeDiameter - Distance));
            }
        }

    }

}
