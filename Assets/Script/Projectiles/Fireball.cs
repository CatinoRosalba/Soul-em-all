using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    private Rigidbody rbBullet;
    float speed;
    public float damage;


    private void Awake()
    {
        rbBullet = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        speed = 40f;
        damage = 1;
        rbBullet.velocity = transform.forward * speed;
        Destroy(gameObject, 2); //Distrugge il proiettile dopo 5 secondi
    }

    private void OnTriggerEnter(Collider other)
    {
        //Distrugge il proiettile se entra in contatto con un nemico
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
