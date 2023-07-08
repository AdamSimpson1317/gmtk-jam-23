using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : Character
{
    public int spotRadius;
    public UnitManager instance;
    public bool turn;
    public bool enemyInRange = false;
    public GameObject enemy;



    public override void Attack(GameObject opponent)
    {
        base.Attack(opponent);
        opponent.GetComponent<EnemyCharacter>().TakeDamage(atk);
    }

    public override void Start()
    {
        base.Start();
        Setup();
        GetUnitManager();
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        
    }

    private void Update()
    {
        if (enemy)
        {
            float enemyX = enemy.transform.position.x - transform.position.x;
            float enemyY = enemy.transform.position.y - transform.position.y;

            //If enemy in range of unit
            if ((enemyX <= atkRange && enemyX >= (atkRange * -1)) && (enemyY <= atkRange && enemyY >= (atkRange * -1)))
            {
                enemyInRange = true;
            }
            else
            {

                enemyInRange = false;
            }

            if ((Time.time > lastAtk + atkRate) && enemyInRange)
            {
                lastAtk = Time.time;
                Attack(enemy);
            }
        }
        else
        {
            Debug.Log("WIN");
        }
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


