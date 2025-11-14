using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public void SetMasterVolume (float volume)
    {
        Debug.Log(volume);
        audioMixer.SetFloat("Master", volume);
    }

    public void SetSFXVolume(float volume)
    {
        Debug.Log(volume);
        audioMixer.SetFloat("SFX", volume);
    }
    public void SetDialogueVolume(float volume)
    {
        Debug.Log(volume);
        audioMixer.SetFloat("Dialogue", volume);
    }
    public void SetMusicVolume(float volume)
    {
        Debug.Log(volume);
        audioMixer.SetFloat("Music", volume);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }


}
