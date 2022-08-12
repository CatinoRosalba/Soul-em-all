using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JigglyFeatures : MonoBehaviour
{
    //Scripts
    public PlayerAim aim;                                                                           //Usato per la verifica col raycast se possibile rampinare/usare Jiggly
    public GameObject hookSpawnPoint;                                                               //Punto di spawn del rampino

    //Variabili Rampino
    public LineRenderer hook;                                                                       //Rampino
    private bool isHooked;                                                                          //Verifica se si sta usando il rampino
    private Vector3 hookPoint;                                                                      //Punto d'aggancio finale del rampino (usato pure per l'attacco di Jiggly)
    private SpringJoint spring;                                                                     //Effetto rampino
    private bool canHook;                                                                           //Variabile usata per il cooldown del rampino
    private float maxHookRange;                                                                     //Range massimo del rampino
    private float minHookRange;                                                                     //Range minimo del rampino

    //Variabili Attacco di Jiggly
    private bool jigglyAttackState;                                                                 //Verifica se si sta usando l'attacco di Jiggly
    private bool CooldownJigglyAttack;                                                              //Cooldown abilità di Jiggly
    private GameObject enemy;                                                                       //Nemico attaccato con Jiggly per il drop della gemma
    private string enemyName;                                                                       //Nome del nemico agganciato
    private float maxJigglyAttackRange;                                                             //Range massimo dell'attacco di Jiggly
    
    void Start()
    {
        isHooked = false;
        jigglyAttackState = false;
        CooldownJigglyAttack = false;
        canHook = true;
        maxHookRange = 30;
        maxJigglyAttackRange = 20;
    }

    void Update()
    {
        //Rampino
        if (aim.jigglyRaycasthitLayer == "GrapplingPoint" && Input.GetKeyDown(KeyCode.E) && isHooked == false && jigglyAttackState == false && canHook == true)    //Se puoi rampinare, viene premuto E, non hai già rampinato e non è attivo l'attacco di Jiggly e è finito il cooldown
        {
            if (isInRange(maxHookRange))                                                                                //Se in range
            {
                isHooked = true;
                StartHook();                                                                                            //Rampina
            }
        } else if(isHooked == true && Input.GetKeyDown(KeyCode.E) && jigglyAttackState == false)                        //Se hai già rampinato e premi E e non è attivo l'attaco di Jiggly
        {
            StopHook();                                                                                                 //Rompi rampino
            isHooked = false;
        }

        //Attacco di Jiggly
        if(aim.jigglyRaycasthitLayer == "Target" && Input.GetKeyDown(KeyCode.Q) && isHooked == false && CooldownJigglyAttack == false)  //Se  puoi attaccare, premi Q, non sei agganciato e non sei in cooldown  
        {
            if (isInRange(maxJigglyAttackRange))
            {
                enemy = aim.jigglyRaycasthit.collider.gameObject;                                                       //Nemico agganciato
                enemyName = enemy.name;                                                                                 //Nome nemico agganciato
                jigglyAttackState = true;                                                                               //Attacco di Jiggly attivo
                hook.positionCount = 2;                                                                                 //Vertici del rampino
                StartCoroutine(StartJigglyAttack());                                                                    //Coroutine di esecuzione dell'attacco
                StartCoroutine(StartCooldownJigglyAttack());                                                            //Cooldown abilità Jiggly                                                                                             //Inizio attacco di Jiggly
            }
        }
        if (jigglyAttackState == true)
        {
            hookPoint = enemy.transform.position;                                                                       //Punto in cui si aggancia il rampino
        }
    }

    private void LateUpdate()
    {
        drawHook();                                                                                                     //Disegna Rampino o Attaco di Jiggly
    }

    //Range massimo di Jiggly
    private bool isInRange(float maxRange)
    {
        float distance = (aim.jigglyRaycasthit.point - gameObject.transform.position).magnitude;                                         //Distanza tra player e punto d'aggrappo
        Debug.Log(distance);
        if (distance <= maxRange)                                                                                       //Se minore del range massimo
        {
            return true;                                                                                                //Puoi aggrapparti
        }
        return false;                                                                                                   //Non puoi aggrapparti
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
        spring.spring = 20f;                                                                        //Valori da modificare per sistemare il rampino a piacimento (da sistemare)
        spring.damper = 3.5f;
        spring.massScale = 20f;
    }

    //Distruggi Rampino
    private void StopHook()
    {
        hook.positionCount = 0;                                                                     //Riduce i vertici a 0 per farlo sparire
        Destroy(spring);                                                                            //Distrugge l'effetto molla
        StartCoroutine(HookCooldown());
    }

    IEnumerator HookCooldown()
    {
        canHook = false;
        yield return new WaitForSeconds(2);
        canHook = true;
    }


    //METODI ATTACCO JIGGLY

    //Inizia Attacco Jiggly
    private void JigglyAttack()
    {
        
    }

    //Gestione attacco Jiggly
    IEnumerator StartJigglyAttack()
    {
        yield return new WaitForSeconds(2);
        GemFromEnemy();                                                                             //Istanzia nel punto del giocatore la gemma relativa al nemico agganciato
        enemyName = "";                                                                             //Resetta il nome del nemico
        enemy = null;                                                                               //Resetta il nemico agganciato
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
