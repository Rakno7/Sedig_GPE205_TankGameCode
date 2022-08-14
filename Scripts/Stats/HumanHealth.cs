using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanHealth : Health
{
    public ParticleSystem blood;
    public GameoverManager gameoverManager;
    private Rigidbody rb;
   
    private void Update()
    {
        //for testing!//for testing!//for testing!
        if(Input.GetKeyDown(KeyCode.X))
        {
            ReduceHealth(5,null);
        }
    }
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
        currentHealth = Mathf.Clamp(currentHealth, 0 ,maxHealth);
        setHealthUi();
        if(currentHealth <= 0)
        {
            if(GameManager.instance.isMultiplayer)
            {
              HumanPawn thisPawn = gameObject.GetComponent<HumanPawn>();
              if(thisPawn.controller.GetComponent<PlayerController>())
              {
              Incompacitate();
              gameoverManager.isGameOver();
              }
              else
              {
              Die();
              }
            }
            else
            {
               Die();
            }
        }
    }
    public override void RestoreHealth(float Amount, Pawn playerPawn)
    {
        
        playerPawn = GetComponent<Pawn>();
        
        currentHealth += Amount;
        currentHealth = Mathf.Clamp(currentHealth,0,maxHealth);
        setHealthUi();
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if(other.transform.tag == "Projectile")
        {
            Instantiate(blood, transform.position,Quaternion.identity);
            ProjectileExplode thisProjectile = other.transform.gameObject.GetComponent<ProjectileExplode>();
            float DamageTaken = thisProjectile.ProjectileDamage;
            whoHitme = thisProjectile.whoTookThisShot;    
            ReduceHealth(DamageTaken,GetComponent<Pawn>());
        }
    }

    public void Incompacitate()
    {
        HumanPawn thisPawn = gameObject.GetComponent<HumanPawn>();
        
        thisPawn.anim.enabled = false;
        thisPawn.SetRagdollColliderState(true);
        thisPawn.SetRagdollRigidbodyState(false);
        thisPawn.controller.GetComponent<PlayerController>().isIncompacitated = true;
    }
     public void Revive()
    {
        HumanPawn thisPawn = gameObject.GetComponent<HumanPawn>();
        thisPawn.gameObject.transform.position = thisPawn.spineToRotate.position;
        thisPawn.SetRagdollColliderState(false);
        thisPawn.SetRagdollRigidbodyState(true);
        thisPawn.anim.enabled = true;
        thisPawn.controller.GetComponent<PlayerController>().isIncompacitated = false;
        RestoreHealth(10,null);
    }
    public void PrepRevive()
    {
        Invoke("Revive", 2);
    }
    public override void Die()
    { 
        if(whoHitme !=null)
        {
            whoHitme.AddScore(1);
            float currentScore = whoHitme.Score + 1;
            SetScoreUI(currentScore);
        
        }
        //when we have an interface we will also want to call a function to update the scoreboard.


        if(GetComponent<HumanPawn>().controller !=null)
        {
          if(GetComponent<HumanPawn>().controller.GetComponent<AiController>())
          {
            if(GameManager.instance.MaxPlayers == 1)
            {
                GameManager.instance.ResetAiSpawns();
                GameManager.instance.ResetPlayerSpawns();
            }
                HumanPawn thisPawn = gameObject.GetComponent<HumanPawn>();
                GameManager.instance.humans.Remove(thisPawn);
                GameManager.instance.aiPlayers.Remove(GetComponent<HumanPawn>().controller.GetComponent<AiController>());
                for(int i = 0; i < GameManager.instance.players.Count; i ++)
                {GameManager.instance.players[i].pawn.gameObject.GetComponent<Health>().playerHUD.SetEnemyCount();}
                Destroy(thisPawn.controller.gameObject);
                Destroy(thisPawn.anim);
                thisPawn.SetRagdollColliderState(true);
                thisPawn.SetRagdollRigidbodyState(false);
                
                Destroy(thisPawn);
                Destroy(this);
          }
         
             if(GetComponent<HumanPawn>().controller.GetComponent<PlayerController>())
             {
                   GameManager.instance.ResetPlayerSpawns();
   
                   HumanPawn thisPawn = gameObject.GetComponent<HumanPawn>();
                   GameManager.instance.humans.Remove(thisPawn);
                   GameManager.instance.players.Remove(GetComponent<HumanPawn>().controller.GetComponent<PlayerController>());
   
                   //DODO: Instead of destroying the player, since that would clear their score, just ragdoll them and disable input, and add the ability to revive a dead teammate!
                   Destroy(thisPawn.controller.gameObject);
                   Destroy(thisPawn.anim);
                   thisPawn.SetRagdollColliderState(true);
                   thisPawn.SetRagdollRigidbodyState(false);
                   
                   Destroy(thisPawn);
                   Destroy(this);
             }
          
        }   
    }
}
