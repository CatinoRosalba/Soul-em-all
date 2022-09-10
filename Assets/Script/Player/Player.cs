using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*Si occupa della vita del giocatore, dei danni ricevuti e del Game Over*/
public class Player : MonoBehaviour
{
    private GameObject sfx;
    private AudioSource damageSound;
    private AudioSource deathSound;
    private AudioSource gameOverSound;

    public GameObject[] goHealth;                                                                   //Oggetto vita del giocatore
    private SpriteRenderer spriteRenderer;
    public int maxHealth;
    public int health;                                                                              //Vita del giocatore
    [SerializeField] GameOverController gameOverController;
    private bool gameOver;                                                                          //Stato di GameOver
    private bool invisibilityFrame;                                                                 //Permette di evitare il danno consecutivo
    private bool invFrameActive;

    private Material matDefault;                                                                    //Materiale di default
    private Material matWhite;                                                                      //Material di colore bianco
    private float flashTime = .10f;                                                                 //Tempo del flash
    private Object explosionRef;                                                                    //Animazione morte

    void Start()
    {
        invFrameActive = false;
        sfx = GameObject.Find("SFX");
        damageSound = sfx.transform.Find("SFX - Player Takes Damage").GetComponent<AudioSource>();
        deathSound = sfx.transform.Find("SFX - Player Dies").GetComponent<AudioSource>();
        gameOverSound = sfx.transform.Find("SFX - Game Over").GetComponent<AudioSource>();

        maxHealth = 5;
        health = maxHealth;
        spriteRenderer = transform.Find("Sprite").GetComponent<SpriteRenderer>();

        matWhite = Resources.Load("Particles/FlashWhite", typeof(Material)) as Material;
        explosionRef = Resources.Load("Particles/Explosion");

        matDefault = spriteRenderer.material;
    }

    void Update()
    {
        GameOver(gameOver);                                                                         //Verifica il GameOver
        if(invisibilityFrame && invFrameActive)
        {
            StartCoroutine(InvisibilityFrameEffect());
        }
    }

    //Gestione del danno sul giocatore da proiettile
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EnemyProjectile") && invisibilityFrame == false)           //Se puoi prendere danno e entri in contatto con un proiettile
        {
            health--;                                                                               //Prendi danno
            damageSound.Play();
            goHealth[health].gameObject.SetActive(false);                                           //Disattivo lo sprite della vita
            StartCoroutine(EFlash());                                                               //Flash del danno
            if (health <= 0)                                                                        //Se la vita è 0 o meno
            {
                GameObject explosion = (GameObject)Instantiate(explosionRef);                       //Esplosione
                ParticleSystem.MainModule setColor = explosion.GetComponent<ParticleSystem>().main;
                deathSound.Play();
                setColor.startColor = new Color(80 / 255f, 18 / 255f, 88 / 255f);                    //Cambia colore delle particelle
                explosion.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                explosion.transform.rotation = gameObject.transform.rotation;

                gameOver = true;                                                                    //Gameover                                                                    //Gameover
            }
            StartCoroutine(InvisibiliyyFrame());                                                    //Iniziano gli InvisibilityFrame
        }
    }

    //Gestione del danno sul giocatore da nemico
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && invisibilityFrame == false)                //Se puoi prendere danno e entri in contatto con un nemico
        {
            health--;                                                                               //Prendi danno
            damageSound.Play();
            gameObject.GetComponent<Rigidbody>().AddForce(Vector3.down * (-500) * Time.fixedDeltaTime, ForceMode.Impulse); //Contraccolpo
            StartCoroutine(EFlash());                                                               //Flash del danno
            goHealth[health].gameObject.SetActive(false);                                             //Disattivo lo sprite della vita
            if (health <= 0)                                                                        //Se la vita è 0 o meno
            {
                GameObject explosion = (GameObject) Instantiate(explosionRef);                       //Esplosione
                ParticleSystem.MainModule setColor = explosion.GetComponent<ParticleSystem>().main;
                deathSound.Play();
                setColor.startColor = new Color(80 / 255f, 18 / 255f, 88 / 255f);                    //Cambia colore delle particelle
                explosion.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                explosion.transform.rotation = gameObject.transform.rotation;

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
            gameOverSound.Play();
            gameOverController.SetUpGameOver();                                                     //Attivo schermata gameover
            gameObject.SetActive(false);                                                            //Morte del player
        }
    }

    //Permette di non prendere danno consecutivo
    IEnumerator InvisibiliyyFrame()
    {
        invisibilityFrame = true;                                                                   //Non prendi danno
        invFrameActive = true;
        yield return new WaitForSeconds(2);
        invisibilityFrame = false;                                                                  //Prendi danno
        invFrameActive = false;
    }

    IEnumerator InvisibilityFrameEffect()
    {
        invFrameActive = false;
        yield return new WaitForSeconds(0.25f);
        spriteRenderer.enabled = false;
        yield return new WaitForSeconds(0.25f);
        spriteRenderer.enabled = true;
        invFrameActive = true;
    }

    //Timer del flash
    IEnumerator EFlash()
    {
        spriteRenderer.material = matWhite;
        yield return new WaitForSeconds(flashTime);
        spriteRenderer.material = matDefault;
    }
}
