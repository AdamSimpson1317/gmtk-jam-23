using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    public FullGrid grid;
    public EnemyMovement Movement;
    public Transform StartPosition;
    public Transform TargetPosition;
    public Transform CheckPoint1;
    public Transform CheckPoint2;
    public Transform CheckPoint3;
    public Transform CheckPoint4;
    public Transform FinalTarget;

    public List<Transform> CheckPoints = new List<Transform>();

    public List<Node> FinalPath = new List<Node>();
    public void ForcedStart()
    {
        //CheckPoints.Add(CheckPoint1);
        //CheckPoints.Add(CheckPoint2);
        //CheckPoints.Add(CheckPoint3);
        //CheckPoints.Add(CheckPoint4);
        //CheckPoints.Add(FinalTarget);
        
        Waiting();
        FindPath(StartPosition.position, CheckPoint1.position);
        TargetPosition = CheckPoint1;
        GetFinalPath(grid.NodeWorldPosition(StartPosition.position), grid.NodeWorldPosition(FinalTarget.position));
        /*FindPath(CheckPoint1.position, CheckPoint2.position);
        TargetPosition = CheckPoint2;
        GetFinalPath(grid.NodeWorldPosition(CheckPoint1.position), grid.NodeWorldPosition(CheckPoint2.position));
        FindPath(CheckPoint2.position, CheckPoint3.position);
        TargetPosition = CheckPoint3;
        GetFinalPath(grid.NodeWorldPosition(CheckPoint2.position), grid.NodeWorldPosition(CheckPoint3.position));
        FindPath(CheckPoint3.position, CheckPoint4.position);
        TargetPosition = CheckPoint4;
        GetFinalPath(grid.NodeWorldPosition(CheckPoint3.position), grid.NodeWorldPosition(CheckPoint4.position));
        FindPath(CheckPoint4.position, FinalTarget.position);
        TargetPosition = FinalTarget;
        GetFinalPath(grid.NodeWorldPosition(CheckPoint4.position), grid.NodeWorldPosition(FinalTarget.position));*/
    }

    /*public void FindPathToCheckpoint(Transform StartPosition, Transform CheckPoint)
    {
        FindPath(StartPosition.position, CheckPoint1.position);
        TargetPosition = CheckPoint1;
        GetFinalPath(grid.NodeWorldPosition(StartPosition.position), grid.NodeWorldPosition(CheckPoint1.position));
    }*/

    IEnumerator Waiting()
    {
        yield return new WaitForSeconds(3f);
    }

    void FindPath(Vector2 StartPos, Vector2 TargetPos)
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
                //GetFinalPath(StartNode, TargetNode);
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
