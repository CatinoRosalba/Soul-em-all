using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageManager : MonoBehaviour
{
    Enemy enemy;

    private void Start()
    {
        enemy = gameObject.GetComponent<Enemy>();
    }

    public void TakeDamage(float damage, string type)
    {
        if (type == enemy.weak)
        {
            enemy.health -= damage * 2f;
            Debug.Log(enemy.health);
        }
        else
        {
            enemy.health -= damage;
        }
    }
}
