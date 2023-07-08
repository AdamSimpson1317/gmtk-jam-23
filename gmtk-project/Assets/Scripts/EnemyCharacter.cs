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

    private void Update()
    {
        if (Time.time > lastAtk + atkRate)
        {
            lastAtk = Time.time;
            
        }
    }
    public override void Attack(GameObject opponent)
    {
        base.Attack(opponent);
    
        //Check if unit in proximity
        if (opponent.CompareTag("Patroller"))
        {
            opponent.GetComponent<Patroler>().TakeDamage(atk);
        }
        else if (opponent.CompareTag("Runner"))
        {
            opponent.GetComponent<Runner>().TakeDamage(atk);
        }
        else if (opponent.CompareTag("Knight"))
        {
            //Check if the unit is within attack range
            if (turn && (Vector2.Distance(gameObject.transform.position, opponent.transform.position) <= atkRange))
            {
                opponent.GetComponent<Knight>().TakeDamage(atk);
                turn = false;  
            }

        }
    }

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
