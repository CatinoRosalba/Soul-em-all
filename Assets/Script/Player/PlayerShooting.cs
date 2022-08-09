using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerShooting : MonoBehaviour
{
    //Scripts
    public UIManager slot;                                                      //Script dell'interfaccia degli slot
    public PlayerAim aim;                                                       //Script della mira

    //Sparo e ammo
    public GameObject bulletSpawnPoint;                                         //Spawn dei proiettili
    public GameObject primaryFire;                                              //Sparo col tasto sinistro del mouse
    public float primaryAmmo;                                                   //Munizioni per lo sparo primario
    public GameObject secondaryFire;                                            //Sparo col tasto destro del mouse
    public float secondaryAmmo;                                                 //Munizioni per lo sparo secondario
    private Vector3 aimDir;                                                     //Direzione di mira tra il punto da colpire e il punto di spawn

    //Controlli
    public bool isEmpty1;                                                       //Controllo se lo sparo primario non ha munizioni
    public bool isEmpty2;                                                       //Controllo se lo sparo secondario non ha munizioni

    private void Start()
    {
        isEmpty1 = true;
        isEmpty2 = true;
    }

    void Update()
    {
        //Direzione del proiettile
        aimDir = (aim.amneryRaycasthit.point - bulletSpawnPoint.transform.position).normalized;               

        //Fuoco Primario
        if (Input.GetKeyDown(KeyCode.Mouse0) && isEmpty1 == false)              //Se ho munzioni e premo sinistro del mouse
        {
            Fire(primaryFire, ref primaryAmmo, ref slot.TXTAmmo1);              //Sparo
            CheckAmmo(primaryAmmo, ref isEmpty1, ref slot.imgEmptySlot1);       //Controllo Munizioni
        }

        //Fuoco secondario
        if (Input.GetKeyDown(KeyCode.Mouse1) && isEmpty2 == false)              //Se ho munzioni e premo destro del mouse
        {
            Fire(secondaryFire, ref secondaryAmmo, ref slot.TXTAmmo2);          //Sparo
            CheckAmmo(secondaryAmmo, ref isEmpty2, ref slot.imgEmptySlot2);     //Controllo Munizioni
        }
    }

    //Sparo
    private void Fire(GameObject spell, ref float ammo, ref TMP_Text slotAmmo)
    {
        Instantiate(spell, bulletSpawnPoint.transform.position, Quaternion.LookRotation(aimDir, Vector3.up));   //Sparo
        ammo--;                                                                 //-1 munizione
        slotAmmo.SetText(ammo.ToString());                                      //Setto il numero di munizioni nell'interfaccia dello sparo
    }
    
    //Controllo sulle munizioni
    private void CheckAmmo(float ammo, ref bool isEmpty, ref Image slotImage)
    {
        if (ammo == 0)                                                          //Se ho finito le munizioni
        {
            slot.EmptySlot(slotImage);                                          //Tolgo l'immagine della gemma dallo slot dello sparo secondario
            isEmpty = true;                                                     //Setto senza munizioni
        }
    }
}
