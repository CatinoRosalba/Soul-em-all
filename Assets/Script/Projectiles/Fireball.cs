using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    Rigidbody rbBullet;                                                         //RigidBody
    float speed;                                                                //Velocità Proiettile
    public float damage;                                                        //Danno Proiettile
    AudioSource audioShot;                                                      //Audio sparo

    void Start()
    {
        rbBullet = GetComponent<Rigidbody>();
        audioShot = GetComponent<AudioSource>();
        audioShot.PlayOneShot(audioShot.clip);
        speed = 65f;
        damage = 1;
        rbBullet.AddForce(transform.forward * speed, ForceMode.Impulse);        //Muove il proiettile
        Destroy(gameObject, 2);                                                 //Distrugge il proiettile dopo 2 secondi
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))                                               //Se il proiettile entra in contatto con un nemico
        {
            other.gameObject.GetComponent<EnemyDamageManager>().TakeDamage(damage, "fire");     //Applica Danno
            Destroy(gameObject);                                                                //Distruggi Proiettile
        }
    }

}
