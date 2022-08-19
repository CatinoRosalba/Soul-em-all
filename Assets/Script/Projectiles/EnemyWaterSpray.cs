using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaterSpray : MonoBehaviour
{
    private Rigidbody rbBullet;                                                      //RigidBody
    float speed;                                                                     //Velocità proiettile
    public float damage;                                                             //Danno proiettile
    [SerializeField] GameObject waterZone;                                           //Zona d'acqua rilasciata alla distruzione                                        

    private void Awake()
    {
        rbBullet = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        speed = 80f;
        damage = 0.8f;
        rbBullet.velocity = transform.forward * speed;                                //Muove il proiettile
        Destroy(gameObject, 2);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (!other.isTrigger && !other.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);                                                         //Distrugge il proiettile
        }
    }
}
