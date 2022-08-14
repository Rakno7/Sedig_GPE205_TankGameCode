using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public audiomanager musicAudioManager;
    public audiomanager uiAudioManager;
    public audiomanager sfxAudioManager;
    void Awake()
    {
      if(GameManager.instance !=null)      
       musicAudioManager = GameManager.instance.musicAudioManager;
       uiAudioManager = GameManager.instance.uiAudioManager;
       sfxAudioManager = GameManager.instance.sfxAudioManager;
    }
    public void PlayerButtonClick()
    {
       uiAudioManager.Play("ButtonClick");
    }
    public void PlayerButtonHover()
    {
       uiAudioManager.Play("ButtonHover");
    }
    public void PlayerToogleClick()
    {
       uiAudioManager.Play("Click");
    }
     public void PlayerSetSeed()
    {
       uiAudioManager.Play("SetSeed");
    }
    
    public void PlayRifleShot()
    {
        int rand;
        rand = Random.Range(1,6);
        
            sfxAudioManager.PlayAtPoint("Rifle" + rand, transform.position);
    }
    public void PlayBulletImpact()
    {
        int rand;
        rand = Random.Range(1,6);
        sfxAudioManager.PlayAtPoint("BulletImpact" + rand, transform.position);
    }

    public void PlayCannonShot()
    {
        int rand;
        rand = Random.Range(1,6);
         sfxAudioManager.PlayAtPoint("Cannon" + rand, transform.position);
    }

    public void PlayCannonImpact()
    {
        int rand;
        rand = Random.Range(1,5);
         sfxAudioManager.PlayAtPoint("CannonImpact" + rand, transform.position);
    }

    public void PlayFootStep()
    {
        int rand;
        rand = Random.Range(1,5);
         sfxAudioManager.PlayAtPoint("Footstep" + rand, transform.position);
    }

     public void PlayHealthPickup()
    {
       sfxAudioManager.PlayAtPoint("PickupHealth",transform.position);
    }
     public void PlaySpeedPickup()
    {
       sfxAudioManager.PlayAtPoint("PickupSpeed",transform.position);
    }
     public void PlayShieldPickup()
    {
       sfxAudioManager.PlayAtPoint("PickupShield",transform.position);
    }
}
