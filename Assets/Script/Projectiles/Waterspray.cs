using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waterspray : MonoBehaviour
{
    private Rigidbody rbBullet;
    float speed;
    public float damage;
    [SerializeField] GameObject waterZone;

    private void Awake()
    {
        rbBullet = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        speed = 30f;
        damage = 0.2f;
        rbBullet.velocity = transform.forward * speed;
        Destroy(gameObject, 0.5f); //Distrugge il proiettile dopo 1.5 secondi
    }

    private void OnTriggerEnter(Collider other)
    {
        //Distrugge il proiettile se entra in contatto con un nemico
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        Instantiate(waterZone, gameObject.transform.position, Quaternion.identity);
    }
}
