using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : Character
{
    public int spotRadius;
    public UnitManager instance;
    public bool turn;

    public override void Attack(GameObject opponent)
    {
        base.Attack(opponent);
    }

    public override void Start()
    {
        base.Start();
        Setup();
        GetUnitManager();
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Check if enemy is in the spot radius
        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("Enemy Inbound");
            //Once enemy spotted, make sure this is logged in a gamemanager
            instance.EditSpotted(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            //Once enemy leaves area, stop spotting
            instance.EditSpotted(false);

        }
    }

    public void GetUnitManager()
    {
        instance = GameObject.FindGameObjectWithTag("UnitManager").GetComponent<UnitManager>();
    }
}


