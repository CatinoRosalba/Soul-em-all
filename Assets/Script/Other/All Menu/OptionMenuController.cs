using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionMenuController : MonoBehaviour
{
    //Variabili di salvataggio
    private static readonly string FirstPlay = "FirstPlay";
    private static readonly string BackgroundVolumePref = "BackgroundVolumePref";
    private static readonly string SoundFXPref = "SoundFXPref";
    private static readonly string SensitivityX = "SensX";
    private int firstPlayInt;

    [Header("Impostazioni - Audio Backgound")]
    [SerializeField] private Slider backgroundVolumeSlider;
    [SerializeField] private TMP_Text backgroundVolumeText;
    [SerializeField] private AudioSource backgroundAudio;
    private float backgroundVolumeValue;

    [Header("Impostazioni - Audio Effetti")]
    [SerializeField] private Slider effectVolumeSlider;
    [SerializeField] private TMP_Text effectVolumeText;
    [SerializeField] private AudioSource[] effectAudio;
    private float effectVolumeValue;

    [Header("Impostazioni - Sensibilità")]
    [SerializeField] private Slider sensitivitySlider;
    [SerializeField] private TMP_Text sensitivityText;
    private float sensX;

    private void Start()
    {
        firstPlayInt = PlayerPrefs.GetInt(FirstPlay);

        if (firstPlayInt == 0)                                                      //Se è la prima volta che apre il gioco
        {
            //Volume Background
            backgroundVolumeValue = 0.5f;                                           //Setta il volume del background di default
            backgroundVolumeSlider.value = backgroundVolumeValue;                   //Assegna allo slider
            backgroundVolumeText.text = backgroundVolumeValue.ToString();           //Setta il testo
            PlayerPrefs.SetFloat(BackgroundVolumePref, backgroundVolumeValue);      //Salva il master volume

            //Volume effetti
            effectVolumeValue = 0.5f;                                               //Setta il volume degli effetti di default
            effectVolumeSlider.value = effectVolumeValue;                           //Assegna allo slider
            effectVolumeText.text = effectVolumeValue.ToString();                   //Setta il testo
            PlayerPrefs.SetFloat(SoundFXPref, effectVolumeValue);                   //Salva il volume

            //Sensibilità
            sensX = 1;                                                              //Setta la sensibilità di default
            sensitivitySlider.value = sensX;                                        //Assegna allo slider
            sensitivityText.text = sensX.ToString();                                //Setta il testo
            PlayerPrefs.SetFloat(SensitivityX, sensX);                                //Salva il volume

            PlayerPrefs.SetInt(FirstPlay, -1);                                      //Setta la variabile di FirstPlay
        }
        else
        {
            //Volume backgound
            backgroundVolumeValue = PlayerPrefs.GetFloat(BackgroundVolumePref);     //Setta il volume del background salvata precedentemente 
            backgroundVolumeSlider.value = backgroundVolumeValue;                   //Assegna allo slider

            //Volume effetti
            effectVolumeValue = PlayerPrefs.GetFloat(SoundFXPref); ;                 //Setta il volume degli effetti salvata precedentemente 
            effectVolumeSlider.value = effectVolumeValue;                            //Assegna allo slider

            //Sensibilità
            sensX = PlayerPrefs.GetFloat(SensitivityX);                                //Setta la sensibilità salvata precedentemente 
            sensitivitySlider.value = sensX;                                         //Assegna allo slider
        }
    }

    //Aggiorna in real time il testo e il volume della musica in background
    public void UpdateBackgoundValue(float value)
    {
        backgroundAudio.volume = value;
        backgroundVolumeText.text = value.ToString("0.0").Replace(",", ".");
    }

    //Aggiorna in real time il testo e il volume degli effetti
    public void UpdateEffectValue(float value)
    {
        for (int i = 0; i < effectAudio.Length; i++)
        {
            effectAudio[i].volume = effectVolumeSlider.value;
        }

        effectVolumeText.text = value.ToString("0.0").Replace(",", ".");
    }

    //Aggiorna in real time il testo dello slider della sensibilità
    public void UpdateSensitivityValue(float value)
    {
        sensitivityText.text = value.ToString("0.0").Replace(",", ".");
    }

    public void UpdateSound()
    {
        backgroundAudio.volume = backgroundVolumeSlider.value;

        for (int i = 0; i < effectAudio.Length; i++)
        {
            effectAudio[i].volume = effectVolumeSlider.value;
        }
    }

    public void Apply()
    {
        PlayerPrefs.SetFloat(BackgroundVolumePref, backgroundVolumeSlider.value);                 //Salva il valore backgound
        PlayerPrefs.SetFloat(SoundFXPref, effectVolumeSlider.value);                              //Salva il valore effetti
        PlayerPrefs.SetFloat(SensitivityX, sensitivitySlider.value);                          //Salva la sensibilità
    }
}
