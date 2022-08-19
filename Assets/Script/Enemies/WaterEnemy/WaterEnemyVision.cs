using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterEnemyVision : MonoBehaviour
{
    private bool hasVision;
    public LayerMask player;
    public LayerMask obstacles;

    private void Start()
    {
        StartCoroutine(FOVRoutine());
    }
    private void Update()
    {
        if (hasVision)
        {
            gameObject.GetComponent<WaterEnemyMovement>().enabled = true;
            gameObject.GetComponent<WaterEnemyAttack>().enabled = true;
        }
    }

    IEnumerator FOVRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            FOVCheck();
        }
    }

    private void FOVCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, 45, player);
        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;
            float distanceToTarget = Vector3.Distance(transform.position, target.position);
            if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstacles))
            {
                hasVision = true;
            }
        }
    }
}
