using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    Rigidbody rbBullet;
    float speed;
    public float damage;

    void Start()
    {
        rbBullet = GetComponent<Rigidbody>();
        speed = 40f;
        damage = 1;
        rbBullet.velocity = transform.forward * speed;
        Destroy(gameObject, 2); //Distrugge il proiettile dopo 5 secondi
    }

    void OnTriggerEnter(Collider other)
    {
        //Distrugge il proiettile se entra in contatto con un nemico
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyDamageManager>().TakeDamage(damage, "fire");
            Destroy(gameObject);
        }
    }

}
