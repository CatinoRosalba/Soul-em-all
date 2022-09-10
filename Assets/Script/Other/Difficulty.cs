using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Difficulty : MonoBehaviour
{
    private static readonly string GameDifficulty = "Difficulty";                           //0 Facile - 1 Normale

    [SerializeField] private TMP_Text easy;
    [SerializeField] private TMP_Text normal;

    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey(GameDifficulty))                                             //Se la variaible di salvataggio non esiste
        {
            PlayerPrefs.SetInt(GameDifficulty, 1);                                           //creo e setto
        }
    }

    public void SelectEasy()
    {
        PlayerPrefs.SetInt(GameDifficulty, 0);
    }

    public void SelectNormal()
    {
        PlayerPrefs.SetInt(GameDifficulty, 1);
    }

}
