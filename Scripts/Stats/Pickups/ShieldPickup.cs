using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPickup : MonoBehaviour
{
    public AudioPlayer audioPlayer;
    public ShieldPowerup powerup;
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
             audioPlayer.PlayShieldPickup();
            powerupManager.Add(powerup);
            Destroy(gameObject);
        }
    } 

}
