using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    //Scripts
    [SerializeField] UIManager slot;
    [SerializeField] PlayerAim aim;

    //Sparo e ammo
    public GameObject bulletSpawnPoint;
    Vector3 aimDir;
    public GameObject primaryFire;
    public float primaryAmmo;
    public GameObject secondaryFire;
    public float secondaryAmmo;

    //Controlli
    public bool isEmpty1;
    public bool isEmpty2;

    private void Start()
    {
        isEmpty1 = true;
        isEmpty2 = true;
    }

    void Update()
    {
        aimDir = (aim.amneryRaycasthit.point - bulletSpawnPoint.transform.position).normalized;               //Direzione di rotazione della mira

        //Fuoco Primario
        if (Input.GetKeyDown(KeyCode.Mouse0) && isEmpty1 == false)
        {
            Fire(primaryFire, ref primaryAmmo);
            slot.TXTAmmo1.SetText(primaryAmmo.ToString());
            if (primaryAmmo == 0)
            {
                slot.EmptySlot(slot.imgEmptySlot1);
                isEmpty1 = true;
            }
        }

        //Fuoco secondario
        if (Input.GetKeyDown(KeyCode.Mouse1) && isEmpty2 == false )
        {
            Fire(secondaryFire, ref secondaryAmmo);
            slot.TXTAmmo2.SetText(secondaryAmmo.ToString());
            if (secondaryAmmo == 0)
            {
                slot.EmptySlot(slot.imgEmptySlot2);
                isEmpty2 = true;
            }
        }
    }

    //Sparo
    private void Fire(GameObject spell, ref float ammo)
    {
        if(spell.name == "Fireball" || spell.name == "WaterSprayDrop")
        {
            Instantiate(spell, bulletSpawnPoint.transform.position, Quaternion.LookRotation(aimDir, Vector3.up));
            ammo--;
        }
    }
}
