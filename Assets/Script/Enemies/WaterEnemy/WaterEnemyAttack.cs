using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaterEnemyAttack : MonoBehaviour
{
    private static readonly string GameDifficulty = "Difficulty";                           //0 Facile - 1 Normale

    private GameObject sfx;
    private AudioSource attackSound;

    public GameObject bulletSpawnPoint;                                //Punto di spawn proiettili
    private GameObject player;                                         //Giocatore
    public bool canAttack;                                            //Usata per il cooldown dell'attacco
    private bool canChangeDirAim;                                      //Usata per il cooldown di cambio direzione sparo
    private Vector3 aimDir;                                            //Direzione di mira

    void Start()
    {
        sfx = GameObject.Find("SFX");
        attackSound = sfx.transform.Find("SFX - Water Enemy Attack").GetComponent<AudioSource>();

        player = GameObject.Find("Amnery");
        canChangeDirAim = false;
        canAttack = false;
        StartCoroutine(AttackCooldown());
    }

    private void Update()
    {
        if(canAttack == true)                                                                       //Se può attaccare
        {
            if(canChangeDirAim == true)                                                             //Se può cambiare direzione sparo (fatto per non chiamare tante volte questo metodo ma solo la coroutine dello sparo)
            {
                AimDirection();                                                                     //Cambia direzione mira
            }
            StartCoroutine(Attack());                                                               //Attacca
            if (!attackSound.isPlaying)
            {
                attackSound.Play();
            }
        }
    }

    //Cambia direzione mira
    private void AimDirection()
    {
        Vector3 playerPositionMax = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z + 2.5f);      //Sparo leggermente a destra
        Vector3 playerPositionMin = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z - 2.5f);      //Sparo leggermente a sinistra
        int randomTarget = Random.Range(1, 4);                                                      //Scelta randomica della direzione dello sparo (da 1 a 4-1=3
        switch (randomTarget)
        {
            case 1:
                aimDir = (playerPositionMax - gameObject.transform.position).normalized;            //Setta leggermente destra
                break;

            case 2:
                aimDir = (playerPositionMin - gameObject.transform.position).normalized;            //Setta leggermente sinistra
                break;
            case 3:
                aimDir = (player.transform.position - gameObject.transform.position).normalized;    //Setta centrale
                break;
        }
        canChangeDirAim = false;                                                                    //Non cambiare direzione mira
    }

    //Spara tanti proiettili in base a quante volte viene chiamata
    IEnumerator Attack()
    {
        yield return new WaitForSeconds(0.2f);
        Instantiate(Resources.Load("Projectiles/EnemyWaterSpray"), bulletSpawnPoint.transform.position, Quaternion.LookRotation(aimDir, Vector3.up));   //Spara tanti proiettili in base a quante volte è stata chiamat la coroutine in 0.2 secondi dal cooldown dello sparo nella direzione scelta
        StartCoroutine(AttackCooldown());                                                           //Cooldown sparo
    }

    //Cooldown sparo
    IEnumerator AttackCooldown()
    {
        if(PlayerPrefs.GetInt(GameDifficulty) == 0)
        {
            canAttack = false;
            yield return new WaitForSeconds(3.5f);
            canAttack = true;
        } else if(PlayerPrefs.GetInt(GameDifficulty) == 1){
            canAttack = false;
            yield return new WaitForSeconds(2f);
            canAttack = true;
        }
        canChangeDirAim = true;
    }
}
