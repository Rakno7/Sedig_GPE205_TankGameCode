using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    public audiomanager musicAudioManager;
    public audiomanager uiAudioManager;
    void Start()
    {
        musicAudioManager.PlayMusic("MenuTrack");
    }
    public void PlayMultiplayer()
    {
        GameManager.instance.isMultiplayer = true;
        GameManager.instance.MaxPlayers = 2;
    }
    public void PlaySingleplayer()
    {
        GameManager.instance.isMultiplayer = false;
        GameManager.instance.MaxPlayers = 1;
    }
}
