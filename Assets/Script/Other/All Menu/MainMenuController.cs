using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] LevelChanger changeLevel;

    [SerializeField] GameObject loadingPanel;
    [SerializeField] Slider progressbar;
    [SerializeField] Button continueButton;
    [SerializeField] Button[] chapterButton;

    private int unlockLevel;
    private int indexLastLevel;

    //Variaibili di salvataggio
    private static readonly string NewGame = "NewGame";             //1 - iniziata nuova partita | 0 - nessuna nuova partita
    private static readonly string FirstPlay = "FirstPlay";         //0 - prima volta | -1: già aperto altre volte
    private static readonly string LastLevel = "LastLevel";         //

    private void Start()
    {
        //Controllo variaible salvataggio: Prima volta che apre il gioco
        if (!PlayerPrefs.HasKey(FirstPlay))                                             //Se la variaible di salvataggio non esiste
        {
            PlayerPrefs.SetInt(FirstPlay, 0);                                           //creo e setto
        }

        //Controllo variaible salvataggio: Nuova partita
        if (!PlayerPrefs.HasKey(NewGame))                                               //Se la variaible di salvataggio non esiste
        {
            PlayerPrefs.SetInt(NewGame, 0);                                             //creo e setto
            continueButton.interactable = false;                                        //disattivo bottone
        } 
        else if(PlayerPrefs.HasKey(NewGame) && PlayerPrefs.GetInt(NewGame) == 0)        //se esiste ed è 0
        {
            continueButton.interactable = false;                                        //disattivo bottone
        }

        //Controllo variaible salvataggio: Livelli salvati
        if (!PlayerPrefs.HasKey(LastLevel))
        {
            PlayerPrefs.SetInt(LastLevel, 0);
        }
    }

    //Quando inizia una nuova partita, salvo il dato
    public void InitNewGame()
    {
        PlayerPrefs.SetInt(NewGame, 1);
        PlayerPrefs.SetInt(LastLevel, 0);
    }

    public void ContinueGame()
    {
        unlockLevel = PlayerPrefs.GetInt(LastLevel);                //salvo l'ultimo livello salvato

        if (unlockLevel == 1)                                       //Tutorial
        {
            changeLevel.FadeAndChangeToLevel("Level_1");            //Continua al Livello 1
        }
        if (unlockLevel == 2)                                       //Livello 1
        {
            changeLevel.FadeAndChangeToLevel("Level_2");            //Continua al Livello 2
        }
        if (unlockLevel == 3)                                       //Livello 2
        {
            changeLevel.FadeAndChangeToLevel("Level_3");            //Continua al Livello 3
        }
    }

    /*
     *  PER LA DEMO IL PRIMO CAPITOLO CONTIENE I LIVELLI: 1-2-3 
     */
    public void ChapterSelectedButton()
    {
        indexLastLevel = PlayerPrefs.GetInt(LastLevel);

        if(indexLastLevel >= 1)                                      //Se ha fatto almeno il tutorial
        {
            for (int i = 0; i < indexLastLevel; i++)
            {
                 chapterButton[0].interactable = true;              //Può interagire con il primio capitolo e continuare 
                                                                    //dall'ultimo livello salvato
            }
        }
        else
        {
            for (int i = 0; i < chapterButton.Length; i++)          //Se non ha fatto nulla
            {
                chapterButton[i].interactable = false;              //Non può interagire con nessun capitolo

            }
        }
        
    }

    //Esco dal gioco
    public void exitButton()
    {
        Application.Quit();
        Debug.Log("Uscito");
    }


    //[SerializeField] TMP_Text progressText;

        //[SerializeField] Animator transition;

        /*public string chapter;

        public void LoadChapter(string nameScene)
        {
            //StartCoroutine(LoadAsync(nameScene));
        }


        IEnumerator LoadAsync(string nameScene)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(nameScene);
        
            loadingPanel.SetActive(true);

            while (!operation.isDone)
            {
                float progress = Mathf.Clamp01(operation.progress / .9f);
                progressbar.value = progress;
                progressText.text = progress * 100f + "%";

                yield return null;
            }

        }*/
}
