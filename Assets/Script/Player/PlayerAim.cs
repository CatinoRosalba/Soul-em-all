using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    //Mira                                                      
    private Vector2 screenPointCenter;                                                          //Metà schermo
    public Ray ray;                                                                             //Ray per il calcolo della mira
    public Transform amneryBulletRay;                                                           //Oggetti usato per il debug della mira di Amnery
    public Transform jigglyBulletRay;                                                           //Ogetto usato per il debug della mira di Jiggly
    public LayerMask amneryLayerMask;                                                           //Superfici che Amnery può mirare
    public LayerMask jigglyLayerMask;                                                           //Superfici che Jiggly può mirare
    public RaycastHit amneryRaycasthit;                                                         //Punto di mira di Amnery già calcolato
    public RaycastHit jigglyRaycasthit;                                                         //Punto di mira di Jiggly già calcolato
    public bool jigglyRaycasthitFound;                                                          //Jiggly può mirare lì?

    private void Start()
    {
        jigglyRaycasthitFound = false;
    }

    void Update()
    {
        //Mira in base alla cam
        screenPointCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);
        ray = Camera.main.ScreenPointToRay(screenPointCenter);                                  //Il ray viene sparato in mezzo allo schermo della camera

        //Mira di Amnery
        if (Physics.Raycast(ray, out amneryRaycasthit, 5000f, amneryLayerMask))                 //Usa il ray precedente per trovare un punto nella mappa a distanza 5000f (alzare se più lontano) e che ha il layer indicato da amneryLayerMask (rimuovere se deve sparare in qualsiasi punto, se non ha il tag definito da questo il raggio non setterà quella posizione per sparare il proiettile) 
        {
            amneryBulletRay.position = amneryRaycasthit.point;                                  //il bulletRay è per debug
        }

        //Mira di Jiggly
        if(Physics.Raycast(ray, out jigglyRaycasthit, 5000f, jigglyLayerMask))                  //Spara il raycast nella posizione ray e se in range e tra le superfici segnate nella layerMask, segna le coordinate nella variabile raycasthit
        {
            jigglyRaycasthitFound = true;                                                       //Superficie colpibile
            jigglyBulletRay.position = jigglyRaycasthit.point;                                  //Il bulletray per il debug
        }
        else
        {
            jigglyRaycasthitFound = false;                                                      //Superficie non colpibile
        }
    }
}
