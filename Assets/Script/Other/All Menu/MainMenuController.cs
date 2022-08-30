using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] GameObject loadingPanel;
    [SerializeField] Slider progressbar;
    [SerializeField] TMP_Text progressText;

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

    public void exitButton()
    {
        Application.Quit();
        Debug.Log("Uscito");
    }

}
