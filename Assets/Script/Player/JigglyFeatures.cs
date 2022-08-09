using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JigglyFeatures : MonoBehaviour
{
    //Scripts
    public PlayerAim aim;                                                                           //Usato per la verifica col raycast se possibile rampinare/usare Jiggly
    public GameObject hookSpawnPoint;                                                               //Punto di spawn del rampino

    //Variabili Rampino
    public LineRenderer hook;                                                                      //Rampino
    private bool isHooked;                                                                          //Verifica se si sta usando il rampino
    private Vector3 hookPoint;                                                                      //Punto d'aggancio finale del rampino
    private SpringJoint spring;                                                                     //Effetto rampino

    //Variabili Attacco di Jiggly
    private bool jigglyAttackState;                                                                 //Verifica se si sta usando l'attacco di Jiggly
    private bool CooldownJigglyAttack;                                                              //Cooldown abilità di Jiggly
    private string enemyName;                                                                       //Nome nemico attaccato con Jiggly per il drop della gemma

    void Start()
    {
        isHooked = false;
        jigglyAttackState = false;
        CooldownJigglyAttack = false;
    }

    void Update()
    {
        //Rampino
        if (aim.jigglyRaycasthitLayer == "GrapplingPoint" && Input.GetKeyDown(KeyCode.E) && isHooked == false && jigglyAttackState == false)    //Se puoi rampinare, viene premuto E, non hai già rampinato e non è attivo l'attacco di Jiggly
        {
            isHooked = true;
            StartHook();                                                                                                //Rampina
        } else if(isHooked == true && Input.GetKeyDown(KeyCode.E) && jigglyAttackState == false)                        //Se hai già rampinato e premi E e non è attivo l'attaco di Jiggly
        {
            StopHook();                                                                                                 //Rompi rampino
            isHooked = false;
        }

        //Attacco di Jiggly
        if(aim.jigglyRaycasthitLayer == "Target" && Input.GetKeyDown(KeyCode.Q) && isHooked == false && CooldownJigglyAttack == false)  //Se  puoi attaccare, premi Q, non sei agganciato e non sei in cooldown  
        {
            enemyName = aim.jigglyRaycasthit.collider.gameObject.name;                                                  //Salvo il nome del nemico per la gemma
            jigglyAttackState = true;                                                                                   //Attacco di Jiggly attivo
            JigglyAttack();                                                                                             //Inizio attacco di Jiggly
        }
    }

    private void LateUpdate()
    {
        drawHook();                                                                                                     //Disegna Rampino o Attaco di Jiggly
    }


    //METODI RAMPINO

    //Crea Rampino
    private void StartHook()
    {
        hookPoint = aim.jigglyRaycasthit.point;                                                     //Punto in cui si aggancia il rampino
        hook.positionCount = 2;                                                                     //Vertici del rampino
        spring = gameObject.AddComponent<SpringJoint>();                                            //Effetto molla per il rampino
        spring.autoConfigureConnectedAnchor = false;                                                //Disattiva l'auto configurazione
        spring.connectedAnchor = hookPoint;                                                         //connette la molla al punto del rampino
        /*float distanceFromPoint = Vector3.Distance(gameObject.transform.position, hookPoint);     //distanze rampino (da sistemare)
        spring.maxDistance = distanceFromPoint * 0.8f;
        spring.minDistance = distanceFromPoint * 0.25f;*/
        spring.spring = 40f;                                                                        //Valori da modificare per sistemare il rampino a piacimento (da sistemare)
        spring.damper = 5f;
        spring.massScale = 20f;
    }

    //Distruggi Rampino
    private void StopHook()
    {
        hook.positionCount = 0;                                                                     //Riduce i vertici a 0 per farlo sparire
        Destroy(spring);                                                                            //Distrugge l'effetto molla
    }


    //METODI ATTACCO JIGGLY

    //Inizia Attacco Jiggly
    private void JigglyAttack()
    {
        hookPoint = aim.jigglyRaycasthit.point;                                                     //Punto in cui si aggancia il rampino
        hook.positionCount = 2;                                                                     //Vertici del rampino
        StartCoroutine(StartJigglyAttack());                                                        //Coroutine di esecuzione dell'attacco
        StartCoroutine(StartCooldownJigglyAttack());                                                //Cooldown abilità Jiggly
    }

    //Gestione attacco Jiggly
    IEnumerator StartJigglyAttack()
    {
        yield return new WaitForSeconds(2);
        GemFromEnemy();                                                                             //Istanzia nel punto del giocatore la gemma relativa al nemico agganciato
        enemyName = "";                                                                             //Resetta il nome del nemico
        hook.positionCount = 0;                                                                     //Distrugge l'attacco
        jigglyAttackState = false;                                                                  //Attacco Jiggly non attivo
        CooldownJigglyAttack = true;                                                                //Inizio Cooldown
    }

    //Cooldown attacco Jiggly
    IEnumerator StartCooldownJigglyAttack()
    {
        yield return new WaitForSeconds(5);
        CooldownJigglyAttack = false;                                                               //Disattiva Cooldown
    }

    //Istanzia nel punto del giocatore la gemma relativa al nemico agganciato
    private void GemFromEnemy()
    {
        if (enemyName == "Fire Enemy" || enemyName == "Fire Enemy(Clone)")                                              //Se il nemico è Fire Enemy
        {
            Instantiate(Resources.Load<GameObject>("FireGem"), gameObject.transform.position, Quaternion.identity);     //Istanzia Fire Gem
        }
        else if (enemyName == "Water Enemy" || enemyName == "Water Enemy(Clone)")                                       //Se il nemico è Water Enemy
        {
            Instantiate(Resources.Load<GameObject>("WaterGem"), gameObject.transform.position, Quaternion.identity);    //Istanzia Water Gem
        }
    }

    //Disegna Jiggly
    private void drawHook()
    {
        if (isHooked == false && jigglyAttackState == false)                                        //Se non c'è il rampino o l'attacco di Jiggly, non disegnare
        {
            return;
        }
        hook.SetPosition(0, hookSpawnPoint.transform.position);                                     //Primo punto del rampino
        hook.SetPosition(1, hookPoint);                                                             //Secondo punto del rampino
    }
}
