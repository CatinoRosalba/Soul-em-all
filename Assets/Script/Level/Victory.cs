using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Victory : MonoBehaviour
{
    private GameObject sfx;
    private AudioSource victorySound;

    private static readonly string LastLevel = "LastLevel";

    [SerializeField] LevelChanger transition;
    [SerializeField] string nextLevel;

    private int indexLevel;

    private void Start()
    {
        sfx = GameObject.Find("SFX");
        victorySound = sfx.transform.Find("SFX - Victory").GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        SaveProgress();
        victorySound.Play();

        if (other.CompareTag("Player"))
        {
            transition.FadeAndChangeToLevel(nextLevel);                     //Passa al prossimo livello
        }
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
