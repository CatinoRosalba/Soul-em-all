using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class WaterEnemyMovement : MonoBehaviour
{
    private GameObject player;                                         //Giocatore
    public NavMeshAgent agent;                                         //NavMeshAgent dell'entità
    public Rigidbody rb;                                               //Rigidbody
    private float distance;                                            //Distanza tra entità e giocatore
    private float minStoppingDistance;                                 //Distanza minima di stop dell'entità dal giocatore
    private float maxStoppingDistance;                                 //Distanza massima di stop dell'entità dal giocatore
    private int direction;                                             //Direzione di spostamento laterale
    private bool canChangeDir;                                         //Usata per il cooldown di ricalcolo della direzione dello spostamento laterale

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Amnery");
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        minStoppingDistance = agent.stoppingDistance - 2;
        maxStoppingDistance = agent.stoppingDistance + 2;
        ChangeDirection();
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(player.transform.position, gameObject.transform.position);      //Calcolo continuamente la distanza dell'entità dal giocatore
        //gameObject.transform.LookAt(player.transform.position);                                     //L'entità guarda il giocatore per settare continuamente i assi
        if (distance > maxStoppingDistance)                                                         //Se è più lontano della distanza di stop massima
        {
            agent.enabled = true;                                                                   //Attiva NavMeshAgent
            agent.SetDestination(player.transform.position);                                        //Il NavMeshAgent lo sposta verso il giocatore
        }
        if (distance < minStoppingDistance)                                                    //Se si trova dopo la distanza minima di stop dal giocatorw
        {
            Vector3 direction = (player.transform.position - gameObject.transform.position).normalized;
            for (int i = 0; i < 30; i++)
            {
                NavMeshHit hit;
                if (NavMesh.SamplePosition(gameObject.transform.position - direction * agent.stoppingDistance * 2, out hit, 3, NavMesh.AllAreas))
                {
                    agent.SetDestination(hit.position);
                    Debug.Log(agent.destination);
                    break;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        /*if (distance <= maxStoppingDistance && distance >= minStoppingDistance)                    //Se l'entità è tra la distanza minima e massima di stop
        {
            agent.enabled = false;                                                                   //Disattiva NavMeshAgent (non funziona per queste due feature, si disattiva per evitare di entrare in conflitto con la fisica)
            rb.velocity = transform.right * 10 * direction;                                          //Spostamento laterale nella direzione scelta randomicamente
            if (canChangeDir == true)                                                                //Se il cooldown di cambio direzione è finito
            {
                ChangeDirection();
                StartCoroutine(DirectionChangeCooldown());
            }
        }
        else*/ 
    }

    private void ChangeDirection()
    {
        canChangeDir = true;                                                                        //Può cambiare direzione
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
    }

    //Cambia direzione spostamento laterale
    IEnumerator DirectionChangeCooldown()
    {
        canChangeDir = false;                                                                       //Non può cambiare direzione
        yield return new WaitForSeconds(2.5f);
        canChangeDir = true;                                                                        //Può cambiare direzione
    }

    private void OnCollisionEnter(Collision collision)
    {
        StopCoroutine(DirectionChangeCooldown());
        direction = -direction;
        StartCoroutine(DirectionChangeCooldown());
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(gameObject.transform.position, agent.destination);
    }
}
