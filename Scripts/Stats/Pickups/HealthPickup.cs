using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{   
    public AudioPlayer audioPlayer;
   
    public HealthPowerup powerup;
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
        PowerupManager powerupManager = other.GetComponentInParent<PowerupManager>();
        if(powerupManager != null)
        {
            audioPlayer.PlayHealthPickup();
            powerupManager.Add(powerup);
            Destroy(gameObject);
        }
    }
}
