using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatZone : MonoBehaviour
{
    private float totalEnemies;
    private bool inCombat;
    private bool clear;
    public GameObject cage;

    private void Start()
    {
        clear = false;
        inCombat = false;
    }

    private void Update()
    {
        if (inCombat)
        {
            CalculateTotalEnemies();
            if (totalEnemies <= 0)
            {
                cage.GetComponent<BoxCollider>().enabled = false;
                cage.GetComponent<MeshRenderer>().enabled = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !inCombat)
        {
            ActivateEnemies();
            inCombat = true;
        }
    }

    private void ActivateEnemies()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            GameObject child = gameObject.transform.GetChild(i).gameObject;
            if (child.CompareTag("Spawner"))
            {
                totalEnemies++;
                child.GetComponent<SpawnerScript>().ActivateEnemy();
            }
        }
    }

    private void CalculateTotalEnemies()
    {
        totalEnemies = 0;
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            GameObject child = gameObject.transform.GetChild(i).gameObject;
            if (child.CompareTag("Enemy"))
            {
                totalEnemies++;
            }
        }
    }
}
