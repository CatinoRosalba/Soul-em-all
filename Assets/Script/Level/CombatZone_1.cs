using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatZone_1 : MonoBehaviour
{
    private float totalEnemies;
    private bool clear;
    private bool inCombat;
    public GameObject cage;

    // Start is called before the first frame update
    void Awake()
    {
        clear = false;
        inCombat = false;
    }

    private void Update()
    {
        totalEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (totalEnemies <= 0 && inCombat == true)
        {
            clear = true;
        }

        if(clear == true)
        {
            cage.GetComponent<BoxCollider>().enabled = false;
            cage.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ActivateSpawners();
            inCombat = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && clear == false)
        {
            inCombat = false;
            DestroyEnemies();
        }
    }

    private void ActivateSpawners()
    {
        GameObject[] spawners = GameObject.FindGameObjectsWithTag("Spawner");
        for(int i = 0; i < spawners.Length; i++)
        {
            spawners[i].GetComponent<SpawnerScript>().SpawnEnemy();
        }
    }

    private void DestroyEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemies.Length; i++)
        {
            Destroy(enemies[i]);
        }
    }
}
