using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health;                                                                    //Vita nemico
    public string weak;                                                                     //Debolezza nemico
    private GameObject drop;                                                                //Drop nemico
    private Object explosionRef;                                                            //Animazione morte
    public LineRenderer hook;

    private void Start()
    {
        explosionRef = Resources.Load("Particles/Explosion");

        if (gameObject.name.Contains("Fire"))          //Se il nome del nemico è Fire Enemy
        {
            health = 3;                                                                         //Setti vita
            weak = "water";                                                                     //Setti debolezza
            drop = Resources.Load<GameObject>("Gems/FireGem");                                  //Setti drop
        } 
        else if(gameObject.name.Contains("Water"))    //Se il nome del nemico è Water Enemy
        {
            health = 3;                                                                         //Setti vita
            weak = "fire";                                                                      //Setti debolezza
            drop = Resources.Load<GameObject>("Gems/WaterGem");                                 //Setti drop
        }
    }

    private void Update()
    {
        if (health <= 0)                                                                        //Se la vita è uguale o minore di 0
        {
            Destroy(gameObject);                                                                //Distruggi nemico
            Instantiate(drop, gameObject.transform.position, Quaternion.identity);              //Istanzia Drop
            DropHealth();

            GameObject explosion = (GameObject)Instantiate(explosionRef);                       //Esplosione
            explosion.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            explosion.transform.rotation = gameObject.transform.rotation;
        }
    }

    private void DropHealth()
    {
        Player player = FindObjectOfType<Player>();
        float dropRate = 0.1f;
        for(int i = player.health; i < player.maxHealth; i++)
        {
            dropRate += 0.05f;
        }
        if (Random.value <= dropRate)
        {
            Instantiate(Resources.Load("Vita"), gameObject.transform.position + new Vector3(0,0,-2), Quaternion.identity);
        }
    }
}
