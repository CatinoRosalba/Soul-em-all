using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollectable : MonoBehaviour
{
    //Sound
    private GameObject sfx;
    private AudioSource doorSound;

    public string collectable;
    private GameObject gate;
    private Inventory inventory;

    private void Start()
    {
        sfx = GameObject.Find("SFX");
        doorSound = sfx.transform.Find("SFX - Door").GetComponent<AudioSource>();
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
                doorSound.Play();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (inventory.CheckCollectable(collectable))
            {
                gate.GetComponent<BoxCollider>().enabled = true;
                gate.GetComponent<MeshRenderer>().enabled = true;
                doorSound.Play();
            }
        }
    }
}
