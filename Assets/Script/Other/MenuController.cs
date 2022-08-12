using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public string newGame;

    public void newGameDialogYes()
    {
        SceneManager.LoadScene(newGame);
    }

    public void newGameDialogNo()
    {
        //SceneManager.LoadScene(newGame);
    }

    public void exitButton()
    {
        Application.Quit();
        Debug.Log("Uscito");
    }
}
