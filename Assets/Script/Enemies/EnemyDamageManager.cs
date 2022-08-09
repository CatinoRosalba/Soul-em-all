using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageManager : MonoBehaviour
{
    Enemy enemy;

    Material matDefault;
    Material matWhite;
    float flashTime = .10f;

    private void Start()
    {
        enemy = gameObject.GetComponent<Enemy>();
        matWhite = Resources.Load("FlashWhite", typeof(Material)) as Material;
        
        foreach (Renderer r in GetComponentsInChildren<Renderer>())
        {
            matDefault = r.material;
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
            r.material = matWhite;
            yield return new WaitForSeconds(flashTime);
            r.material = matDefault;
        }

    }

}
