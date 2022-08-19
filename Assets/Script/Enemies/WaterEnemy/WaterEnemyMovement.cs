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
    public GameObject rightStep;
    public GameObject leftStep;                                        
    private float distance;                                            //Distanza tra entità e giocatore
    private float minDistanceFromPlayer;                               //Distanza minima di stop dell'entità dal giocatore
    private float maxDistanceFromPlayer;                               //Distanza massima di stop dell'entità dal giocatore
    private int direction;                                             //Direzione di spostamento laterale
    private bool canChangeDir;                                         //Usata per il cooldown di ricalcolo della direzione dello spostamento laterale

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Amnery");
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        minDistanceFromPlayer = 10;
        maxDistanceFromPlayer = 14;
        canChangeDir = true;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(player.transform.position, gameObject.transform.position);    //Calcolo continuamente la distanza dell'entità dal giocatore
        gameObject.transform.LookAt(player.transform.position);                                   //L'entità guarda il giocatore per settare continuamente i assi
        if (distance > maxDistanceFromPlayer)                                                     //Se è più lontano della distanza di stop massima
        {
            agent.SetDestination(player.transform.position);                                      //Il NavMeshAgent lo sposta verso il giocatore
        }
        if (distance < minDistanceFromPlayer)                                                     //Se si trova dopo la distanza minima di stop dal giocatorw
        {
            Vector3 direction = (player.transform.position - gameObject.transform.position).normalized;
            for (int i = 0; i < 30; i++)
            {
                NavMeshHit hit;
                if (NavMesh.SamplePosition(gameObject.transform.position - direction, out hit, 3, NavMesh.AllAreas))
                {
                    agent.SetDestination(hit.position);
                    break;
                }
            }
        }
        if (distance <= maxDistanceFromPlayer && distance >= minDistanceFromPlayer)                  //Se l'entità è tra la distanza minima e massima di stop
        {
            if (canChangeDir)
            {
                direction = Random.Range(1, 3);
                StartCoroutine(DirectionChangeCooldown());
            }
            Debug.Log(direction);
            if (direction == 1)
            {
                agent.SetDestination(rightStep.transform.position);
            } else if ( direction == 2)
            {
                agent.SetDestination(leftStep.transform.position);
            }
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
        direction = Random.Range(1, 3);
        StartCoroutine(DirectionChangeCooldown());
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(gameObject.transform.position, agent.destination);
    }
}
