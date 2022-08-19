using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatZone : MonoBehaviour
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
        if (other.gameObject.CompareTag("Player") && clear == false)
        {
            ActivateSpawners();
            inCombat = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inCombat = false;
            DestroyEnemies();
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

    private void DestroyEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemies.Length; i++)
        {
            Destroy(enemies[i]);
        }
    }
}
