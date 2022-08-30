using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    [SerializeField] Animator transition;

    private string levelToChange;  

    public void ChangeLevel()
    {
        FadeToLevel("Tutorial");
    }

    public void FadeToLevel(string levelName)
    {
        levelToChange = levelName;
        transition.SetTrigger("FadeOut");
    }

    public void OnFadeComplate()
    {
        SceneManager.LoadScene(levelToChange);
    }
}
