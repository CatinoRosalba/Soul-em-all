using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeScript : MonoBehaviour
{
    private void Update()
    {
        Destroy(gameObject, 10);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")){
            Heal(other.gameObject.GetComponent<Player>());
        }
    }

    private void Heal(Player player)
    {
        if(player.health < player.maxHealth){
            player.health++;
            Destroy(gameObject);
        }
    }
}
