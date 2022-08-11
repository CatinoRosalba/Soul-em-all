using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    private GameObject player;
    private NavMeshAgent agent;
    private Rigidbody rb;

    float distance;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Amnery");
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(player.transform.position);
        distance = Vector3.Distance(player.transform.position, gameObject.transform.position);
        if (distance <= agent.stoppingDistance)
        {
            //Si ferma, carica e attacca
        }
    }
}
