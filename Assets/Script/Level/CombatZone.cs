using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatZone : MonoBehaviour
{
    private float totalEnemies;
    private bool inCombat;
    public GameObject cage;

    private void Start()
    {
        inCombat = false;
    }

    private void Update()
    {
        if (inCombat)
        {
            totalEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
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
            ActivateSpawners();
            inCombat = true;
        }
    }

    private void ActivateSpawners()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            GameObject child = gameObject.transform.GetChild(i).gameObject;
            if (child.CompareTag("Spawner"))
            {
                child.GetComponent<SpawnerScript>().SpawnEnemy();
            }
        }
    }
}
