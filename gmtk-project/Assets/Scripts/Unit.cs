using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string unitName;
    public int health;
    public int maxHealth;
    public int atk;
    public int movementPoints;
    public int spotRadius;
    public State state = State.Idle;

    private void Start()
    {
        health = maxHealth;
    }

    public void Move()
    {
        //Move unit by movement points
    }

    public void Attack(GameObject opponent)
    {
       //Attack opponent
       //Get enemy script and use TakeDamage() to remove health
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if(health <= 0)
        {
            Die();
        }
    }

    public void Spot()
    {
        //Spot opponent
        //Look at radius perimeter and spot the enemy
        //Access player script to make them visible (while in radius)
    }

    private void Die()
    {
        Debug.Log("Unit died");
    }

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

public enum State
{
    Idle,
    Walk,
    Attack,
    Die
}
