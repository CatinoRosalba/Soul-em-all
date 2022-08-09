using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCollecting : MonoBehaviour
{
    //Altri Script
    [SerializeField] PlayerShooting playerShooting;
    [SerializeField] UIManager slot;

    //Spells
    [SerializeField] GameObject fireball;
    [SerializeField] GameObject waterspray;
    Collider anotherOther;

    //Particles gems
    [SerializeField] GameObject effect;
    GameObject effectClone;

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

 
    private void Update()
    {
        //Se senza ammo allora puoi raccogliere
        if (canPickup == true)
        {
            //Se premo prima il tasto dello slot pieno e poi quello vuoto, non cambia immagine (da controllare)
            if (Input.GetKeyDown(KeyCode.Mouse0) && playerShooting.isEmpty1 == true)
            {
                pickup1 = true;
                slot.EquipSlot(anotherOther.gameObject, slot.imgEmptySlot1);
            }
            if (Input.GetKeyDown(KeyCode.Mouse1) && playerShooting.isEmpty2 == true)
            {
                pickup2 = true;
                slot.EquipSlot(anotherOther.gameObject, slot.imgEmptySlot2);
            }
        }
    }

    //Posso raccogliere la gemma se sono vicino (mettere segnale visivo)
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Ammo"))
        {
            canPickup = true;
            anotherOther = other;
            effectClone = Instantiate(effect, other.transform.position, Quaternion.identity);
        }
    }

    //Se sono vicino a delle ammo e premo un pulsante del mouse lo raccolgo (Si distruggono le altre gemme dopo la prima, da fixare)
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Ammo"))
        {
            if (pickup1 == true)
            {
                ConvertGemToProjectile(other.gameObject, ref playerShooting.primaryFire, ref playerShooting.primaryAmmo, 2, 5);
                slot.TXTAmmo1.SetText(playerShooting.primaryAmmo.ToString());
                playerShooting.isEmpty1 = false;
                pickup1 = false;
                canPickup = false;
                Destroy(other.gameObject);
                Destroy(effectClone);
            }
            if (pickup2 == true)
            {
                ConvertGemToProjectile(other.gameObject, ref playerShooting.secondaryFire, ref playerShooting.secondaryAmmo, 2, 5);
                slot.TXTAmmo2.SetText(playerShooting.secondaryAmmo.ToString());
                playerShooting.isEmpty2 = false;
                pickup2 = false;
                canPickup = false;
                Destroy(other.gameObject);
                Destroy(effectClone);
            }
        }
    }

    //Toglie la possibilitÓ di raccolta della gemma se ti allontani
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ammo"))
        {
            canPickup = false;
            Destroy(effectClone);
        }
    }

    //Converte la gemma nello sparo (da finire)
    public void ConvertGemToProjectile(GameObject gem, ref GameObject spell, ref float ammo, int min, int max)
    {
        if(gem.name == "FireGem" || gem.name == "FireGem(Clone)")
        {
            spell = fireball;
            ammo = Random.Range(min, max);
        }
        if(gem.name == "WaterGem" || gem.name == "WaterGem(Clone)")
        {
            spell = waterspray;
            ammo = Random.Range(min, max);
        }
    }
}
