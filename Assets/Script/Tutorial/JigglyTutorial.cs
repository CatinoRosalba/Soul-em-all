using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JigglyTutorial : MonoBehaviour
{
    public JigglyFeatures jiggly;
    public GameObject fireEnemy;
    private GameObject block;
    private bool hasHooked;
    private bool hasJigglyAttacked;

    private void Start()
    {
        hasHooked = false;
        hasJigglyAttacked = false;
        block = transform.parent.gameObject;
    }

    private void Update()
    {
        if (jiggly.isHooked)
        {
            hasHooked = true;
        }
        if (jiggly.jigglyAttackState)
        {
            hasJigglyAttacked = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasHooked && hasJigglyAttacked)
        {
            block.GetComponent<BoxCollider>().enabled = false;
            Destroy(fireEnemy);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(hasHooked && hasJigglyAttacked)
        {
            block.GetComponent<BoxCollider>().enabled = false;
            Destroy(fireEnemy);
        }
    }
}
