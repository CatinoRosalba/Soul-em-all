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
    }
    private void Update()
    {
        rbBullet.velocity = transform.forward * speed;
        Destroy(gameObject, 0.5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Distrugge il proiettile se entra in contatto con un nemico
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            Vector3 enemyPosition = new Vector3(other.gameObject.transform.position.x, 0.2f, other.gameObject.transform.position.z);
            Instantiate(waterZone,enemyPosition, Quaternion.Euler(90f, 0f, 0f));
        }
    }
}
