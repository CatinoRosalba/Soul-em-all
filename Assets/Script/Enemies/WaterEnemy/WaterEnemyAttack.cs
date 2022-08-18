using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaterEnemyAttack : MonoBehaviour
{
    private GameObject player;                                          //Giocatore
    private NavMeshAgent agent;                                         //NavMeshAgent dell'entità
    private GameObject bulletSpawnPoint;                                //Punto di spawn proiettili
    private Rigidbody rb;                                               //Rigidbody

    private float distance;                                             //Distanza tra entità e giocatore
    private float minStoppingDistance;                                  //Distanza minima di stop dell'entità dal giocatore
    private float maxStoppingDistance;                                  //Distanza massima di stop dell'entità dal giocatore
    private int direction;                                              //Direzione di spostamento laterale
    private bool canChangeDir;                                          //Usata per il cooldown di ricalcolo della direzione dello spostamento laterale
    private bool canAttack;                                             //Usata per il cooldown dell'attacco
    private bool canChangeDirAim;                                       //Usata per il cooldown di cambio direzione sparo
    private Vector3 aimDir;                                             //Direzione di mira

    void Start()
    {
        player = GameObject.Find("Amnery");
        bulletSpawnPoint = transform.Find("bulletSpawnPoint").gameObject;
        rb = gameObject.GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        minStoppingDistance = agent.stoppingDistance - 2;
        maxStoppingDistance = agent.stoppingDistance + 2;
        canChangeDirAim = false;
        canAttack = false;
        StartCoroutine(AttackCooldown());
        StartCoroutine(DirectionStrafe());
    }

    private void Update()
    {
        distance = Vector3.Distance(player.transform.position, gameObject.transform.position);      //Calcolo continuamente la distanza dell'entità dal giocatore
        gameObject.transform.LookAt(player.transform.position);                                     //L'entità guarda il giocatore per settare continuamente i assi
        if (distance > maxStoppingDistance)                                                         //Se è più lontano della distanza di stop massima
        {
            agent.enabled = true;                                                                   //Attiva NavMeshAgent
            agent.SetDestination(player.transform.position);                                        //Il NavMeshAgent lo sposta verso il giocatore
        }
        if(canAttack == true)                                                                       //Se può attaccare
        {
            if(canChangeDirAim == true)                                                             //Se può cambiare direzione sparo (fatto per non chiamare tante volte questo metodo ma solo la coroutine dello sparo)
            {
                AimDirection();                                                                     //Cambia direzione mira
            }
            StartCoroutine(Attack());                                                               //Attacca
        }
    }

    private void FixedUpdate()
    {
        if (distance <= maxStoppingDistance && distance >= minStoppingDistance)                     //Se l'entità è tra la distanza minima e massima di stop
        {
            agent.enabled = false;                                                                  //Disattiva NavMeshAgent (non funziona per queste due feature, si disattiva per evitare di entrare in conflitto con la fisica)
            rb.velocity = transform.right * 10 * direction;                                         //Spostamento laterale nella direzione scelta randomicamente
            if(canChangeDir == true)                                                                //Se il cooldown di cambio direzione è finito
            {
                StartCoroutine(DirectionStrafe());                                                  //Cambia direzione
            }
        }
        else if (distance < minStoppingDistance)                                                    //Se si trova dopo la distanza minima di stop dal giocatorw
        {
            agent.enabled = false;                                                                  //Disattiva NavMeshAgent (non funziona per queste due feature, si disattiva per evitare di entrare in conflitto con la fisica)
            rb.velocity = transform.forward * -10;                                                  //Sposta indietro l'entità
        }
    }

    //Cambia direzione spostamento laterale
    IEnumerator DirectionStrafe()
    {
        canChangeDir = false;                                                                       //Non può cambiare direzione
        yield return new WaitForSeconds(2.5f);
        int random = Random.Range(1, 3);                                                            //Scelta randomica direzione (da 1 a 3-1=2)
        switch (random)
        {
            case 1:
                direction = -1;                                                                     //Setta sinistra
                break;

            case 2:
                direction = 1;                                                                      //Setta destra
                break;
        }
        canChangeDir = true;                                                                        //Può cambiare direzione
    }

    //Cambia direzione mira
    private void AimDirection()
    {
        Vector3 playerPositionMax = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z + 2.5f);      //Sparo leggermente a destra
        Vector3 playerPositionMin = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z - 2.5f);      //Sparo leggermente a sinistra
        int randomTarget = Random.Range(1, 4);                                                      //Scelta randomica della direzione dello sparo (da 1 a 4-1=3
        switch (randomTarget)
        {
            case 1:
                aimDir = (playerPositionMax - gameObject.transform.position).normalized;            //Setta leggermente destra
                break;

            case 2:
                aimDir = (playerPositionMin - gameObject.transform.position).normalized;            //Setta leggermente sinistra
                break;
            case 3:
                aimDir = (player.transform.position - gameObject.transform.position).normalized;    //Setta centrale
                break;
        }
        canChangeDirAim = false;                                                                    //Non cambiare direzione mira
    }

    //Spara tanti proiettili in base a quante volte viene chiamata
    IEnumerator Attack()
    {
        yield return new WaitForSeconds(0.2f);
        Instantiate(Resources.Load("Projectiles/EnemyWaterSpray"), bulletSpawnPoint.transform.position, Quaternion.LookRotation(aimDir, Vector3.up));   //Spara tanti proiettili in base a quante volte è stata chiamat la coroutine in 0.2 secondi dal cooldown dello sparo nella direzione scelta
        StartCoroutine(AttackCooldown());                                                           //Cooldown sparo
    }

    //Cooldown sparo
    IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(4f);
        canAttack = true;
        canChangeDirAim = true;
    }
}
