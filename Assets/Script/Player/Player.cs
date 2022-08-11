using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*Si occupa della vita del giocatore, dei danni ricevuti e del Game Over*/
public class Player : MonoBehaviour
{
    public GameObject player;
    float health;                                                                                   //Vita del giocatore
    bool gameOver;                                                                                  //Stato di GameOver
    bool invisibilityFrame;                                                                         //Permette di evitare il danno consecutivo 

    void Start()
    {
        health = 3;
    }

    void Update()
    {
        GameOver(gameOver);                                                                         //Verifica il GameOver
    }

    //Gestione del danno sul giocatore
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Projectile") && invisibilityFrame == false)                //Se puoi prendere danno e entri in contatto con un proiettile
        {
            health--;                                                                               //Prendi danno
            if(health <= 0)                                                                         //Se la vita è 0 o meno
            {
                gameOver = true;                                                                    //Gameover
            }
            StartCoroutine(InvisibiliyyFrame());                                                    //Iniziano gli InvisibilityFrame
        }
    }

    //Funzione di GameOver
    private void GameOver(bool gameOver)
    {
        if (gameOver == true)
        {
            //Messaggio sconfitta/schermata/animazione morte/ecc.
            gameObject.SetActive(false);                                                            //Morte del player
        }
    }

    //Permette di non prendere danno consecutivo
    IEnumerator InvisibiliyyFrame()
    {
        invisibilityFrame = true;                                                                   //Non prendi danno
        yield return new WaitForSeconds(1.5f);
        invisibilityFrame = false;                                                                  //Prendi danno
    }

    #region Singleton

    public static Player instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion
}
