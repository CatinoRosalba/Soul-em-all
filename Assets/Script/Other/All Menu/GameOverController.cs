using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    GameObject cameraObj;
    public static bool isGameOver = false;

    public void SetUpGameOver()
    {
        cameraObj = GameObject.Find("Third Person Camera");

        gameObject.SetActive(true);
        
        cameraObj.GetComponent<FreeLookAxisDriver>().enabled = false;
        Time.timeScale = 0;
        
        isGameOver = true;

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public void RestartButton()
    {
        cameraObj = GameObject.Find("Third Person Camera");
        cameraObj.GetComponent<FreeLookAxisDriver>().enabled = true;

        FindObjectOfType<LevelChanger>().FadeAndChangeToLevel(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;

        isGameOver = false;
    }

    public void BackToMainMenu()
    {
        cameraObj = GameObject.Find("Third Person Camera");
        cameraObj.GetComponent<FreeLookAxisDriver>().enabled = true;

        FindObjectOfType<LevelChanger>().FadeAndChangeToLevel("MainMenu");
        Time.timeScale = 1;

        isGameOver = false;
    }

}
