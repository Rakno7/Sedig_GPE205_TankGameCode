using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class OptionsUI : MonoBehaviour
{
    public Animator anim;
    public AudioMixer mainAudioMixer;
    public Slider mainVolumeSlider;
    public Slider musicVolumeSlider;   
    public Slider SFXVolumeSlider;  
    public Slider AmbianceVolumeSlider; 
    public InputField SeedInput; 
    public Toggle mapOfTheDayToggle;
    public Toggle stringSeedToggle;
    public Text BotCount;
    public Text MapHeightCount;
    public Text MapWidthCount;
    

    void Start()
    {
        //anim.SetBool("isMoving", true);
        BotCount.text = GameManager.instance.MaxAIPlayers.ToString();
        MapWidthCount.text = GameManager.instance.levelGen.cols.ToString();
        MapHeightCount.text = GameManager.instance.levelGen.rows.ToString();
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

    public void UseMapOfTheDay()
    {
        stringSeedToggle.isOn = false;
        GameManager.instance.isUseSeed = false;
        GameManager.instance.isUseDateSeed = true;
        
        
    }

    public void UseStringSeed()
    {
        mapOfTheDayToggle.isOn = false;
        GameManager.instance.isUseDateSeed = false;
        GameManager.instance.isUseSeed = true;
        
        
    }
    public void SetStringSeed()
    {
        GameManager.instance.mapStringSeed = SeedInput.text;
    }

    public void AddBot()
    {
       GameManager.instance.MaxAIPlayers += 1;
       BotCount.text = GameManager.instance.MaxAIPlayers.ToString();
    }

    public void SubtractBot()
    {
       GameManager.instance.MaxAIPlayers -= 1;
       BotCount.text = GameManager.instance.MaxAIPlayers.ToString();
    }
     public void AddMapHeight()
    {
       GameManager.instance.levelGen.rows +=1;
       MapHeightCount.text = GameManager.instance.levelGen.rows.ToString();
    }

    public void SubtractMapHeight()
    {
         GameManager.instance.levelGen.rows -=1;
         MapHeightCount.text = GameManager.instance.levelGen.rows.ToString();
    }
      public void AddMapWidth()
    {
        GameManager.instance.levelGen.cols +=1;
        MapWidthCount.text = GameManager.instance.levelGen.cols.ToString();
    }

    public void SubtractMapWidth()
    {
         GameManager.instance.levelGen.cols -=1;
         MapWidthCount.text = GameManager.instance.levelGen.cols.ToString();
    }
}
