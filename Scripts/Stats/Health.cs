using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
   public bool isCanTakeDamage;
   public float maxHealth;
   public float currentHealth;
   public HudUi playerHUD;
   public Controller whoHitme;
   public virtual void Start()
   {
    
   }
   public abstract void ReduceHealth(float Amount, Pawn playerPawn);
   public abstract void RestoreHealth(float Amount, Pawn playerPawn);
   public abstract void Die();
   
   
   public virtual void setHealthUi()
   {
      if(playerHUD==null) return;
      //Get a percentage
     
      float percentage = currentHealth/maxHealth;
       Debug.Log(percentage * 100);
      //set health slider value to percentage.
       playerHUD.SetHealthUi(percentage * 100);
   }
  public virtual void SetScoreUI(float Amount)
  {
   
   HudUi otherplayerhud = whoHitme.pawn.gameObject.GetComponent<Health>().playerHUD;
   if(otherplayerhud !=null)
   {
      otherplayerhud.SetScoreCount(Amount);
   }

  }
   
}
