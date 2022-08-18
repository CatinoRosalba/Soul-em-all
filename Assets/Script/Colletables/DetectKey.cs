using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectKey : MonoBehaviour
{
    public string key;
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
            Debug.Log("a");
            if (inventory.CheckKeys(key))
            {
                Debug.Log("b");
                gate.GetComponent<BoxCollider>().enabled = false;
                gate.GetComponent<MeshRenderer>().enabled = false;
            }
        }
    }
}
