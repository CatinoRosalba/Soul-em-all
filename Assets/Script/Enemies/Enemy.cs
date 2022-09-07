using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private static readonly string GameDifficulty = "Difficulty";                           //0 Facile - 1 Normale

    private NavMeshAgent agent;
    private GameObject sfx;
    private AudioSource deathSound;

    public float health;                                                                    //Vita nemico
    public string weak;                                                                     //Debolezza nemico
    private GameObject drop;                                                                //Drop nemico
    private Object explosionRef;                                                            //Animazione morte
    public LineRenderer hook;

    private void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();

        sfx = GameObject.Find("SFX");
        deathSound = sfx.transform.Find("SFX - Enemy Dies").GetComponent<AudioSource>();

        explosionRef = Resources.Load("Particles/Explosion");

        if (gameObject.name.Contains("Fire"))          //Se il nome del nemico è Fire Enemy
        {
            weak = "water";                                                                     //Setti debolezza
            drop = Resources.Load<GameObject>("Gems/FireGem");                                  //Setti drop
            health = 3;
            if (PlayerPrefs.GetInt(GameDifficulty) == 0)
            {
                agent.speed = 15;
            } else if(PlayerPrefs.GetInt(GameDifficulty) == 1)
            {
                agent.speed = 20;
            }
        } 
        else if(gameObject.name.Contains("Water"))    //Se il nome del nemico è Water Enemy
        {
            weak = "fire";                                                                      //Setti debolezza
            drop = Resources.Load<GameObject>("Gems/WaterGem");                                 //Setti drop
            health = 3;
            if (PlayerPrefs.GetInt(GameDifficulty) == 0)
            {
                agent.speed = 7.5f;
            }
            else if (PlayerPrefs.GetInt(GameDifficulty) == 1)
            {
                agent.speed = 10;   
            }
        }
    }

    private void Update()
    {
        if (health <= 0)                                                                        //Se la vita è uguale o minore di 0
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
        if(PlayerPrefs.GetInt(GameDifficulty) == 0)
        {
            float dropRate = 0.2f;
            for (int i = player.health; i < player.maxHealth; i++)
            {
                dropRate += 0.1f;
            }
            if (Random.value <= dropRate)
            {
                Instantiate(Resources.Load("Vita"), gameObject.transform.position + new Vector3(0, 0, -2), Quaternion.identity);
            }
        } else if (PlayerPrefs.GetInt(GameDifficulty) == 1)
        {
            float dropRate = 0.1f;
            for (int i = player.health; i < player.maxHealth; i++)
            {
                dropRate += 0.05f;
            }
            if (Random.value <= dropRate)
            {
                Instantiate(Resources.Load("Vita"), gameObject.transform.position + new Vector3(0, 0, -2), Quaternion.identity);
            }
        }
    }
}
