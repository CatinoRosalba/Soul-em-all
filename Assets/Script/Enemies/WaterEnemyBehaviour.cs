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
    private Vector3 aimDir;
    private Vector3 backstep;

    void Start()
    {
        player = GameObject.Find("Amnery");
        agent = GetComponent<NavMeshAgent>();
        canAttack = false;
        StartCoroutine(AttackCooldown());
    }

    private void Update()
    {
        gameObject.transform.LookAt(player.transform.position);
        distance = Vector3.Distance(player.transform.position, gameObject.transform.position);
        if (distance > agent.stoppingDistance)
        {
            agent.SetDestination(player.transform.position);
            if(distance == agent.stoppingDistance)
            {
                //Cammina in cerchio
            }
        }
        else
        {
            //Backstep
        }

        if(canAttack == true)
        {
            StartCoroutine(Attack());
        }
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
