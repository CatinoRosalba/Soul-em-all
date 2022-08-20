using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [Header("Nuovo gioco")]
    public string newGame;


    public void newGameDialogYes()
    {
        SceneManager.LoadScene(newGame);                                    //Carica una nuova scena
        Time.timeScale = 1;
    }

    public void newGameDialogNo()
    {
        SceneManager.LoadScene(newGame);
    }

    public void exitButton()
    {
        Application.Quit();
        Debug.Log("Uscito");
    }

}
