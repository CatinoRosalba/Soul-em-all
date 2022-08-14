using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waterzone : MonoBehaviour
{
    float damage;                                           //Danno zona
    bool canDamage;                                         //Variabile usata per il tick di danno

    private void Start()
    {
        damage = 0.1f;
        canDamage = true;
    }

    private void Update()
    {
        Destroy(gameObject, 5);                             //Dstruggi la zona dopo 5 secondi
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))                                                   //Se un nemico rimane in contatto
        {
            if(canDamage == true)                                                                   //Se può far danno
            {
                other.gameObject.GetComponent<EnemyDamageManager>().TakeDamage(damage, "water");    //calcola danno
            }
        }
    }
}
