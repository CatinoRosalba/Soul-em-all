using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    private GameObject player;
    private NavMeshAgent agent;
    private Rigidbody rb;

    private float distance;
    private bool isFollowing;
    private bool canAttack;

    void Start()
    {
        player = GameObject.Find("Amnery");
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        isFollowing = true;
        canAttack = true;
    }

    private void Update()
    {
        distance = Vector3.Distance(player.transform.position, gameObject.transform.position);
        if (isFollowing == true)
        {
            agent.SetDestination(player.transform.position);
        }
    }

    private void FixedUpdate()
    {
        if (distance <= agent.stoppingDistance && canAttack == true)
        {
            StartCoroutine(Melee(agent, distance));
        }
    }

    IEnumerator Melee(NavMeshAgent agent, float distance)
    {
        canAttack = false;
        isFollowing = false;
        agent.enabled = false;
        yield return new WaitForSeconds(0.5f);
        Vector3 direction = player.transform.position - gameObject.transform.position;
        rb.AddForce(direction * 7.5f, ForceMode.Impulse);
        yield return new WaitForSeconds(0.7f);
        agent.enabled = true;
        isFollowing = true;
        StartCoroutine(AttackCooldown());
    }
    
    IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(3);
        canAttack = true;
    }
}
