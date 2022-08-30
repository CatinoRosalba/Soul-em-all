using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    [SerializeField] Animator transition;
    [SerializeField] GameObject fadeObject;

    private string levelToChange;

    public void FadeAndChangeToLevel(string levelName)
    {
        fadeObject.SetActive(true);
        levelToChange = levelName;
        transition.SetTrigger("FadeOut");
    }

    public void OnFadeComplate()
    {
        SceneManager.LoadScene(levelToChange);
    }
}
