using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameoverManager : MonoBehaviour
{
    public Pawn pawn;
    private int numberofdeadplayers;
    
    public void isGameOver()
    {
       Controller playercontroller = pawn.controller.GetComponent<PlayerController>();
       if(playercontroller!=null)
       {
            if(GameManager.instance.players[0].isIncompacitated && GameManager.instance.players[1].isIncompacitated)
            {
                GameManager.instance.gameFSM.ReloadGame();
                GameManager.instance.gameFSM.ActivateGameOver();
            }
       }
    }
    public void isGameWon()
    {
       if(GameManager.instance.aiPlayers.Count == 0)
       {
        //activate win screen
       }
    }
   
}
