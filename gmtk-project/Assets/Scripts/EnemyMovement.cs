using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public PathFinding PathFound;

    Node NextNode;
    Node CurrentNode;
    Node PreviousNode;
    int speed = 50;
    int i;
    int j;
    bool moving = false;

    Vector2 targetPosition;
    Vector2 finalPosition;

    public UIManager uiMan;

    private void Start()
    {
        j = 0;
    }
    private void FixedUpdate()
    {
        targetPosition.x = PathFound.TargetPosition.position.x;
        targetPosition.y = PathFound.TargetPosition.position.y;
        finalPosition.x = PathFound.FinalTarget.position.x;
        finalPosition.y = PathFound.FinalTarget.position.y;

        if ((PathFound.FinalPath[0].Position == targetPosition) || (PathFound.FinalPath[1].Position == targetPosition))
        {
            PathFound.FinalPath[0].IsClaimed = false;
            if(targetPosition == finalPosition)
            {
                //Lose game
                uiMan.ToggleLose(true);
            }
        }
        if (moving == false && PathFound.FinalPath != null)
        {
            moving = true;
            StartCoroutine(Move());
        }
        else
        {
            j += 1;
            if(j == 100)
            {
                moving = false; 
                PathFound.ForcedStart();
                j = 0;
            }
        }
    }

    IEnumerator Move()
    {
        CurrentNode = PathFound.FinalPath[0];
        CurrentNode.IsAgent = true;
        Vector2 newTransform = transform.position;
        if(CurrentNode.Position == newTransform)
        {
            if (PathFound.FinalPath[0].IsClaimed == true)
            {
                PathFound.FinalPath[0].IsClaimed = false;
            }
            NextNode = PathFound.FinalPath[1];
            if (NextNode.IsAgent == false && NextNode.IsClaimed == false)
            {
                PathFound.FinalPath[1].IsClaimed = true;
                newTransform = Vector2.MoveTowards(newTransform, NextNode.Position, speed * Time.deltaTime);
                PathFound.StartPosition.position = newTransform;
                PreviousNode = CurrentNode;
                CurrentNode = NextNode;
                i++;
                moving = false;
                PreviousNode.IsAgent = false;
            }
            else if (NextNode.IsAgent == true)
            {
                yield return new WaitForSeconds(50f);
            }

            yield return new WaitForSeconds(50f);
        }
        
    }
}