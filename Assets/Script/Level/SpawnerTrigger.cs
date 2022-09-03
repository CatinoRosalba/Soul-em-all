using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerTrigger : MonoBehaviour
{
    private bool hasEntered;

    private void Start()
    {
        hasEntered = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !hasEntered)
        {
            GameObject parent = transform.parent.gameObject;
            for (int i = 0; i < parent.transform.childCount; i++)
            {
                GameObject child = parent.transform.GetChild(i).gameObject;
                if (child.CompareTag("Spawner"))
                {
                    child.GetComponent<SpawnerScript>().SpawnEnemy();
                }
            }
            hasEntered = true;
        }
    }
}
