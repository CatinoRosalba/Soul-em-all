using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    Rigidbody rbBullet;                                                         //RigidBody
    float speed;                                                                //Velocità Proiettile
    public float damage;                                                        //Danno Proiettile
    //public AudioSource audioShot;                                               //Audio sparo

    void Start()
    {
        rbBullet = GetComponent<Rigidbody>();
        //audioShot.Play();
        speed = 3250f;
        damage = 1;
        rbBullet.AddForce(transform.forward * speed * Time.fixedDeltaTime, ForceMode.Impulse);        //Muove il proiettile
        Destroy(gameObject, 2);                                                 //Distrugge il proiettile dopo 2 secondi
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player") && !other.isTrigger)                             //Se ha tag Enemy
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                other.gameObject.GetComponent<EnemyDamageManager>().TakeDamage(damage, "fire");     //Applica Danno
            }
            Destroy(gameObject);                                                                //Distruggi Proiettile
        }
    }

}
