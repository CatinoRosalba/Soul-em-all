using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FireEnemyAttack : MonoBehaviour
{
    private GameObject player;                                                                      //Giocatore
    private NavMeshAgent agent;                                                                     //NavMeshAgent
    private Rigidbody rb;                                                                           //Rigidbody

    private float distance;                                                                         //Distanza entità da giocatore
    private bool canAttack;                                                                         //Usato per controllare se può attaccare
    private bool canBrake;                                                                          //Usato per controllare se può frenare lo scatto

    void Start()
    {
        player = GameObject.Find("Amnery");
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        canAttack = false;
        StartCoroutine(AttackCooldown());
    }

    private void Update()
    {
        distance = Vector3.Distance(player.transform.position, gameObject.transform.position);          //Calcolo continuamente la distanza tra l'entità e il giocatore
        if (distance > agent.stoppingDistance)                                                                        //Se sta seguendo il giocatore
        {
            agent.SetDestination(player.transform.position);                                            //Il NavMeshAgent direziona l'entità al giocatore
        }
    }

    private void FixedUpdate()
    {
        if (distance <= agent.stoppingDistance && canAttack == true)                                    //Se si trova nel range di stop e può attaccare
        {
            StartCoroutine(Melee());                                                                    //Scatta sul giocatore
        }
        if(canBrake == true)                                                                            //Se può frenare
        {
            StartCoroutine(Brake());                                                                    //Frena
        }
        else                                                                                            //Se non deve frenare
        {
            rb.drag = 1;                                                                                //Resetta il drag
        }
    }

    //Attacco scatto
    IEnumerator Melee()
    {
        canAttack = false;                                                                              //Resetta la possibilità di poter attaccare
        agent.enabled = false;                                                                          //Disattiva il NavMeshAgent per evitare che entri in conflitto con la fisica

        yield return new WaitForSeconds(0.7f);
        Vector3 direction = player.transform.position - gameObject.transform.position;                  //Calcola la direzione tra il giocatore e l'entità
        rb.AddForce(direction * 7.5f, ForceMode.Impulse);                                               //Scatta
        canBrake = true;                                                                                //Abilità il freno

        yield return new WaitForSeconds(0.7f);
        agent.enabled = true;                                                                           //Riattiva il NavMeshAgent
        canBrake = false;                                                                               //Disattiva la frenata
        StartCoroutine(AttackCooldown());                                                               //Attiva il cooldown dell'attacco
    }

    //Frenata
    IEnumerator Brake()
    {
        float initialDistance = Vector3.Distance(player.transform.position, gameObject.transform.position);     //Primo calcolo della distanza tra giocatore e entità
        yield return 0; 
        float secondDistance = Vector3.Distance(player.transform.position, gameObject.transform.position);      //Secondo calcolo della distanza tra giocatore e entità
        if (secondDistance > initialDistance)                                                                   //Se la seconda distanza è maggiore della prima (si sta allontanando dal giocatore ovvero lo ha mancato)
        {
            rb.drag = 8;                                                                                        //Aumenta il drag per frenare
        }
    }
    
    //Cooldown attacco
    IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(3);
        canAttack = true;
    }
}
