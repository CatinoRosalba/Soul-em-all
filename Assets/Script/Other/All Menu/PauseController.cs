using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    [SerializeField] GameObject pausa;

    public static bool isGamePaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (!isGamePaused)
            {
                PauseGame();
            }
            else
            {
                Invoke("ResumeGame", 0.5f);
            }
        }
    }

    void PauseGame()
    {
        pausa.SetActive(true);
        Time.timeScale = 0;

        isGamePaused = true;

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public void ResumeGame()
    {
        pausa.SetActive(false);
        Time.timeScale = 1f;

        isGamePaused = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Debug.Log("menu");
    }

    public void Regame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

}
