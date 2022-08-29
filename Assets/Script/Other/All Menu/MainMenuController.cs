using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public string chapter;

    public void newGameDialogYes()
    {
        SceneManager.LoadScene("Tutorial");                   //Carica una nuova scena
        Time.timeScale = 1;
    }

    public void exitButton()
    {
        Application.Quit();
        Debug.Log("Uscito");
    }

    public void selectChapter(string chapter)
    {
        SceneManager.LoadScene(chapter);                   //Carica una nuova scena
        Time.timeScale = 1;
    }

}
