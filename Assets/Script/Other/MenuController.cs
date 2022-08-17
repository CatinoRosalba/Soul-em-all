using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Cinemachine;

public class MenuController : MonoBehaviour
{
    //Variabili generali
    private static readonly string FirstPlay = "FirstPlay";
    private static readonly string BackgroundVolumePref = "BackgroundVolumePref";
    private static readonly string SoundFXPref = "SoundFXPref";
    private static readonly string SensitivityX = "SensX";
    private int firstPlayInt;


    [Header("Nuovo gioco")]
    public string newGame;

    [Header("Impostazioni - Audio Backgound")]
    [SerializeField] private Slider backgroundVolumeSlider;
    [SerializeField] private AudioSource backgroundAudio;
    private float backgroundVolumeValue;

    [Header("Impostazioni - Audio Effetti")]
    [SerializeField] private Slider effectVolumeSlider;
    [SerializeField] private AudioSource[] effectAudio;
    private float effectVolumeValue;

    [Header("Impostazioni - Sensibilità")]
    [SerializeField] private Slider sensitivitySlider;
    private int sensX;


    private void Start()
    {

        firstPlayInt = PlayerPrefs.GetInt(FirstPlay);

        if (firstPlayInt == 0)                                                      //Se è la prima volta che apre il gioco
        {       
            //Volume Background
            backgroundVolumeValue = 0.5f;                                           //Setta il volume del background
            backgroundVolumeSlider.value = backgroundVolumeValue;                   //Assegna allo slider
            PlayerPrefs.SetFloat(BackgroundVolumePref, backgroundVolumeValue);      //Salva il master volume

            //Volume effetti
            effectVolumeValue = 0.5f;                                               //Setta il volume degli effetti
            effectVolumeSlider.value = effectVolumeValue;                           //Assegna allo slider
            PlayerPrefs.SetFloat(SoundFXPref, effectVolumeValue);                   //Salva il volume

            //Sensibilità
            sensX = 500;
            sensitivitySlider.value = sensX;
            PlayerPrefs.SetInt(SensitivityX, sensX);
            
            PlayerPrefs.SetInt(FirstPlay, -1);                                      //Setta la variabile di FirstPlay
        }
        else
        {
            //Volume backgound
            backgroundVolumeValue = PlayerPrefs.GetFloat(BackgroundVolumePref);     //Setta il volume del background
            backgroundVolumeSlider.value = backgroundVolumeValue;                   //Assegna allo slider

            //Volume effetti
            effectVolumeValue = PlayerPrefs.GetFloat(SoundFXPref); ;                 //Setta il volume degli effetti
            effectVolumeSlider.value = effectVolumeValue;                            //Assegna allo slider

            //Sensibitlià
            sensX = PlayerPrefs.GetInt(SensitivityX);
            sensitivitySlider.value = sensX;
        }
    }

    /*
     * METODI MENU PRINCIPALE
     */
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

    /*
     * METODI IMPOSTAZIONI
     */
    public void UpdateSound()
    {
        backgroundAudio.volume = backgroundVolumeSlider.value;

        for (int i = 0; i < effectAudio.Length; i++)
        {
            effectAudio[i].volume = effectVolumeSlider.value;
        }
    }

    public void ApplySound()
    {
        PlayerPrefs.SetFloat(BackgroundVolumePref, backgroundVolumeSlider.value);                 //Salva il valore backgound
        PlayerPrefs.SetFloat(SoundFXPref, effectVolumeSlider.value);                              //Salva il valore effetti
        PlayerPrefs.SetInt(SensitivityX, (int) sensitivitySlider.value);
    }
    

}
