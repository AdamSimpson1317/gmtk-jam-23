using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patroler : Unit
{

    public float speed = 0.8f;
    public float range = 3f;

    float startingY;
    int dir = 1;

    public void Patrol()
    {
        //Patrol area
    }

    public override void Start()
    {
        base.Setup();
        GetUnitManager();
        startingY = transform.position.y;
        enemy = GameObject.FindGameObjectWithTag("Enemy");

    }

    void FixedUpdate()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime * dir);
        if(transform.position.y < startingY || transform.position.y > startingY + range)
        {
            dir *= -1;
        }
    }

    public override void Die()
    {
        base.Die();
        Destroy(gameObject);
    }


}
