using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    public GameObject enemy;
    public bool setAttack;
    public bool setMovement;
    public bool setVision;
    private GameObject clone;

    public void SpawnEnemy()
    {
        clone = Instantiate(enemy, gameObject.transform.position, Quaternion.identity);
        if (clone.name.Contains("Fire"))
        {
            clone.GetComponent<FireEnemyAttack>().enabled = setAttack;
            clone.GetComponent<FireEnemyMovement>().enabled = setMovement;
            clone.GetComponent<FireEnemyVision>().enabled = setVision;
        } else if (clone.name.Contains("Water"))
        {
            clone.GetComponent<WaterEnemyAttack>().enabled = setAttack;
            clone.GetComponent<WaterEnemyMovement>().enabled = setMovement;
            clone.GetComponent<WaterEnemyVision>().enabled = setVision;
        }
    }
}
