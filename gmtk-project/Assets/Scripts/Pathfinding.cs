using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    public Grid grid;
    public AgentMovement Movement;
    public Transform StartPosition;
    public Transform TargetPosition;

    public List<Node> FinalPath = new List<Node>();
    public void ForcedStart()
    {
        Waiting();
        FindPath(StartPosition.position, TargetPosition.position);
        
    }

    IEnumerator Waiting()
    {
        yield return new WaitForSeconds(3f);
    }

    void FindPath(Vector3 StartPos, Vector3 TargetPos)
    {
        Node StartNode = grid.NodeWorldPosition(StartPos);
        StartNode.IsAgent = true;
        Node TargetNode = grid.NodeWorldPosition(TargetPos);

        List<Node> OpenList = new List<Node>();
        HashSet<Node> ClosedList = new HashSet<Node>();

        OpenList.Add(StartNode);

        while(OpenList.Count > 0)
        {
            Node CurrentNode = OpenList[0];
            for(int i = 1; i < OpenList.Count; i++)
            {
                if(OpenList[i].fCost < CurrentNode.fCost || OpenList[i].fCost == CurrentNode.fCost && OpenList[i].hCost < CurrentNode.hCost)
                {
                    CurrentNode = OpenList[i];
                }
            }
            OpenList.Remove(CurrentNode);
            ClosedList.Add(CurrentNode);
            if(CurrentNode == TargetNode)
            {
                GetFinalPath(StartNode, TargetNode);
            }

            foreach (Node NeighbourNode in grid.GetNeighbouringNodes(CurrentNode))
            {
                if (!NeighbourNode.IsWall || ClosedList.Contains(NeighbourNode))
                {
                    continue;
                }
                int MoveCost = CurrentNode.gCost + GetManhattanDistance(CurrentNode, NeighbourNode);

                if(MoveCost < NeighbourNode.gCost || !OpenList.Contains(NeighbourNode))
                {
                    NeighbourNode.gCost = MoveCost;
                    NeighbourNode.hCost = GetManhattanDistance(NeighbourNode, TargetNode);
                    NeighbourNode.Parent = CurrentNode;

                    if (!OpenList.Contains(NeighbourNode))
                    {
                        OpenList.Add(NeighbourNode);
                    }
                }
            }
        }
    }

    void GetFinalPath(Node StartingNode, Node EndNode)
    {
        Node CurrentNode = EndNode;

        while(CurrentNode != StartingNode)
        {
            FinalPath.Add(CurrentNode);
            CurrentNode.IsAgent = false;
            CurrentNode = CurrentNode.Parent;
        }

        FinalPath.Add(CurrentNode);
        FinalPath.Reverse();

        grid.FinalPath = FinalPath;
    }

    int GetManhattanDistance(Node NodeA, Node NodeB)
    {
        int ix = Mathf.Abs(NodeA.gridX - NodeB.gridX);
        int iy = Mathf.Abs(NodeA.gridY - NodeB.gridY);

        return ix + iy;
    }

}
