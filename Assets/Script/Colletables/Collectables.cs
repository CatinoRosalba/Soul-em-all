using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour
{
    private GameObject sfx;
    private AudioSource pickupSound;
    public string id;
    private Inventory inventory;

    private void Start()
    {
        sfx = GameObject.Find("SFX");
        pickupSound = sfx.transform.Find("SFX - Pickup").GetComponent<AudioSource>();
        inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inventory.GetComponent<Inventory>().addCollectable(id, gameObject.tag);
            pickupSound.Play();
            Destroy(gameObject);
        }
    }
}