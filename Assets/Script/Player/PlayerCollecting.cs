using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCollecting : MonoBehaviour
{
    //Sound
    private GameObject sfx;
    private AudioSource pickupSound;

    //Script
    private PlayerShooting playerShooting;                          //Script dello sparo
    [SerializeField] UIManager slot;                                //Script dell'interfaccia

    //Controlli
    private bool pickupRange;                                               //Controllo se posso prendere
    private bool pickup1;                                                   //Prendo nello slot 1
    private bool pickup2;                                                   //Prendo nello slot 2
    private GameObject gem;

    private void Start()
    {
        sfx = GameObject.Find("SFX");
        pickupSound = sfx.transform.Find("SFX - Pickup").GetComponent<AudioSource>();

        pickupRange = false;
        pickup1 = false;
        pickup2 = false;
        gem = null;
        playerShooting = gameObject.GetComponent<PlayerShooting>();
    }
    
    //Sistema di identificazione e attivazione raccolta
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ammo"))                                                    //Se entra in contatto con delle munizioni
        {
            pickupRange = true;                                                                     //Puoi raccogliere
            gem = other.gameObject;
        }
    }

    //Sistema di disattivazione raccolta
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ammo"))                                                    //Se non sono più in contatto con delle munizioni
        {
            pickupRange = false;                                                                    //Non posso raccogliere
            gem = null;
        }
    }

    private void Update()
    {
        //Sistema di raccolta nello slot apposito
        if (pickupRange == true)                               //Se posso raccogliere e sono nel raggio della gemma
        {
            if (playerShooting.isEmpty1)
            {
                pickup1 = true;
            } else if (playerShooting.isEmpty2)
            {
                pickup2 = true;
            }

            if (HasGem(playerShooting.equippedGem1, gem))
            {
                playerShooting.primaryAmmo += gem.GetComponent<GemScript>().ammo;
                slot.TXTAmmo1.SetText(playerShooting.primaryAmmo.ToString());
                pickupRange = false;
                pickupSound.Play();
                Destroy(gem);
            }
            else if (HasGem(playerShooting.equippedGem2, gem))
            {
                playerShooting.secondaryAmmo += gem.GetComponent<GemScript>().ammo;
                slot.TXTAmmo2.SetText(playerShooting.secondaryAmmo.ToString());
                pickupRange = false;
                pickupSound.Play();
                Destroy(gem);
            }

            if (Input.GetKeyDown(KeyCode.Q) 
                && PauseController.isGamePaused == false)                                            //Se premo il sinistro e non ho munzioni sullo sparo primario
            {
                pickup1 = true;                                                                     //Raccolgo nello slot1
            }
            else if (Input.GetKeyDown(KeyCode.E) 
                && Input.GetKeyDown(KeyCode.Q) == false 
                && PauseController.isGamePaused == false)   //Se premo il destro e non ho munizioni sullo sparo secondario e non ho premuto l'altro tasto del mouse
            {
                pickup2 = true;                                                                     //Raccolgo nello slot2
            }
        }
    }

    //Sistema di equipaggiamento
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Ammo"))                                                    //Se ancora in contatto con delle munizioni
        {
            if (pickup1 == true)                                                                    //Se posso raccogliere nello slot1
            {
                if (!playerShooting.isEmpty1)
                {
                    dropEquippedGem(playerShooting.equippedGem1, playerShooting.primaryAmmo);
                }
                ConvertGemToProjectile(other.gameObject, ref playerShooting.primaryFire, ref playerShooting.primaryAmmo, ref playerShooting.equippedGem1);         //Converte la gemma nella spell giusta
                pickupSound.Play();
                slot.EquipSlot(other.gameObject, slot.imgEmptySlot1);                               //Aggiunge la gemma allo slot1 dell'interfaccia
                slot.TXTAmmo1.SetText(playerShooting.primaryAmmo.ToString());                       //Aggiunge il numero di munizioni allo slot1 dell'interfaccia
                playerShooting.isEmpty1 = false;                                                    //Ha munizioni
                pickup1 = false;                                                                    //Non può raccogliere nello slot1
                pickupRange = false;                                                                //Non può raccoliere
                Destroy(other.GetComponent<GemScript>().effectClone);
                Destroy(other.gameObject);                                                          //Distruggi la gemma
            }
            if (pickup2 == true)                                                                    //Se posso raccogliere nello slot2
            {
                if (!playerShooting.isEmpty2)
                {
                    dropEquippedGem(playerShooting.equippedGem2, playerShooting.secondaryAmmo);
                }
                ConvertGemToProjectile(other.gameObject, ref playerShooting.secondaryFire, ref playerShooting.secondaryAmmo, ref playerShooting.equippedGem2);     //Converte la gemma nella spell giusta
                pickupSound.Play();
                slot.EquipSlot(other.gameObject, slot.imgEmptySlot2);                               //Aggiunge la gemma allo slot2 dell'interfaccia
                slot.TXTAmmo2.SetText(playerShooting.secondaryAmmo.ToString());                     //Aggiunge il numero di munizioni allo slot2 dell'interfaccia
                playerShooting.isEmpty2 = false;                                                    //Ha munizioni
                pickup2 = false;                                                                    //Non può raccogliere nello slot2
                pickupRange = false;                                                                //Non può raccoliere
                Destroy(other.GetComponent<GemScript>().effectClone);
                Destroy(other.gameObject);                                                          //Distruggi la gemma
            }
        }
    }

    private bool HasGem(GameObject equippedGem, GameObject gem)
    {
        if (equippedGem != null && gem.name.Contains(equippedGem.name))
        {
            return true;
        }
        return false;
    }

    private void dropEquippedGem(GameObject equippedGem, int ammo)
    {
        GameObject clone;
        clone = Instantiate(equippedGem, gameObject.transform.position, Quaternion.identity);
        clone.GetComponent<GemScript>().canDespawn = false;
        clone.GetComponent<GemScript>().ammo = ammo;
    }

    //Converte la gemma nello sparo
    public void ConvertGemToProjectile(GameObject gem, ref GameObject spell, ref int ammo, ref GameObject equippedGem)
    {
        if(gem.name.Contains("Fire"))                                                               //Se la gemma si chiama FireGem
        {
            spell = (GameObject)Resources.Load("Projectiles/Fireball");                             //Equipaggia la Fireball
            equippedGem = (GameObject)Resources.Load("Gems/FireGem");
        }
        if(gem.name.Contains("Water"))                                                              //Se la gemma si chiama WaterGem
        {
            spell = (GameObject)Resources.Load("Projectiles/WaterSpray");                           //Equipaggia il Waterspray
            equippedGem = (GameObject)Resources.Load("Gems/WaterGem");
        }
        ammo = gem.GetComponent<GemScript>().ammo;
    }
}
