using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectKey : MonoBehaviour
{
    public string key;
    public GameObject gate;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.gameObject.GetComponent<Inventory>().CheckKeys(key))
            {
                gate.GetComponent<BoxCollider>().enabled = false;
            }
        }
    }
}
