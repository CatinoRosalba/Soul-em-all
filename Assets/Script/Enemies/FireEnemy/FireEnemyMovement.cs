using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class FireEnemyMovement : MonoBehaviour
{
    public GameObject player;                                                                      //Giocatore
    public NavMeshAgent agent;                                                                     //NavMeshAgent
    public float distance;                                                                         //Distanza entit� da giocatore

    void Start()
    {
        player = GameObject.Find("Amnery");
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        distance = Vector3.Distance(player.transform.position, gameObject.transform.position);          //Calcolo continuamente la distanza tra l'entit� e il giocatore
        if (distance > agent.stoppingDistance && agent.enabled == true)                                 //Se sta seguendo il giocatore
        {
            agent.SetDestination(player.transform.position);                                            //Il NavMeshAgent direziona l'entit� al giocatore
        }
    }
}
