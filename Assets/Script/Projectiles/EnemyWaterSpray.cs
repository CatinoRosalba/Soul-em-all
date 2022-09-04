using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaterSpray : MonoBehaviour
{
    private Rigidbody rbBullet;                                                      //RigidBody
    float speed;                                                                     //Velocità proiettile                                 

    private void Awake()
    {
        rbBullet = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        speed = 4000;
        rbBullet.velocity = transform.forward * speed * Time.fixedDeltaTime;                                //Muove il proiettile
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
