using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter : Character
{
    public bool visible = false;
    public bool turn = true;
    public GameObject gfx;

    public override void Start()
    {
        base.Start();
        ToggleVisible(false);
    }
    /*private void OnTriggerStay2D(Collider2D collision)
    {
        //Check if unit in proximity
        if (collision.CompareTag("Patroller"))
        {
            collision.GetComponent<Patroler>().TakeDamage(atk);
        }
        else if (collision.CompareTag("Runner"))
        {
            collision.GetComponent<Runner>().TakeDamage(atk);
        }
        else if (collision.CompareTag("Knight"))
        {
            //Check if the unit is within attack range
            if (turn && (Vector2.Distance(gameObject.transform.position, collision.transform.position) <= atkRange))
            {
                collision.GetComponent<Knight>().TakeDamage(atk);
                turn = false;
            }

        }
    }*/

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Unit"))
        {
            //Once enemy leaves area, stop spotting
        }
    }

    public void ToggleVisible(bool toggle)
    {
        gfx.SetActive(toggle);
        visible = toggle;
    }
}
