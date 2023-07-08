using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public bool[] spotted;

    public bool Spotted()
    {
        for (int i = 0; i < spotted.Length; i++)
        {
            if(spotted[i] == true)
            {
                return true;
            }
        }
        return false;
    }

}
