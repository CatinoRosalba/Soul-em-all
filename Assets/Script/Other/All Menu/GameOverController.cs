using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    GameObject cameraObj;

    public void SetUpGameOver()
    {
        cameraObj = GameObject.Find("Third Person Camera");
        gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        cameraObj.GetComponent<FreeLookAxisDriver>().enabled = false;
        Time.timeScale = 0;
    }

    public void RestartButton()
    {
        cameraObj = GameObject.Find("Third Person Camera");
        cameraObj.GetComponent<FreeLookAxisDriver>().enabled = true;
        FindObjectOfType<LevelChanger>().FadeAndChangeToLevel(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    public void BackToMainMenu()
    {
        cameraObj = GameObject.Find("Third Person Camera");
        cameraObj.GetComponent<FreeLookAxisDriver>().enabled = true;
        FindObjectOfType<LevelChanger>().FadeAndChangeToLevel("MainMenu");
        Time.timeScale = 1;
    }

}
