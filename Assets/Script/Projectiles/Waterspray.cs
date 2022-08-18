using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waterspray : MonoBehaviour
{
    private Rigidbody rbBullet;                                                         //RigidBody
    float speed;                                                                        //Velocità proiettile
    public float damage;                                                                //Danno proiettile
    [SerializeField] GameObject waterZone;                                              //Zona d'acqua rilasciata alla distruzione
    AudioSource audioShot;                                                              //Audio sparo

    private void Awake()
    {
        rbBullet = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        audioShot = GetComponent<AudioSource>();
        audioShot.PlayOneShot(audioShot.clip);
        speed = 12.5f;
        damage = 0.8f;
        rbBullet.AddForce(transform.forward * speed, ForceMode.Impulse);                                  //Muove il proiettile
    }


    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player") && !other.isTrigger)                             //Se ha tag Enemy
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                other.gameObject.GetComponent<EnemyDamageManager>().TakeDamage(damage, "water");    //Applica danno
            }
            Vector3 projectilePosition = new Vector3(gameObject.transform.position.x, 0.2f, gameObject.transform.position.z);
            Instantiate(waterZone, projectilePosition, Quaternion.Euler(90f, 0f, 0f));              //Rilascia la waterzone nella posizione del proiettile
            Destroy(gameObject);                                                                    //Distrugge il proiettile
        } 
    }
}
