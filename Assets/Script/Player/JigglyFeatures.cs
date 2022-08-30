using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JigglyFeatures : MonoBehaviour
{
    //Scripts
    private PlayerAim aim;                                                                          //Usato per la verifica col raycast se possibile rampinare/usare Jiggly
    private GameObject hookSpawnPoint;                                                              //Punto di spawn del rampino
    [SerializeField] UIManager slot;                                                                //Interfaccia

    //Variabili Rampino
    public LineRenderer hook;                                                                       //Rampino
    public bool isHooked;                                                                          //Verifica se si sta usando il rampino
    private Vector3 hookPoint;                                                                      //Punto d'aggancio finale del rampino (usato pure per l'attacco di Jiggly)
    private Rigidbody rb;                                                                           //Rigidbody usatp per il pull
    private bool isPulling;                                                                         //Se il giocatore sat venendo tirato dal rampino
    private bool canHook;                                                                           //Variabile usata per il cooldown del rampino
    private float maxHookRange;                                                                     //Range massimo del rampino

    //Variabili Attacco di Jiggly
    public bool jigglyAttackState;                                                                 //Verifica se si sta usando l'attacco di Jiggly
    private bool CooldownJigglyAttack;                                                              //Cooldown abilità di Jiggly
    private GameObject enemy;                                                                       //Nemico attaccato con Jiggly per il drop della gemma
    private string enemyName;                                                                       //Nome del nemico agganciato
    private float maxJigglyAttackRange;                                                             //Range massimo dell'attacco di Jiggly

    void Start()
    {
        hook.enabled = false;
        isHooked = false;
        jigglyAttackState = false;
        CooldownJigglyAttack = false;
        canHook = true;
        isPulling = false;
        maxHookRange = 40;
        maxJigglyAttackRange = 20;
        rb = gameObject.GetComponent<Rigidbody>();
        aim = gameObject.GetComponent<PlayerAim>();
        hookSpawnPoint = transform.Find("JigglySpawnPoint").gameObject;
    }

    void Update()
    {
        //Rampino
        if (aim.jigglyRaycasthitLayer == "GrapplingPoint" && Input.GetKeyDown(KeyCode.LeftShift) && isHooked == false && jigglyAttackState == false && canHook == true)    //Se puoi rampinare, viene premuto E, non hai già rampinato e non è attivo l'attacco di Jiggly e è finito il cooldown
        {
            if (isInRange(maxHookRange))                                                                                //Se in range
            {   
                hook.enabled = true;                                                                                    //Attiva rampino
                isHooked = true;                                                                                        //Sei rampinato
                StartHook();                                                                                            //Rampina
            }
        } else if(isHooked == true && Input.GetKeyDown(KeyCode.LeftShift) && jigglyAttackState == false)                        //Se hai già rampinato e premi E e non è attivo l'attaco di Jiggly
        {
            hook.enabled = false;                                                                                       //Disattiva rampino
            StopHook();                                                                                                 //Rompi rampino
            isHooked = false;                                                                                           //Non sei rampinato
        }

        if (isPulling)                                                                                                  //Se il rampino tira il giocatore
        {
            rb.AddForce((hookPoint - gameObject.transform.position).normalized * 0.4f, ForceMode.VelocityChange);       //Tira con al fisica
        }

        //Attacco di Jiggly
        if(aim.jigglyRaycasthitLayer == "Enemy" && Input.GetKeyDown(KeyCode.R) && isHooked == false && CooldownJigglyAttack == false)  //Se  puoi attaccare, premi Q, non sei agganciato e non sei in cooldown  
        {
            if (isInRange(maxJigglyAttackRange))
            {
                hook.enabled = true;                                                                                    //Attiva rampino
                enemy = aim.jigglyRaycasthit.collider.gameObject;                                                       //Nemico agganciato
                enemyName = enemy.name;                                                                                 //Nome nemico agganciato
                jigglyAttackState = true;                                                                               //Attacco di Jiggly attivo
                hook.positionCount = 2;                                                                                 //Vertici del rampino
                slot.DisableJigglyAttackSlot();                                                                         //Countdown skill
                StartCoroutine(StartJigglyAttack());                                                                    //Coroutine di esecuzione dell'attacco
                StartCoroutine(StartCooldownJigglyAttack());                                                            //Cooldown abilità Jiggly                                                                                             //Inizio attacco di Jiggly
            }
        }
        if (slot.isAttackCountDown)                                                                                     //Se lo slot è in cooldown
        {
            slot.ApplyAttackCountDown();                                                                                //aggiorna il countdown
        }
        if (jigglyAttackState == true)                                                                                  //Se stai usando l'attacco di Jiggl
        {
            hookPoint = enemy.transform.position;                                                                       //Aggiorna l'aggancio al punto in cui si aggancia il rampino
        }
    }

    private void LateUpdate()
    {
        drawHook();                                                                                                     //Disegna Rampino o Attaco di Jiggly
    }

    //Range massimo di Jiggly
    private bool isInRange(float maxRange)
    {
        float distance = Vector3.Distance(aim.jigglyRaycasthit.point, gameObject.transform.position);                   //Distanza tra player e punto d'aggrappo
        if (distance <= maxRange)                                                                                       //Se minore del range massimo
        {
            return true;                                                                                                //Puoi aggrapparti
        }
        return false;                                                                                                   //Non puoi aggrapparti
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

    //METODI RAMPINO

    //Crea Rampino
    private void StartHook()
    {
        hookPoint = aim.jigglyRaycasthit.collider.transform.position;                               //Punto in cui si aggancia il rampino
        hook.positionCount = 2;                                                                     //Vertici del rampino
        isPulling = true;                                                                           //Il rampino tira il giocatore
        rb.useGravity = false;
    }

    //Distruggi Rampino
    private void StopHook()
    {
        hook.positionCount = 0;                                                                     //Riduce i vertici a 0 per farlo sparire
        isPulling = false;                                                                          //Il rampino non tira il giocatore
        rb.useGravity = true;
        StartCoroutine(HookCooldown());                                                             //Attiva cooldown
    }

    //Cooldown rampino
    IEnumerator HookCooldown()
    {
        canHook = false;
        yield return new WaitForSeconds(1);
        canHook = true;
    }


    //METODI ATTACCO JIGGLY

    //Gestione attacco Jiggly
    IEnumerator StartJigglyAttack()
    {
        yield return new WaitForSeconds(1.5f);
        GemFromEnemy();                                                                             //Istanzia nel punto del giocatore la gemma relativa al nemico agganciato
        hook.enabled = false;                                                                       //Disattiva rampino
        enemyName = "";                                                                             //Resetta il nome del nemico
        enemy = null;                                                                               //Resetta il nemico agganciato
        hook.positionCount = 0;                                                                     //Distrugge l'attacco
        jigglyAttackState = false;                                                                  //Attacco Jiggly non attivo
    }

    //Cooldown attacco Jiggly
    IEnumerator StartCooldownJigglyAttack()
    {
        CooldownJigglyAttack = true;                                                                //Inizio Cooldown
        yield return new WaitForSeconds(7);
        CooldownJigglyAttack = false;                                                               //Disattiva Cooldown
    }

    //Istanzia nel punto del giocatore la gemma relativa al nemico agganciato
    private void GemFromEnemy()
    {
        if (enemyName.Contains("Fire"))                                               //Se il nemico è Fire Enemy
        {
            Instantiate(Resources.Load<GameObject>("Gems/FireGem"), gameObject.transform.position, Quaternion.identity);     //Istanzia Fire Gem
        }
        else if (enemyName.Contains("Water"))                                         //Se il nemico è Water Enemy
        {
            Instantiate(Resources.Load<GameObject>("Gems/WaterGem"), gameObject.transform.position, Quaternion.identity);    //Istanzia Water Gem
        }
    }
}
