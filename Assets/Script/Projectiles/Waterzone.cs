using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waterzone : MonoBehaviour
{
    float damage;                                           //Danno zona
    bool canDamage;                                         //Variabile usata per il tick di danno

    private void Start()
    {
        damage = 0.2f;
        canDamage = true;
    }

    private void Update()
    {
        Destroy(gameObject, 7);                             //Dstruggi la zona dopo 5 secondi
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))                                                   //Se un nemico rimane in contatto
        {
            if(canDamage == true)                                                                   //Se può far danno
            {
                other.gameObject.GetComponent<EnemyDamageManager>().TakeDamage(damage, "water");    //calcola danno
                StartCoroutine(DamageCooldown());
            }
        }
    }

    IEnumerator DamageCooldown()
    {
        canDamage = false;
        yield return new WaitForSeconds(0.6f);
        canDamage = true;
    }
}
