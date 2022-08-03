using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollecting : MonoBehaviour
{
    //Altri Script
    [SerializeField] PlayerShooting playerShooting;
    [SerializeField] GameObject fireball;
    [SerializeField] GameObject waterspray;

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
            if (Input.GetKeyDown(KeyCode.Mouse0) && playerShooting.isEmpty1 == true)
            {
                pickup1 = true;
                //cambia hud
            }
            if (Input.GetKeyDown(KeyCode.Mouse1) && playerShooting.isEmpty2 == true)
            {
                pickup2 = true;
                //cambia hud
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
                ConvertGemToProjectile(other.gameObject, ref playerShooting.primaryFire);
                playerShooting.isEmpty1 = false;
                pickup1 = false;
                canPickup = false;
                Destroy(other.gameObject);
            }
            if (pickup2 == true)
            {
                ConvertGemToProjectile(other.gameObject, ref playerShooting.secondaryFire);
                playerShooting.isEmpty2 = false;
                pickup2 = false;
                canPickup = false;
                Destroy(other.gameObject);
            }
        }
    }

    //Toglie la possibilità di raccolta della gemma se ti allontani
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ammo"))
        {
            canPickup = false;
        }
    }

    //Converte la gemma nello sparo (da finire)
    public void ConvertGemToProjectile(GameObject gem, ref GameObject spell)
    {
        if(gem.name == "FireGem" || gem.name == "FireGem(Clone)")
        {
            spell = fireball;
        }
        if(gem.name == "WaterGem" || gem.name == "WaterGem(Clone)")
        {
            spell = waterspray;
        }
    }
}
