using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Victory : MonoBehaviour
{
    private GameObject sfx;
    public GameObject uiFinishDemo;
    public GameObject cameraObj;
    private AudioSource victorySound;

    public static bool isVictory = false;

    private static readonly string LastLevel = "LastLevel";

    [SerializeField] LevelChanger transition;
    [SerializeField] string nextLevel;

    private int indexLevel;

    private void Start()
    {
        isVictory = false;
        sfx = GameObject.Find("SFX");
        victorySound = sfx.transform.Find("SFX - Victory").GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        SaveProgress();
        victorySound.Play();

        //Se collide con il giocatore e non siamo all'ultimo livello
        if (other.CompareTag("Player") && !Equals(SceneManager.GetActiveScene(), SceneManager.GetSceneByName("Level_3")))
        {
            transition.FadeAndChangeToLevel(nextLevel);                     //Passa al prossimo livello
            isVictory = false;
        } 
        else if(other.CompareTag("Player"))
        {                                                                   //Visualizza schermata di fine demo
            cameraObj = GameObject.Find("Third Person Camera");
            cameraObj.GetComponent<FreeLookAxisDriver>().enabled = false;

            uiFinishDemo.SetActive(true);

            Time.timeScale = 0;

            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;

            isVictory = true;
        }
    }

    public void BackToMainMenu()
    {
        isVictory = false;

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        cameraObj = GameObject.Find("Third Person Camera");
        cameraObj.GetComponent<FreeLookAxisDriver>().enabled = true;
        FindObjectOfType<LevelChanger>().FadeAndChangeToLevel("MainMenu");
        
        Time.timeScale = 1;
    }

    private void SaveProgress()
    {
        indexLevel = SceneManager.GetActiveScene().buildIndex;              //Salvo l'index del livello appena concluso

        if(indexLevel > PlayerPrefs.GetInt(LastLevel))                      //se il livello concluso è successivo all'ultimo già salvato
        {
            PlayerPrefs.SetInt(LastLevel, indexLevel);                      //aggiorno il dato
        }
    }
}
