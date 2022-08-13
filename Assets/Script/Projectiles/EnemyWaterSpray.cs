using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaterSpray : MonoBehaviour
{
    private Rigidbody rbBullet;                                                      //RigidBody
    float speed;                                                                     //Velocità proiettile
    public float damage;                                                             //Danno proiettile
    [SerializeField] GameObject waterZone;                                           //Zona d'acqua rilasciata alla distruzione
    AudioSource audioShot;

    private void Awake()
    {
        rbBullet = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        audioShot = GetComponent<AudioSource>();
        audioShot.PlayOneShot(audioShot.clip);
        speed = 70f;
        damage = 0.8f;
        rbBullet.velocity = transform.forward * speed;                                //Muove il proiettile
        Destroy(gameObject, 2);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Default"))
        {
            Destroy(gameObject);                                                          //Distrugge il proiettile
        }
    }
}
