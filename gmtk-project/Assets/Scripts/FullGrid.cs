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
        gridSizeY = Mathf.RoundToInt((gridWorldSize.y / nodeDiameter));
        CreateGrid();
        //Create Agents and puts into a list.
        CreateAgentList();
        while (AgentsList.Count != i)
        {
            CurrentAgent = AgentsList[0];
            CurrentAgent.GetComponent<PathFinding>().ForcedStart();                     
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

                if (Physics2D.OverlapCircle(worldPoint, nodeRadius, WallMask))
                {
                    Wall = false;
                    Free = -1;
                    
                }

                if (Physics2D.OverlapCircle(worldPoint, nodeRadius, AgentMask))
                {
                    Agent = false;
                }
                if (Free==0)
                {
                    set.Add(x, y);
                }

                grid[x, y] = new Node(Wall, Agent, Claim, Free, worldPoint, x, y);
            }
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
