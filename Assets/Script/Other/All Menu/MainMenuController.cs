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
    [SerializeField] Button[] levelButton;
    [SerializeField] Toggle[] difficultButton;

    private int unlockLevel;
    private int indexLastLevel;

    //Variaibili di salvataggio
    private static readonly string NewGame = "NewGame";             //1 - iniziata nuova partita | 0 - nessuna nuova partita
    private static readonly string FirstPlay = "FirstPlay";         //0 - prima volta | -1: gi� aperto altre volte
    private static readonly string LastLevel = "LastLevel";         //
    private static readonly string GameDifficulty = "Difficulty";   //0 Facile - 1 Normale

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
        else if (PlayerPrefs.HasKey(NewGame) && PlayerPrefs.GetInt(NewGame) == 0)        //se esiste ed � 0
        {
            continueButton.interactable = false;                                        //disattivo bottone
        }

        //Controllo variaible salvataggio: Livelli salvati
        if (!PlayerPrefs.HasKey(LastLevel))
        {
            PlayerPrefs.SetInt(LastLevel, 0);
        }

        if (PlayerPrefs.GetInt(LastLevel) == 4)                                          //Completato Livello 3
        {
            continueButton.interactable = false;                                        //Disattivo bottone continua
        }

        if (PlayerPrefs.GetInt(GameDifficulty) == 0)
        {
            for (int i = 0; i < difficultButton.Length; i++)
            {
                difficultButton[0].isOn = true;
                difficultButton[1].isOn = false;
            }

        }
        else
        {
            for (int i = 0; i < difficultButton.Length; i++)
            {
                difficultButton[0].isOn = false;
                difficultButton[1].isOn = true;
            }
        }
    }



    //Imposta la difficolt� facile
    #region Difficulty
    public void SetEasyDifficulty(bool isOn)
    {
        if (isOn)
        {
            PlayerPrefs.SetInt(GameDifficulty, 0);
        }
    }

    //Imposta la difficolt� normale
    public void SetNormalDifficulty(bool isOn)
    {
        if (isOn)
        {
            PlayerPrefs.SetInt(GameDifficulty, 1);
        }
    }
    #endregion

    //Quando inizia una nuova partita, salvo il dato
    public void InitNewGame()
    {
        PlayerPrefs.SetInt(NewGame, 1);
        PlayerPrefs.SetInt(LastLevel, 0);
    }

    public void ContinueGame()
    {
        unlockLevel = PlayerPrefs.GetInt(LastLevel);                //recupero l'ultimo livello salvato

        if (unlockLevel == 0)                                       //Non ha completato il Tutorial
        {
            changeLevel.FadeAndChangeToLevel("Tutorial");           //Riprende all'inizio del tutorial
        }
        if (unlockLevel == 1)                                       //Completato Tutorial
        {
            changeLevel.FadeAndChangeToLevel("Level_1");            //Continua al Livello 1
        }
        if (unlockLevel == 2)                                       //Completato Livello 1
        {
            changeLevel.FadeAndChangeToLevel("Level_2");            //Continua al Livello 2
        }
        if (unlockLevel == 3)                                       //Completato Livello 2
        {
            changeLevel.FadeAndChangeToLevel("Level_3");            //Continua al Livello 3
        }
    }

    /*
     *  PER LA DEMO IL PRIMO CAPITOLO CONTIENE I LIVELLI: 1-2-3 
     */
    public void InteractableChapter()
    {
        indexLastLevel = PlayerPrefs.GetInt(LastLevel);

        if (indexLastLevel >= 1)                                   //Se ha fatto almeno il tutorial
        {
            for (int i = 0; i < chapterButton.Length; i++)
            {
                chapterButton[0].interactable = true;              //Pu� interagire con il primio capitolo e visualizzare i livelli
                chapterButton[1].interactable = false;
                chapterButton[2].interactable = false;
            }
        }
        else
        {
            for (int i = 0; i < chapterButton.Length; i++)          //Se non ha fatto nulla
            {
                chapterButton[i].interactable = false;              //Non pu� interagire con nessun capitolo

            }
        }
        
    }

    public void InteractableLevel()
    {
        indexLastLevel = PlayerPrefs.GetInt(LastLevel);

        if (indexLastLevel == 2)                                      //Se ha completato il lvl 1 sblocco solo lui
        {
            for (int i = 0; i < levelButton.Length; i++)
            {
                levelButton[0].interactable = true;
                levelButton[1].interactable = false;
                levelButton[2].interactable = false;
            }
        }
        else if (indexLastLevel == 3)                               //Se ha completato il lvl 2 sblocco 1 e 2
        {
            for (int i = 0; i < levelButton.Length; i++)
            {
                levelButton[0].interactable = true;
                levelButton[1].interactable = true;
                levelButton[2].interactable = false;
            }
        } 
        else if (indexLastLevel == 4)                               //Se ha completato il lvl 3 li sblocco tutti
        {
            for (int i = 0; i < levelButton.Length; i++)
            {
                levelButton[i].interactable = true;
            }
        }
        else
        {                                                           //Se non ha superato nessun livello li lascio bloccati
            for (int i = 0; i < levelButton.Length; i++)
            {
                levelButton[i].interactable = false;
            }
        }
    }

    //Esco dal gioco
    public void ExitButton()
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
