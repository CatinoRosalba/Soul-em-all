using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    public void SetUpGameOver()
    {
        gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        Time.timeScale = 0;
    }

    public void RestartButton()
    {
        FindObjectOfType<LevelChanger>().FadeAndChangeToLevel(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    public void BackToMainMenu()
    {
        FindObjectOfType<LevelChanger>().FadeAndChangeToLevel("MainMenu");
        Time.timeScale = 1;
    }

}
