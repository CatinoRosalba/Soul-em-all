using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waterspray : MonoBehaviour
{
    private GameObject sfx;
    private AudioSource attackSound;

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
        sfx = GameObject.Find("SFX");
        attackSound = sfx.transform.Find("SFX - Player Water Shot").GetComponent<AudioSource>();

        audioShot = GetComponent<AudioSource>();
        audioShot.PlayOneShot(audioShot.clip);
        speed = 875f;
        damage = 0.8f;
        rbBullet.AddForce(transform.forward * speed * Time.fixedDeltaTime, ForceMode.Impulse);                                  //Muove il proiettile
        attackSound.Play();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player") && !other.isTrigger)                             //Se ha tag Enemy
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                other.gameObject.GetComponent<EnemyDamageManager>().TakeDamage(damage, "water");    //Applica danno
            }
            Vector3 projectilePosition = new Vector3(gameObject.transform.position.x, CalculateFloorPoint() + 0.1f, gameObject.transform.position.z);
            Instantiate(waterZone, projectilePosition, Quaternion.Euler(90f, 0f, 0f));              //Rilascia la waterzone nella posizione del proiettile
            Destroy(gameObject);                                                                    //Distrugge il proiettile
        } 
    }

    private float CalculateFloorPoint()
    {
        RaycastHit hit;
        float distance = 1f;
        Vector3 dir = new Vector3(0, -1);

        Physics.Raycast(transform.position, dir, out hit, distance);
        return hit.point.y;
    }
}
