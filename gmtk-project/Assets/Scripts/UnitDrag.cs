using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;

public class UnitDrag : MonoBehaviour
{
    public int gold = 500;

    public Tilemap tilemap;
    public Vector3Int location;
    public GameObject unitPrefab;
    public int unitIndex = 0;
    public GameObject[] prefabs;
    public int[] unitCosts;
    public UnitManager unitMan;

    //UI
    public TextMeshProUGUI goldText;

    public float lastGold = -1f;
    public float goldRate = 1f;

    public void Dropper()
    {
        Vector3 mp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        location = tilemap.WorldToCell(mp);
        GameObject newUnit = Instantiate(prefabs[unitIndex], location, Quaternion.identity);
        unitMan.enemies.Add(newUnit);

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
            if(unitCosts[unitIndex] <= gold)
            {
                gold -= unitCosts[unitIndex];
                UpdateUI();
                Dropper();
                //Update UI
            }
            
            
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            unitIndex = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            unitIndex = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            unitIndex = 2;
        }
    }

    private void FixedUpdate()
    {
        if(Time.time > lastGold + goldRate)
        {
            lastGold = Time.time;
            AddGold(100);
            UpdateUI();
        }
    }

    public void AddGold(int influx)
    {
        gold += influx;
    }

    public void UpdateUI()
    {
        goldText.text = "Gold: "+ gold.ToString();
    }
}
