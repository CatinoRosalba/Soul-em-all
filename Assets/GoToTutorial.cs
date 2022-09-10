using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToTutorial : MonoBehaviour
{
    [SerializeField] Animator transition;                      //Accedo all'animazione della transizione
    [SerializeField] GameObject fadeObject;                    //FadeTransition object

    public void OnEnable()
    {
        fadeObject.SetActive(true);                             //Attivo l'oggetto della transizione
        transition.SetTrigger("FadeOut");                       //Setto la transizione
    }

    public void OnFadeComplate()
    {
        SceneManager.LoadScene("Tutorial");                  //Cambio scena con il nome del livello precedente
    }
}
