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
        speed = 80f;
        rbBullet.velocity = transform.forward * speed;                                //Muove il proiettile
        Destroy(gameObject, 2);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (!other.isTrigger && !other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log(other);
            Destroy(gameObject);                                                         //Distrugge il proiettile
        }
    }
}
