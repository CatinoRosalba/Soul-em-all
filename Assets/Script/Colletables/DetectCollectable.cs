using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollectable : MonoBehaviour
{
    public string collectable;
    private GameObject gate;
    private Inventory inventory;

    private void Start()
    {
        gate = transform.parent.gameObject;
        inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (inventory.CheckCollectable(collectable))
            {
                gate.GetComponent<BoxCollider>().enabled = false;
                gate.GetComponent<MeshRenderer>().enabled = false;
            }
        }
    }
}
