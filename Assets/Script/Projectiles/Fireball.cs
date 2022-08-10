using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    Rigidbody rbBullet;                                                         //RigidBody
    float speed;                                                                //Velocit� Proiettile
    public float damage;                                                        //Danno Proiettile
    AudioSource audioShot;

    void Start()
    {
        rbBullet = GetComponent<Rigidbody>();
        audioShot = GetComponent<AudioSource>();
        audioShot.PlayOneShot(audioShot.clip);
        speed = 40f;
        damage = 1;
        rbBullet.velocity = transform.forward * speed;                          //Muove il proiettile
        Destroy(gameObject, 2);                                                 //Distrugge il proiettile dopo 2 secondi
    }

    void OnTriggerEnter(Collider other)
    {
        //Se il proiettile entra in contatto con un nemico
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyDamageManager>().TakeDamage(damage, "fire");     //Applica Danno
            Destroy(gameObject);                                                                //Distruggi Proiettile
        }
    }

}
