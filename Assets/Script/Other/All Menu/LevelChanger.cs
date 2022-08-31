using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    [SerializeField] Animator transition;                      //Accedo all'animazione della transizione
    [SerializeField] GameObject fadeObject;                    //FadeTransition object

    private string levelToChange;                              //Livello a cui bisogno cambiare

    public void FadeAndChangeToLevel(string levelName)
    {
        fadeObject.SetActive(true);                             //Attivo l'oggetto della transizione
        levelToChange = levelName;                              //Passo il nome del livello
        transition.SetTrigger("FadeOut");                       //Setto la transizione
    }

    public void OnFadeComplate()
    {
        SceneManager.LoadScene(levelToChange);                  //Cambio scena con il nome del livello precedente
    }
}
