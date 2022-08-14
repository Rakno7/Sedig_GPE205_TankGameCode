using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankHealth : Health
{
    public float wallCollisionDamage = 5;
    private Rigidbody rb;
    public override void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        currentHealth = maxHealth;
        isCanTakeDamage = true;
    }
    public override void ReduceHealth(float Amount, Pawn playerPawn)
    {
        if(!isCanTakeDamage) return;
        
        
        playerPawn = GetComponent<Pawn>();
        
        currentHealth -= Amount;
        currentHealth = Mathf.Clamp(currentHealth,0,maxHealth);
        
        if(currentHealth <= 0)
        {
            Die();
        }
    }
    public override void RestoreHealth(float Amount, Pawn playerPawn)
    {
        
        playerPawn = GetComponent<Pawn>();
        
        currentHealth += Amount;
        currentHealth = Mathf.Clamp(currentHealth,0,maxHealth);
        
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if(other.transform.tag == "Projectile")
        {
             ProjectileExplode thisProjectile = other.transform.gameObject.GetComponent<ProjectileExplode>();
            float DamageTaken = thisProjectile.ProjectileDamage;
            whoHitme = thisProjectile.whoTookThisShot;    
            ReduceHealth(DamageTaken,GetComponent<Pawn>());
        }
       
        //else if(rb.velocity.magnitude > 2) //To Do: figure out why wall collision damage only works if the collision is on angle.
        //{
        //   ReduceHealth(wallCollisionDamage,GetComponent<Pawn>());
        //}
    }

    public override void Die()
    {
         if(whoHitme !=null)
        {
        whoHitme.AddScore(1);
        float currentScore = whoHitme.Score + 1;
        SetScoreUI(currentScore);
        }
            TankPawn thisPawn = gameObject.GetComponent<TankPawn>();
            GameManager.instance.Vehicles.Remove(thisPawn);
            
            
            if(gameObject.GetComponentInParent<TankPawn>().controller !=null)
            {
                if(gameObject.GetComponentInParent<TankPawn>().controller.GetComponent<AiController>())
                {
                 Destroy(gameObject.GetComponentInParent<TankPawn>().controller.gameObject);
                 GameManager.instance.aiPlayers.Remove(gameObject.GetComponentInParent<TankPawn>().controller.GetComponent<AiController>());
                    if(thisPawn.Driver != null)
                     {
                         Destroy(thisPawn.Driver.gameObject);
                         GameManager.instance.humans.Remove(thisPawn.Driver.GetComponent<HumanPawn>());
                     //    GameManager.instance.DeadAIPlayers.Add(thisPawn.Driver.GetComponent<HumanPawn>());
                     }
                for(int i = 0; i < GameManager.instance.players.Count; i ++)
                {GameManager.instance.players[i].pawn.gameObject.GetComponent<Health>().playerHUD.SetEnemyCount();}
                }
                else 
                {
                    //if its a player make them exit the vehicle before destroying the game object
                   gameObject.GetComponentInParent<TankPawn>().EnterVehicle();
                }
            }
            Destroy(gameObject);
    }
}
