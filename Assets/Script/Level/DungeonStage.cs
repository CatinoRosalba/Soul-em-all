using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonStage : MonoBehaviour
{
    private float totalEnemies;
    private bool clear;
    private bool inCombat;
    private bool openedGates;
    public GameObject closingGate;
    public GameObject openingGate;

    private void Start()
    {
        clear = false;
        inCombat = false;
        openedGates = false;
    }

    private void Update()
    {
        totalEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (totalEnemies <= 0 && inCombat)
        {
            clear = true;
        }
        if (clear && openedGates == false)
        {
            openingGate.GetComponent<BoxCollider>().enabled = false;
            openingGate.GetComponent<MeshRenderer>().enabled = false;
            openedGates = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && clear == false &&  totalEnemies == 0)
        {
            closingGate.GetComponent<BoxCollider>().enabled = true;
            closingGate.GetComponent<MeshRenderer>().enabled = true;
            inCombat = true;
            ActivateSpawners();
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
