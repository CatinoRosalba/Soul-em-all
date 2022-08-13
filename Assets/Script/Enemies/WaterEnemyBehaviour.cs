using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaterEnemyBehaviour : MonoBehaviour
{
    private GameObject player;
    private NavMeshAgent agent;
    public GameObject bulletSpawnPoint;
    private Rigidbody rb;

    private float distance;
    private float minStoppingDistance;
    private float maxStoppingDistance;
    private int direction;
    private bool canChangeDir;
    private bool canMove;
    private bool canAttack;
    private Vector3 aimDir;

    void Start()
    {
        player = GameObject.Find("Amnery");
        rb = gameObject.GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        minStoppingDistance = agent.stoppingDistance - 2;
        maxStoppingDistance = agent.stoppingDistance + 2;
        canMove = true;
        canAttack = false;
        StartCoroutine(AttackCooldown());
        StartCoroutine(DirectionStrafe());
    }

    private void Update()
    {
        distance = Vector3.Distance(player.transform.position, gameObject.transform.position);
        gameObject.transform.LookAt(player.transform.position);
        if (distance > maxStoppingDistance)
        {
            agent.enabled = true;
            agent.SetDestination(player.transform.position);
        }
        if(canAttack == true)
        {
            StartCoroutine(Attack());
        }
    }

    private void FixedUpdate()
    {
        if (distance <= maxStoppingDistance && distance >= minStoppingDistance)
        {
            agent.enabled = false;
            rb.velocity = transform.right * 10 * direction;
            if(canChangeDir == true)
            {
                StartCoroutine(DirectionStrafe());
            }
        }
        else if (distance < minStoppingDistance)
        {
            agent.enabled = false;
            rb.velocity = transform.forward * -10;
        }
    }

    IEnumerator DirectionStrafe()
    {
        canChangeDir = false;
        int random = Random.Range(1, 3);
        switch (random)
        {
            case 1:
                direction = -1;
                break;

            case 2:
                direction = 1;
                break;
        }
        yield return new WaitForSeconds(2.5f);
        canChangeDir = true;
    }

    IEnumerator Attack()
    {
        Vector3 playerPositionMax = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z + 2.5f);
        Vector3 playerPositionMin = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z - 2.5f);
        int randomTarget = Random.Range(1, 4);
        switch (randomTarget)
        {
            case 1:
                aimDir = (playerPositionMax - gameObject.transform.position).normalized;
                break;

            case 2:
                aimDir = (playerPositionMin - gameObject.transform.position).normalized;
                break;
            case 3:
                aimDir = (player.transform.position - gameObject.transform.position).normalized;
                break;
        }
        yield return new WaitForSeconds(0.2f);
        Instantiate(Resources.Load("EnemyWaterSpray"), bulletSpawnPoint.transform.position, Quaternion.LookRotation(aimDir, Vector3.up));
        StartCoroutine(AttackCooldown());
    }

    IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(4f);
        canAttack = true;
    }
}
