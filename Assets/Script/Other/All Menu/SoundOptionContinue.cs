using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundOptionContinue : MonoBehaviour
{
    private static readonly string BackgroundVolumePref = "BackgroundVolumePref";
    private static readonly string SoundFXPref = "SoundFXPref";

    private float backgroundVolumeValue;
    private float effectVolumeValue;

    [SerializeField] private AudioSource backgroundAudio;
    [SerializeField] private AudioSource[] effectAudio;

    private void Awake()
    {
        ContinueSound();
    }

    public void ContinueSound()
    {
        backgroundVolumeValue = PlayerPrefs.GetFloat(BackgroundVolumePref);
        effectVolumeValue = PlayerPrefs.GetFloat(SoundFXPref);

        backgroundAudio.volume = backgroundVolumeValue;

        for (int i = 0; i < effectAudio.Length; i++)
        {
            effectAudio[i].volume = effectVolumeValue;
        }
    }
}
