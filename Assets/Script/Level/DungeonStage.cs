using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonStage : MonoBehaviour
{
    private GameObject sfx;
    private AudioSource doorSound;

    private float totalEnemies;
    private bool clear;
    private bool inCombat;
    private bool openedGates;
    public GameObject closingGate;
    public GameObject openingGate;

    private void Start()
    {
        sfx = GameObject.Find("SFX");
        doorSound = sfx.transform.Find("SFX - Door").GetComponent<AudioSource>();

        clear = false;
        inCombat = false;
        openedGates = false;
    }

    private void Update()
    {
        if (inCombat)
        {
            CalculateTotalEnemies();
            if (totalEnemies <= 0)
            {
                clear = true;
            }
            if (clear && openedGates == false)
            {
                openingGate.GetComponent<BoxCollider>().enabled = false;
                openingGate.GetComponent<MeshRenderer>().enabled = false;
                doorSound.Play();
                openedGates = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && clear == false &&  totalEnemies == 0)
        {
            closingGate.GetComponent<BoxCollider>().enabled = true;
            closingGate.GetComponent<MeshRenderer>().enabled = true;
            doorSound.Play();
            inCombat = true;
            ActivateEnemies();
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
