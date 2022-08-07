using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float health;
    bool gameOver;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        health = 3;
    }

    void Update()
    {
        if(gameOver == true)
        {
            //Messaggio sconfitta/schermata/animazione morte/ecc.
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Projectile"))
        {
            health--;
            if(health <= 0)
            {
                gameOver = true;
            }
        }
    }
}
