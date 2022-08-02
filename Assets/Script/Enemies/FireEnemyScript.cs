using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEnemyScript : MonoBehaviour
{

    float health;
    [SerializeField] GameObject fireAmmo;

    private void Start()
    {
        health = 3;
    }

    private void OnTriggerEnter(Collider other)
    {

        //Danno
        if (other.gameObject.CompareTag("Projectile"))
        {
            health--;
        }
        //Morte
        if (health <= 0)
        {
            Destroy(gameObject);
            Instantiate(fireAmmo, gameObject.transform.position, Quaternion.identity);
        }

    }

}
