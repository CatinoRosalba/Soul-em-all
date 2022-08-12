using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FireEnemyBehaviour : MonoBehaviour
{
    private GameObject player;
    private NavMeshAgent agent;
    private Rigidbody rb;

    private float distance;
    private bool isFollowing;
    private bool canAttack;
    private bool canBrake;

    void Start()
    {
        player = GameObject.Find("Amnery");
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        isFollowing = true;
        canAttack = false;
        StartCoroutine(AttackCooldown());
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

        if(canBrake == true)
        {
            StartCoroutine(Brake());
        }
        else
        {
            rb.drag = 1;
        }
    }

    IEnumerator Melee(NavMeshAgent agent, float distance)
    {
        canAttack = false;
        isFollowing = false;
        agent.enabled = false;

        yield return new WaitForSeconds(0.7f);
        Vector3 direction = player.transform.position - gameObject.transform.position;
        rb.AddForce(direction * 7.5f, ForceMode.Impulse);
        canBrake = true;

        yield return new WaitForSeconds(0.7f);
        agent.enabled = true;
        isFollowing = true;
        canBrake = false;
        StartCoroutine(AttackCooldown());
    }

    IEnumerator Brake()
    {
        float initialDistance = Vector3.Distance(player.transform.position, gameObject.transform.position);
        yield return 0; 
        float secondDistance = Vector3.Distance(player.transform.position, gameObject.transform.position);
        if (secondDistance > initialDistance)
        {
            rb.drag = 8;
        }
    }
    
    IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(3);
        canAttack = true;
    }
}
