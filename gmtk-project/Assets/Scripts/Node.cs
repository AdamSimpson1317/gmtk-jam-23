using UnityEngine;

public class Node 
{
    public int gridX; //X in Node array
    public int gridY; //Y in Node array

    public bool IsWall; //Says if node is obstructed.
    public bool IsAgent; //Says if another Agent is obstructing the node.
    public bool IsClaimed; //Says if another Agent has claimed the node to move onto.
    public int IsFree; //Says if the node is not free (-1), free (0) or a part of a zone (1+).

    public Vector3 Position; //World Position of node.

    public Node Parent; //Previous node.

    public int gCost; //Cost to move.
    public int hCost; //Distance from goal.

    public int fCost { get { return gCost + hCost; } }

    public Node(bool Wall, bool Agent, bool Claim, int Free, Vector3 Pos, int GridX, int GridY)
    {
        IsWall = Wall;
        IsAgent = Agent;
        IsClaimed = Claim;
        IsFree = Free;
        Position = Pos;
        gridX = GridX;
        gridY = GridY;
    }
}
