using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FireEnemyAttack : MonoBehaviour
{
    private static readonly string GameDifficulty = "Difficulty";                           //0 Facile - 1 Normale

    private GameObject sfx;
    private AudioSource chargeSound;
    private AudioSource attackSound;

    private FireEnemyMovement mov;                                                                  //Scirpt che gestisce il movimento del FireEnemy
    private Rigidbody rb;                                                                           //Rigidbody

    private bool canAttack;                                                                         //Usato per controllare se pu� attaccare
    private bool canBrake;                                                                          //Usato per controllare se pu� frenare lo scatto

    void Start()
    {
        sfx = GameObject.Find("SFX");
        chargeSound = sfx.transform.Find("SFX - Fire Enemy Charge").GetComponent<AudioSource>();
        attackSound = sfx.transform.Find("SFX - Fire Enemy Attack").GetComponent<AudioSource>();

        rb = GetComponent<Rigidbody>();
        mov = GetComponent<FireEnemyMovement>();
        canAttack = false;
        StartCoroutine(AttackCooldown());
    }

    private void FixedUpdate()
    {
        if (mov.distance <= mov.agent.stoppingDistance && canAttack == true)                                //Se si trova nel range di stop e pu� attaccare
        {
            StartCoroutine(Melee());                                                                    //Scatta sul giocatore
        }
        if(canBrake == true)                                                                            //Se pu� frenare
        {
            StartCoroutine(Brake());                                                                    //Frena
        }
        else                                                                                            //Se non deve frenare
        {
            rb.drag = 1;                                                                                //Resetta il drag
        }
    }

    //Attacco scatto
    IEnumerator Melee()
    {
        canAttack = false;                                                                              //Resetta la possibilit� di poter attaccare
        mov.agent.enabled = false;                                                                      //Disattiva il NavMeshAgent per evitare che entri in conflitto con la fisica
        chargeSound.Play();

        if (PlayerPrefs.GetInt(GameDifficulty) == 0)
        {
            yield return new WaitForSeconds(1.5f);
            Vector3 direction = mov.player.transform.position - gameObject.transform.position;              //Calcola la direzione tra il giocatore e l'entit�
            rb.AddForce(direction * 275f * Time.fixedDeltaTime, ForceMode.Impulse);                                               //Scatta
        }
        else if(PlayerPrefs.GetInt(GameDifficulty) == 1)
        {
            yield return new WaitForSeconds(0.7f);
            Vector3 direction = mov.player.transform.position - gameObject.transform.position;              //Calcola la direzione tra il giocatore e l'entit�
            rb.AddForce(direction * 375f * Time.fixedDeltaTime, ForceMode.Impulse);                                               //Scatta
        }
        attackSound.Play();
        canBrake = true;                                                                                //Abilit� il freno

        yield return new WaitForSeconds(0.7f);
        mov.agent.enabled = true;                                                                       //Riattiva il NavMeshAgent
        canBrake = false;                                                                               //Disattiva la frenata
        StartCoroutine(AttackCooldown());                                                               //Attiva il cooldown dell'attacco
    }

    //Frenata
    IEnumerator Brake()
    {
        float initialDistance = Vector3.Distance(mov.player.transform.position, gameObject.transform.position);     //Primo calcolo della distanza tra giocatore e entit�
        yield return 0; 
        float secondDistance = Vector3.Distance(mov.player.transform.position, gameObject.transform.position);      //Secondo calcolo della distanza tra giocatore e entit�
        if (secondDistance > initialDistance)                                                                   //Se la seconda distanza � maggiore della prima (si sta allontanando dal giocatore ovvero lo ha mancato)
        {
            rb.drag = 8;                                                                                        //Aumenta il drag per frenare
        }
    }
    
    //Cooldown attacco
    IEnumerator AttackCooldown()
    {
        if (PlayerPrefs.GetInt(GameDifficulty) == 0)
        {
            yield return new WaitForSeconds(4.5f);
        }
        else if (PlayerPrefs.GetInt(GameDifficulty) == 1)
        {
            yield return new WaitForSeconds(3);
        }
        canAttack = true;
    }
}
