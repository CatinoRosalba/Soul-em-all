using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{

    //Tipo di spell da usare
    [SerializeField] GameObject Ammo1;
    GameObject Ammo2;

    //Mira
    [SerializeField] GameObject bulletSpawnPoint;
    Vector3 aimDir;
    [SerializeField] private LayerMask aimColliderLayerMask = new LayerMask();
    [SerializeField] private Transform debugTransform;

    private void Start()
    {

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    void Update()
    {

        //Mira
        Vector2 screenPointCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenPointCenter);                          //scannerizza il percorso dalla camera al centro dello schermo (il crosshair) e colpirà un punto della mappa
        
        //Usa il ray precedente per trovare un punto nella mappa a distanza z 5000f (alzare se più lontano) e che ha il layer indicato da aimColliderLayerMask (rimuovere se deve sparare in qualsiasi punto, se non ha il tag definito da questo il raggio non setterà quella posizione per sparare il proiettile)
        if (Physics.Raycast(ray, out RaycastHit raycasthit, 5000f, aimColliderLayerMask))   
        {
            debugTransform.position = raycasthit.point;     //Rende visibile con un elemento il punto in cui è possibile sparare
        }
        aimDir = (raycasthit.point - bulletSpawnPoint.transform.position).normalized;   //Direzione di rotazione della mira

        //Fuoco primario
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Fire(Ammo1);
        }

        //Fuoco secondario
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Fire(Ammo2);
        }

    }

    private void Fire(GameObject ammo)
    {
        Instantiate(ammo, bulletSpawnPoint.transform.position, Quaternion.LookRotation(aimDir, Vector3.up));
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Ammo"))
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Ammo1 = collision.gameObject;
            }
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                Ammo2 = collision.gameObject;
            }
        }

    }

}
