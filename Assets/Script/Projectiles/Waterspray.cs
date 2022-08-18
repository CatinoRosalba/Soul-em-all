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
        if (other.gameObject.CompareTag("Enemy"))                                                   //Se ha tag Enemy
        {
            other.gameObject.GetComponent<EnemyDamageManager>().TakeDamage(damage, "water");        //Applica danno
            Destroy(gameObject);                                                                    //Distrugge il proiettile
            Vector3 enemyPosition = new Vector3(other.gameObject.transform.position.x, 0.2f, other.gameObject.transform.position.z);
            Instantiate(waterZone,enemyPosition, Quaternion.Euler(90f, 0f, 0f));                    //Rilascia la waterzone nella posizione del nemico
        } else if (other.gameObject.CompareTag("Enemy") == false && other.gameObject.CompareTag("Category") == false) //Se non ha tag Enemy o Category
        {
            Destroy(gameObject);
            Vector3 projectilePosition = new Vector3(gameObject.transform.position.x, 0.2f, gameObject.transform.position.z);
            Instantiate(waterZone, projectilePosition, Quaternion.Euler(90f, 0f, 0f));              //Rilascia la waterzone nella posizione del proiettile
        }
    }
}
