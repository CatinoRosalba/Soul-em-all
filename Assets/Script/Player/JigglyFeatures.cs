using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JigglyFeatures : MonoBehaviour
{
    //Scripts
    public PlayerAim aim;                                                                           //Usato per la verifica col raycast se possibile rampinare/usare Jiggly
    public GameObject hookSpawnPoint;                                                               //Punto di spawn del rampino

    //Variabili
    private LineRenderer hook;                                                                      //Rampino
    private bool isHooked;                                                                          //Già rampinato?
    private Vector3 hookPoint;                                                                      //Punto d'aggancio rampino                                                                                                                               //Punti rampinabili
    private SpringJoint spring;                                                                     //Molla
    IEnumerator coroutine;

    void Start()
    {
        hook = gameObject.GetComponent<LineRenderer>();
        isHooked = false;
    }

    void Update()
    {
        if (aim.jigglyRaycasthitFound == 7 && Input.GetKeyDown(KeyCode.E) && isHooked == false)          //Se puoi rampinare, viene premuto E e non hai già rampinato
        {
            isHooked = true;
            StartHook();                                                                            //Rampina
        } else if(isHooked == true && Input.GetKeyDown(KeyCode.E))                                  //Se hai già rampinato e premi E
        {
            StopHook();                                                                             //Rompi rampino
            isHooked = false;
        }

        if(aim.jigglyRaycasthitFound == 6 && Input.GetKeyDown(KeyCode.E) && isHooked == false)
        {
            isHooked = true;
            JigglyAttack();
            StopCoroutine(DestroyJigglyAttack());
        }
        
    }

    private void LateUpdate()
    {
        drawHook();
    }

    private void StartHook()
    {
        hookPoint = aim.jigglyRaycasthit.point;                                                     //Punto in cui si aggancia il rampino
        spring = gameObject.AddComponent<SpringJoint>();                                            //Effetto molla per il rampino
        spring.autoConfigureConnectedAnchor = false;
        spring.connectedAnchor = hookPoint;                                                         //connette la molla al punto del rampino
        /*float distanceFromPoint = Vector3.Distance(gameObject.transform.position, hookPoint);     //distanze rampino (da sistemare)
        spring.maxDistance = distanceFromPoint * 0.8f;
        spring.minDistance = distanceFromPoint * 0.25f;*/
        spring.spring = 10f;                                                                        //Valori da modificare per sistemare il rampino a piacimento (da sistemare)
        spring.damper = 7f;
        spring.massScale = 4.5f;
        hook.positionCount = 2;                                                                     //Vertici del rampino
    }

    private void StopHook()
    {
        hook.positionCount = 0;                                                                     //Riduce i vertici a 0 per farlo sparire
        Destroy(spring);                                                                            //Distrugge l'effetto molla
    }

    private void drawHook()
    {
        if (isHooked == false)                                                                      //Se non c'è il rampino, non disegnare
        {
            return;
        }

        hook.SetPosition(0, hookSpawnPoint.transform.position);                                     //Primo punto del rampino
        hook.SetPosition(1, hookPoint);                                                             //Secondo punto del rampino
    }

    private void JigglyAttack()
    {
        hookPoint = aim.jigglyRaycasthit.point;                                                     //Punto in cui si aggancia il rampino
        hook.positionCount = 2;                                                                     //Vertici del rampino
        
        StartCoroutine(DestroyJigglyAttack());                                                      //Coroutine di distruzione
    }

    /*private void GemFromEnemy()
    {
        if (gameObject.name == "Fire Enemy" || gameObject.name == "Fire Enemy(Clone)")
        {
            drop = Resources.Load<GameObject>("FireGem");
        }
        else if (gameObject.name == "Water Enemy" || gameObject.name == "Water Enemy(Clone)")
        {
            drop = Resources.Load<GameObject>("Assets / Prefab / WaterGem.prefab");
        }
    }*/

    IEnumerator DestroyJigglyAttack()
    {
        yield return new WaitForSeconds(2);
        //GemFromEnemy();
        hook.positionCount = 0;
        isHooked = false;
    }
}
