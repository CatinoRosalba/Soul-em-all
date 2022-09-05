using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeScript : MonoBehaviour
{
    private GameObject sfx;
    private AudioSource healSound;

    private void Start()
    {
        sfx = GameObject.Find("SFX");
        healSound = sfx.transform.Find("SFX - Heal").GetComponent<AudioSource>();
    }

    private void Update()
    {
        Destroy(gameObject, 10);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")){
            Heal(other.gameObject.GetComponent<Player>());
            healSound.Play();
        }
    }

    private void Heal(Player player)
    {
        if(player.health < player.maxHealth){
            player.health++;
            player.goHealth[player.health-1].SetActive(true);
            Destroy(gameObject);
        }
    }
}
