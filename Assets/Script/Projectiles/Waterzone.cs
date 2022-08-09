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
        //Se un nemico rimane in contatto
        if (other.gameObject.CompareTag("Enemy"))
        {
            if(canDamage == true)
            {
                other.gameObject.GetComponent<EnemyDamageManager>().TakeDamage(damage, "water");    //calcola danno
                StartCoroutine(DamageTick());                                                       //Cooldown danno
            }
        }
    }

    IEnumerator DamageTick()
    {
        canDamage = false;
        yield return new WaitForSeconds(0.7f);
        canDamage = true;
    }
}
