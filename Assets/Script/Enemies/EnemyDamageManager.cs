using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageManager : MonoBehaviour
{
    Enemy enemy;

    Color originalColor;
    float flashTime = .10f;

    private void Start()
    {
        enemy = gameObject.GetComponent<Enemy>();

        foreach (Renderer r in GetComponentsInChildren<Renderer>())
        {
             originalColor = r.material.color;
        }
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

        StartCoroutine(EFlash());
    }

    IEnumerator EFlash()
    {
        foreach (Renderer r in GetComponentsInChildren<Renderer>())
        {
            if (gameObject.name == "Fire Enemy" || gameObject.name == "Fire Enemy(Clone)")
            {
                r.material.color = Color.red;
                yield return new WaitForSeconds(flashTime);
                r.material.color = originalColor;
            }
            else if (gameObject.name == "Water Enemy" || gameObject.name == "Water Enemy(Clone)")
            {
                r.material.color = Color.blue;
                yield return new WaitForSeconds(flashTime);
                r.material.color = originalColor;
            }
            
        }

    }

}
