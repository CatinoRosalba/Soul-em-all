using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCollecting : MonoBehaviour
{
    //Altri Script
    [SerializeField] PlayerShooting playerShooting;
    [SerializeField] GameObject fireball;
    [SerializeField] GameObject waterspray;
    [SerializeField] Image imgEmptySlot1;
    [SerializeField] Image imgEmptySlot2;

    private Collider anotherOther;

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
            //Se premo prima il tasto dello slot pieno e poi quello vuoto, non cambia immagine
            if (Input.GetKeyDown(KeyCode.Mouse0) && playerShooting.isEmpty1 == true)
            {
                pickup1 = true;
                ChangeImageSlot(anotherOther.gameObject, ref imgEmptySlot1);
            }
            if (Input.GetKeyDown(KeyCode.Mouse1) && playerShooting.isEmpty2 == true)
            {
                pickup2 = true;
                ChangeImageSlot(anotherOther.gameObject, ref imgEmptySlot2);
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
        anotherOther = other;
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

    public void ChangeImageSlot(GameObject gem, ref Image imgEmptySlot)
    {
        if (gem.name == "FireGem" || gem.name == "FireGem(Clone)")
        {
           imgEmptySlot.sprite = Resources.Load<Sprite>("skill_fuoco_attiva");
        }
        if (gem.name == "WaterGem" || gem.name == "WaterGem(Clone)")
        {
            imgEmptySlot.sprite = Resources.Load<Sprite>("skill_acqua_attiva");
        }
        
    }

}
