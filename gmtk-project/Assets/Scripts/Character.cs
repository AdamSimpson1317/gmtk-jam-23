using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public string characterName;
    public int health;
    public int maxHealth;
    public int atk;
    public int movementPoints;
    public int atkRange;
    public State state = State.Idle;

    public virtual void Start() { }

    public void Setup()
    {
        health = maxHealth;
    }

    public void Move()
    {
        //Move unit by movement points
    }

    public virtual void Attack(GameObject opponent)
    {
        //Attack opponent
        //Get enemy script and use TakeDamage() to remove health
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    public virtual void Die() { }
       
}

public enum State
{
    Idle,
    Walk,
    Attack,
    Die
}
