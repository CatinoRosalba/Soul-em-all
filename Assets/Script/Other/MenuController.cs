using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [Header("Nuovo gioco")]
    public string newGame;

    [Header("Impostazioni - Audio")]
    [SerializeField] private TMP_Text volumeTextValure = null;
    [SerializeField] private Slider volumeSlider = null;
    [SerializeField] private float defaultVolume = 1.0f;
    
    [SerializeField] private GameObject confermationBox = null;

    public void newGameDialogYes()
    {
        SceneManager.LoadScene(newGame);                                    //Carica una nuova scena
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

    public float SetVolume(float volume)
    {
        AudioListener.volume = volume;
        volumeTextValure.SetText(volume.ToString("0.0").Replace(",", "."));
        return volume;
    }

    public void VolumeApply()
    {
        PlayerPrefs.SetFloat("masterVolume", AudioListener.volume);                 //Salva il valore
        //StartCoroutine(ConfermationBox());
    }

    public void ResetButton()
    {
        //Audio Reset
        AudioListener.volume = defaultVolume;
        volumeSlider.value = defaultVolume;
        volumeTextValure.SetText(defaultVolume.ToString("0.0").Replace(",", "."));
        VolumeApply();
    }

    

    /*public IEnumerator ConfermationBox()
    {
        confermationBox.SetActive(true);
        yield return new WaitForSeconds(2);
        confermationBox.SetActive(false);
    }*/
}
