using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class IngameOptionsUI : MonoBehaviour
{
    public AudioMixer mainAudioMixer;
    public Slider mainVolumeSlider;
    public Slider musicVolumeSlider;   
    public Slider SFXVolumeSlider;  
    public Slider AmbianceVolumeSlider; 

    void Start()
    {
        
    }
    public void OnMainVolumeChange()
    {
        float newVolume = mainVolumeSlider.value;
        if (newVolume <= 0)
        {
            newVolume = -80;
        } 
        else
        {
            // Above 0 get the log10 value 
            newVolume = Mathf.Log10(newVolume);
            // Set the range to 0-20db range 
            newVolume = newVolume * 20;
        }
        // Set the volume to the new volume setting
        mainAudioMixer.SetFloat("Main Volume", newVolume);
    }
    public void OnMusicVolumeChange()
    {
        float newVolume = musicVolumeSlider.value;
        if (newVolume <= 0)
        {
            newVolume = -80;
        } 
        else
        {
            // Above 0 get the log10 value 
            newVolume = Mathf.Log10(newVolume);
            // Set the range to 0-20db range 
            newVolume = newVolume * 20;
        }
        // Set the volume to the new volume setting
        mainAudioMixer.SetFloat("Music Volume", newVolume);
    }
    public void OnSFXVolumeChange()
    {
        float newVolume = SFXVolumeSlider.value;
        if (newVolume <= 0)
        {
            newVolume = -80;
        } 
        else
        {
            // Above 0 get the log10 value 
            newVolume = Mathf.Log10(newVolume);
            // Set the range to 0-20db range 
            newVolume = newVolume * 20;
        }
        // Set the volume to the new volume setting
        mainAudioMixer.SetFloat("SFX Volume", newVolume);
    }
    public void OnAmbianceVolumeChange()
    {
        float newVolume = AmbianceVolumeSlider.value;
        if (newVolume <= 0)
        {
            newVolume = -80;
        } 
        else
        {
            // Above 0 get the log10 value 
            newVolume = Mathf.Log10(newVolume);
            // Set the range to 0-20db range 
            newVolume = newVolume * 20;
        }
        // Set the volume to the new volume setting
        mainAudioMixer.SetFloat("Ambiance Volume", newVolume);
    }
}
