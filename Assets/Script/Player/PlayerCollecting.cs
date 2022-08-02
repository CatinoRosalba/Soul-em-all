using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollecting : MonoBehaviour
{
    //Altri Script
    [SerializeField] PlayerShooting playerShooting;
    [SerializeField] GameObject fireball;

    //Controlli
    bool canPickup;
    bool pickup1;
    bool pickup2;

    private void Start()
    {
        canPickup = false;
        pickup1 = false;
        pickup2 = false;
    }

    //Se senza ammo allora puoi raccogliere
    private void Update()
    {
        if(canPickup == true)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                pickup1 = true;
            }
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                pickup2 = true;
            }
        }
    }

    //Posso raccogliere la gemma se sono vicino (mettere segnale visivo)
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Ammo"))
        {
            canPickup = true;
        }
    }

    //Se sono vicino a delle ammo e premo un pulsante del mouse lo raccolgo (Si distruggono le altre gemme dopo la prima, da fixare)
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Ammo"))
        {
            if (pickup1 == true)
            {
                playerShooting.primaryFire = ConvertGemToProjectile(other.gameObject);
                playerShooting.isEmpty1 = false;
                pickup1 = false;
                Destroy(other.gameObject);
            }
            if (pickup2 == true)
            {
                playerShooting.secondaryFire = ConvertGemToProjectile(other.gameObject);
                playerShooting.isEmpty2 = false;
                pickup2 = false;
                Destroy(other.gameObject);
            }
        }
    }

    //Toglie la possibilità di raccolta della gemma se ti allontani
    private void OnTriggerExit(Collider other)
    {
        canPickup = false;
    }

    //Converte la gemma nello sparo (da finire)
    public GameObject ConvertGemToProjectile(GameObject gem)
    {
        return fireball;
    }
}
