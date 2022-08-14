using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileExplode : MonoBehaviour
{
public GameObject ExplosionParticles;
public GameObject ExplosionSmokeParticles;
public float explosiveForce;
public float explosionRadius;
public Controller whoTookThisShot;
private AudioPlayer audioPlayer;
public bool isBullet;

public float ProjectileDamage;   

    
    private void Start()
    {
       audioPlayer = GetComponent<AudioPlayer>();
       if(audioPlayer!=null)
       { 
        if(isBullet)
        {
           audioPlayer.PlayRifleShot();
        }
        else
        {
            audioPlayer.PlayCannonShot();
        }
       
       }

       else
       {
        Debug.LogWarning("No Audio Player but trying to play audio!");
       }
    }
    private void OnCollisionEnter(Collision other)
    {
        Vector3 Pos = transform.position;
        Quaternion Rotation = transform.rotation;

        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
       
        foreach(Collider hitObjects in colliders)
        {
            Rigidbody rb = hitObjects.GetComponent<Rigidbody>();
            if(rb != null && !hitObjects.gameObject.GetComponent<HumanHealth>() && !hitObjects.gameObject.GetComponent<TankHealth>())
            {
            rb.AddExplosionForce(explosiveForce,transform.position,explosionRadius);
            }
        }
        
        //audio
        if(isBullet)
        {
           //if(other.gameObject.GetComponent<HumanPawn>())
           //{
              audioPlayer.PlayBulletImpact();
           //}
           // if(other.gameObject.GetComponent<TankPawn>())
           //{
   //
           //}
        }
        else
        {
            audioPlayer.PlayCannonImpact();
        }
        
        
        Instantiate(ExplosionParticles, Pos, Rotation);
        Instantiate(ExplosionSmokeParticles, Pos, Rotation);
    }
}
