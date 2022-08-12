using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaterEnemyBehaviour : MonoBehaviour
{
    private GameObject player;
    private NavMeshAgent agent;
    public GameObject bulletSpawnPoint;

    private float distance;
    private bool canAttack;

    void Start()
    {
        player = GameObject.Find("Amnery");
        agent = GetComponent<NavMeshAgent>();
        canAttack = false;
        StartCoroutine(AttackCooldown());
    }

    private void Update()
    {
        distance = Vector3.Distance(player.transform.position, gameObject.transform.position);
        if (distance > agent.stoppingDistance)
        {
            agent.SetDestination(player.transform.position);
            if(distance == agent.stoppingDistance)
            {
                agent.SetDestination(gameObject.transform.position);
                //Cammina in cerchio
            }
        }
        else
        {
            //Allontana
        }

        if(canAttack == true)
        {
            Instantiate(Resources.Load("WaterSpray"), bulletSpawnPoint.transform.position, Quaternion.LookRotation(player.transform.position, Vector3.up));
            StartCoroutine(AttackCooldown());
        }
    }

    IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(3);
        canAttack = true;
    }
}
