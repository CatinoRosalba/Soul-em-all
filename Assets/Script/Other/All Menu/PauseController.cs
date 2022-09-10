using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    [SerializeField] GameObject pausa;
    GameObject cameraObj;

    public static bool isGamePaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isGamePaused && !DialogueTrigger.isStartedDialogue && !GameOverController.isGameOver)
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
        cameraObj = GameObject.Find("Third Person Camera");
        
        pausa.SetActive(true);

        cameraObj.GetComponent<FreeLookAxisDriver>().enabled = false;
        Time.timeScale = 0;

        isGamePaused = true;

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public void ResumeGame()
    {
        cameraObj = GameObject.Find("Third Person Camera");
        pausa.SetActive(false);

        cameraObj.GetComponent<FreeLookAxisDriver>().enabled = true;
        Time.timeScale = 1f;

        isGamePaused = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void BackToMainMenu()
    {
        isGamePaused = false;
        cameraObj = GameObject.Find("Third Person Camera");
        cameraObj.GetComponent<FreeLookAxisDriver>().enabled = true;
        FindObjectOfType<LevelChanger>().FadeAndChangeToLevel("MainMenu");
        Time.timeScale = 1;
    }

    public void Regame()
    {
        isGamePaused = false;
        cameraObj = GameObject.Find("Third Person Camera");
        cameraObj.GetComponent<FreeLookAxisDriver>().enabled = true;
        FindObjectOfType<LevelChanger>().FadeAndChangeToLevel(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

}
