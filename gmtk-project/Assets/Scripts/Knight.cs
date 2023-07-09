using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Unit
{
    public override void Die()
    {
        base.Die();
        Destroy(gameObject);
    }
}
