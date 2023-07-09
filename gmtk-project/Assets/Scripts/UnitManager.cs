using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    //public static UnitManager instance;

    public int spottedCount = 0;
    public EnemyCharacter enemy;
    public List<GameObject> enemies = new List<GameObject>();

    public void Spotted()
    {

        if (spottedCount > 0)
        {
            enemy.ToggleVisible(true);
        }
        else
        {

            enemy.ToggleVisible(false);
        }
    }

    public void EditSpotted(bool add)
    {
        Debug.Log("test");
        if (add)
        {
            spottedCount++;
        }
        else
        {
            spottedCount--;
        }
    }

    private void Update()
    {
        if (enemy)
        {
            Spotted();
        }
    }

}
