using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPickup : MonoBehaviour
{
    public AudioPlayer audioPlayer;
    public SpeedPowerup powerup;
    public void Start()
    {
         audioPlayer = GetComponent<AudioPlayer>();
      if(audioPlayer!=null)
      {
         audioPlayer.sfxAudioManager = GameManager.instance.sfxAudioManager;
      }
    }
    public void OnTriggerEnter(Collider other)
    {
        PowerupManager powerupManager = other.GetComponent<PowerupManager>();
        if(powerupManager != null)
        {
             audioPlayer.PlaySpeedPickup();
            powerupManager.Add(powerup);
            Destroy(gameObject);
        }
    }
}
