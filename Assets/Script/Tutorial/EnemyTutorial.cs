using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTutorial : MonoBehaviour
{
    private float totalEnemies;
    private GameObject block;
    public GameObject fireEnemy;
    public GameObject waterEnemy;

    private void Start()
    {
        block = transform.parent.gameObject;
    }

    private void Update()
    {
        if(fireEnemy == null && waterEnemy == null)
        {
            block.GetComponent<BoxCollider>().enabled = false;
        }
    }
}
