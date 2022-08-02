using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    //Sparo e ammo
    public GameObject primaryFire;
    public GameObject primaryAmmo;
    public GameObject secondaryFire;
    public GameObject secondaryAmmo;

    //Mira
    [SerializeField] GameObject bulletSpawnPoint;
    [SerializeField] private LayerMask aimColliderLayerMask = new LayerMask();
    [SerializeField] private Transform BulletRay;
    Vector3 aimDir;

    //Controlli
    public bool isEmpty1;
    public bool isEmpty2;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isEmpty1 = true;
        isEmpty2 = true;
    }

    void Update()
    {
        //Mira
        Vector2 screenPointCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenPointCenter);                                  //scannerizza il percorso dalla camera al centro dello schermo (il crosshair) e colpirà un punto della mappa
        if (Physics.Raycast(ray, out RaycastHit raycasthit, 5000f, aimColliderLayerMask))           //Usa il ray precedente per trovare un punto nella mappa a distanza z 5000f (alzare se più lontano) e che ha il layer indicato da aimColliderLayerMask (rimuovere se deve sparare in qualsiasi punto, se non ha il tag definito da questo il raggio non setterà quella posizione per sparare il proiettile) 
        {
            BulletRay.position = raycasthit.point;                                                  //Rende visibile con un elemento il punto in cui è possibile sparare
        }
        aimDir = (raycasthit.point - bulletSpawnPoint.transform.position).normalized;               //Direzione di rotazione della mira

        //Fuoco Primario
        if (Input.GetKeyDown(KeyCode.Mouse0) && isEmpty1 == false)
        {
            Fire(primaryFire);
        }

        //Fuoco secondario
        if (Input.GetKeyDown(KeyCode.Mouse1) && isEmpty2 == false)
        {
            Fire(secondaryFire);
        }
    }

    //Sparo
    private void Fire(GameObject ammo)
    {
        Instantiate(ammo, bulletSpawnPoint.transform.position, Quaternion.LookRotation(aimDir, Vector3.up));
    }
}
