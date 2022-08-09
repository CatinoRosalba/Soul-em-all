using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health;
    public string weak;
    public GameObject drop;

    private Object explosionRef;

    private void Start()
    {
        explosionRef = Resources.Load("Explosion");

        if (gameObject.name == "Fire Enemy" || gameObject.name == "Fire Enemy(Clone)")
        {
            health = 3;
            weak = "water";
            drop = Resources.Load<GameObject>("FireGem");
        } 
        else if(gameObject.name == "Water Enemy" || gameObject.name == "Water Enemy(Clone)")
        {
            health = 5;
            weak = "fire";
            drop = Resources.Load<GameObject>("Assets / Prefab / WaterGem.prefab");
        }
    }

    private void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
            Instantiate(drop, gameObject.transform.position, Quaternion.identity);

            GameObject explosion = (GameObject)Instantiate(explosionRef);
            explosion.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            explosion.transform.rotation = gameObject.transform.rotation;
        }
    }
}
