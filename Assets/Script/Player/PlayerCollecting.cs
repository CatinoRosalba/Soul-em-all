using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class PlayerCollecting : MonoBehaviour
{
    //Script
    private PlayerShooting playerShooting;                          //Script dello sparo
    [SerializeField] UIManager slot;                                //Script dell'interfaccia

    //Particles gems
    [SerializeField] GameObject effect;                             //Effetto caricato 
    private GameObject effectClone;                                         //Effetto applicato

    //Controlli
    private bool pickupRange;                                               //Controllo se posso prendere
    private bool pickup1;                                                   //Prendo nello slot 1
    private bool pickup2;                                                   //Prendo nello slot 2

    private void Start()
    {
        pickupRange = false;
        pickup1 = false;
        pickup2 = false;
        playerShooting = gameObject.GetComponent<PlayerShooting>();
    }
    
    //Sistema di identificazione e attivazione raccolta
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ammo"))                                                    //Se entra in contatto con delle munizioni
        {
            pickupRange = true;                                                                     //Puoi raccogliere
            effectClone = Instantiate(effect, other.transform.position, Quaternion.identity);       //Particelle
        }
    }

    //Sistema di disattivazione raccolta
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ammo"))                                                    //Se non sono più in contatto con delle munizioni
        {
            pickupRange = false;                                                                    //Non posso raccogliere
            Destroy(effectClone);                                                                   //Stop particelle
        }
    }

    private void Update()
    {
        //Sistema di raccolta nello slot apposito
        if (pickupRange == true && playerShooting.canCollect == true)                               //Se posso raccogliere e sono nel raggio della gemma
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && playerShooting.isEmpty1 == true)                //Se premo il sinistro e non ho munzioni sullo sparo primario
            {
                pickup1 = true;                                                                     //Raccolgo nello slot1
                
            }
            else if (Input.GetKeyDown(KeyCode.Mouse1) && playerShooting.isEmpty2 == true && Input.GetKeyDown(KeyCode.Mouse0) == false)   //Se premo il destro e non ho munizioni sullo sparo secondario e non ho premuto l'altro tasto del mouse
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
                ConvertGemToProjectile(other.gameObject, ref playerShooting.primaryFire, ref playerShooting.primaryAmmo, 2, 5);         //Converte la gemma nella spell giusta
                slot.EquipSlot(other.gameObject, slot.imgEmptySlot1);                               //Aggiunge la gemma allo slot1 dell'interfaccia
                slot.TXTAmmo1.SetText(playerShooting.primaryAmmo.ToString());                       //Aggiunge il numero di munizioni allo slot1 dell'interfaccia
                playerShooting.isEmpty1 = false;                                                    //Ha munizioni
                pickup1 = false;                                                                    //Non può raccogliere nello slot1
                pickupRange = false;                                                                //Non può raccoliere
                Destroy(other.gameObject);                                                          //Distruggi la gemma
                Destroy(effectClone);                                                               //Stop particelle
            }
            if (pickup2 == true)                                                                    //Se posso raccogliere nello slot2
            {
                ConvertGemToProjectile(other.gameObject, ref playerShooting.secondaryFire, ref playerShooting.secondaryAmmo, 2, 5);     //Converte la gemma nella spell giusta
                slot.EquipSlot(other.gameObject, slot.imgEmptySlot2);                               //Aggiunge la gemma allo slot2 dell'interfaccia
                slot.TXTAmmo2.SetText(playerShooting.secondaryAmmo.ToString());                     //Aggiunge il numero di munizioni allo slot2 dell'interfaccia
                playerShooting.isEmpty2 = false;                                                    //Ha munizioni
                pickup2 = false;                                                                    //Non può raccogliere nello slot2
                pickupRange = false;                                                                //Non può raccoliere
                Destroy(other.gameObject);                                                          //Distruggi la gemma
                Destroy(effectClone);                                                               //Stop particelle
            }
        }
    }

    //Converte la gemma nello sparo
    public void ConvertGemToProjectile(GameObject gem, ref GameObject spell, ref float ammo, int min, int max)
    {
        if(gem.name.Contains("Fire"))                                                               //Se la gemma si chiama FireGem
        {
            spell = (GameObject)Resources.Load("Projectiles/Fireball");                             //Equipaggia la Fireball
            ammo = Random.Range(min, max);                                                          //Genera le munizioni in maniera casuale tra un minimo e un massimo
        }
        if(gem.name.Contains("Water"))                                                              //Se la gemma si chiama WaterGem
        {
            spell = (GameObject)Resources.Load("Projectiles/WaterSpray");                                       //Equipaggia il Waterspray
            ammo = Random.Range(min, max);                                                          //Genera le munizioni in maniera casuale tra un minimo e un massimo
        }
    }
}
