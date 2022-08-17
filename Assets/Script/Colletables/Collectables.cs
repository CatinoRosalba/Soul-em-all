using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour
{
    Inventory inventory;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>().addCollectable(gameObject.name, gameObject.tag);
        }
    }
}