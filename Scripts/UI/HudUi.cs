using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudUi : MonoBehaviour
{
    public Slider healthSlider;
    public Text scoreCount;
    public Text enemyCount;
    public Text EnemyText;
    public AudioPlayer Audio;
    
    private void Start()
    {
      if(GameManager.instance.MaxPlayers == 1)
      {
         Audio.sfxAudioManager = GameManager.instance.sfxAudioManager;
         enemyCount.enabled = false;
         EnemyText.enabled = false;
      }
      else
      {
         enemyCount.text = GameManager.instance.MaxAIPlayers.ToString();
      }
    }
    public void SetHealthUi(float currentHealth)
    {
      healthSlider.value = currentHealth;
    }
    public void SetScoreCount(float ScoreToAdd)
    {
      scoreCount.text = ScoreToAdd.ToString();
    }
    public void SetEnemyCount()
    {
      if(GameManager.instance.MaxPlayers > 1)
      {
         enemyCount.text = GameManager.instance.aiPlayers.Count.ToString();
         if(GameManager.instance.aiPlayers.Count == 0)
         {
            Audio.PlayShieldPickup();
            Invoke("WinGame", 2);
         }
      }
    }
    public void WinGame()
    {
       GameManager.instance.gameFSM.ReloadGame();
       GameManager.instance.gameFSM.ActivatWinScreen();
    }
}
