using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waterzone : MonoBehaviour
{
    float damage;

    private void Start()
    {
        damage = 0.1f;
    }

    private void Update()
    {
        Destroy(gameObject, 5);
    }

    private void OnTriggerStay(Collider other)
    {
        //Distrugge il proiettile se entra in contatto con un nemico
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyDamageManager>().TakeDamage(damage, "water");    //Mettere timer di danno
        }
    }
}
