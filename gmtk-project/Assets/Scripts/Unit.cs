using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : Character
{
    public int spotRadius;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Check if enemy is in the spot radius
        if (collision.CompareTag("Enemy"))
        {
            //Once enemy spotted, make sure this is logged in a gamemanager
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            //Once enemy leaves area, stop spotting
        }
    }
}


