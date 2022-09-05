using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameObject sfx;
    private AudioSource deathSound;

    public float health;                                                                    //Vita nemico
    public string weak;                                                                     //Debolezza nemico
    private GameObject drop;                                                                //Drop nemico
    private Object explosionRef;                                                            //Animazione morte
    public LineRenderer hook;

    private void Start()
    {
        sfx = GameObject.Find("SFX");
        deathSound = sfx.transform.Find("SFX - Enemy Dies").GetComponent<AudioSource>();

        explosionRef = Resources.Load("Particles/Explosion");

        if (gameObject.name.Contains("Fire"))          //Se il nome del nemico � Fire Enemy
        {
            health = 3;                                                                         //Setti vita
            weak = "water";                                                                     //Setti debolezza
            drop = Resources.Load<GameObject>("Gems/FireGem");                                  //Setti drop
        } 
        else if(gameObject.name.Contains("Water"))    //Se il nome del nemico � Water Enemy
        {
            health = 3;                                                                         //Setti vita
            weak = "fire";                                                                      //Setti debolezza
            drop = Resources.Load<GameObject>("Gems/WaterGem");                                 //Setti drop
        }
    }

    private void Update()
    {
        if (health <= 0)                                                                        //Se la vita � uguale o minore di 0
        {
            Destroy(gameObject);                                                                //Distruggi nemico
            GameObject clone = Instantiate(drop, gameObject.transform.position, Quaternion.identity);              //Istanzia Drop
            clone.GetComponent<GemScript>().canDespawn = true;
            DropHealth();

            deathSound.Play();
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
