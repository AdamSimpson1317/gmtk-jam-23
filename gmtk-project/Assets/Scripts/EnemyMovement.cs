using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public PathFinding PathFound;
    //public TextManager txt;

    Node NextNode;
    Node CurrentNode;
    Node PreviousNode;
    int speed = 50;
    int i;
    int j;
    bool moving = false;

    Vector2 targetPosition;
    Vector2 finalPosition;

    private void Start()
    {
        j = 0;
        //TargetNode.Position = PathFound.TargetPosition.position;
    }
    private void FixedUpdate()
    {
        /*Debug.Log(PathFound.FinalPath == null);
        Debug.Log(PathFound.FinalPath[0].Position);
        Debug.Log(PathFound.FinalPath[0].IsClaimed);
        Debug.Log(PathFound.FinalPath[1].Position);
        Debug.Log(PathFound.FinalPath[1].IsClaimed);
        Debug.Log(PathFound.TargetPosition.position);
        Debug.Log("/////////////////////////////////////");*/
        targetPosition.x = PathFound.TargetPosition.position.x;
        targetPosition.y = PathFound.TargetPosition.position.y;
        finalPosition.x = PathFound.FinalTarget.position.x;
        finalPosition.y = PathFound.FinalTarget.position.y;

        if ((PathFound.FinalPath[0].Position == targetPosition) || (PathFound.FinalPath[1].Position == targetPosition))
        {
            //txt.AddText();
            PathFound.FinalPath[0].IsClaimed = false;
            if(targetPosition == finalPosition)
            {
                Destroy(gameObject);
            }
            else
            {
                //Change checkpoints
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
                //Adding this line instead of later, where !!!!!!! comment is, improves perfomance, talk about this in dissertation. 
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
        //Debug.Log("Current Position " + CurrentNode.Position);
        if(CurrentNode.Position == newTransform)
        {
            if (PathFound.FinalPath[0].IsClaimed == true)
            {
                PathFound.FinalPath[0].IsClaimed = false;
            }
            NextNode = PathFound.FinalPath[1];
            //Debug.Log(NextNode.IsAgent);
            if (NextNode.IsAgent == false && NextNode.IsClaimed == false)
            {
                PathFound.FinalPath[1].IsClaimed = true;
                //Debug.Log(NextNode.Position);
                newTransform = Vector2.MoveTowards(newTransform, NextNode.Position, speed * Time.deltaTime);
                PathFound.StartPosition.position = newTransform;
                //Debug.Log(PathFound.StartPosition.position);
                PreviousNode = CurrentNode;
                CurrentNode = NextNode;
                /*if(CurrentNode.Position == TargetNode.Position)
                {
                    AgentsPassed.text += 1;
                    Destroy(gameObject);
                }*/
                //Debug.Log(i);
                i++;
                moving = false;
                PreviousNode.IsAgent = false;
            }
            else if (NextNode.IsAgent == true)
            {
                //!!!!!!!! This is very important to talk about in dissertation, Removing next line and similar line at the end of this, improves performance?
                //PathFound.ForcedStart();
                yield return new WaitForSeconds(50f);
            }

            //PathFound.ForcedStart();
            yield return new WaitForSeconds(50f);
        }
        
    }
}
