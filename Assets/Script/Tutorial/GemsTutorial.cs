using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemsTutorial : MonoBehaviour
{
    public PlayerShooting slot;
    private GameObject block;

    private void Start()
    {
        block = transform.parent.gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!slot.isEmpty1 && !slot.isEmpty2)
            {
                block.GetComponent<BoxCollider>().enabled = false;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!slot.isEmpty1 && !slot.isEmpty2)
            {
                block.GetComponent<BoxCollider>().enabled = false;
            }
        }
    }
}
