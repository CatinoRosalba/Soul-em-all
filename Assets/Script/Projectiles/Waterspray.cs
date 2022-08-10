using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waterspray : MonoBehaviour
{
    private Rigidbody rbBullet;                                                         //RigidBody
    float speed;                                                                        //Velocità proiettile
    public float damage;                                                                //Danno proiettile
    [SerializeField] GameObject waterZone;                                              //Zona d'acqua rilasciata alla distruzione
    AudioSource audioShot;

    private void Awake()
    {
        rbBullet = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        audioShot = GetComponent<AudioSource>();
        audioShot.PlayOneShot(audioShot.clip);
        speed = 30f;
        damage = 0.8f;
        rbBullet.velocity = transform.forward * speed;                                //Muove il proiettile
        Destroy(gameObject, 1f);                                                      //Distrugge il proiettile dopo mezzo secondo
    }

    private void OnTriggerEnter(Collider other)
    {
        //Se incontra un entità Enemy
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyDamageManager>().TakeDamage(damage, "water");        //Applica danno
            Destroy(gameObject);                                                                    //Distrugge il proiettile
            Vector3 enemyPosition = new Vector3(other.gameObject.transform.position.x, 0.2f, other.gameObject.transform.position.z);    
            Instantiate(waterZone,enemyPosition, Quaternion.Euler(90f, 0f, 0f));                    //Rilascia la waterzone nella posizione del nemico
        }
    }
}
