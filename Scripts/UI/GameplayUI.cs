using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayUI : MonoBehaviour
{
   public GameFSM gameStates;

    void Start()
    {
        
    }

    void Update()
    {
        //TODO: change this to escape when the game is built
       if(Input.GetKeyDown(KeyCode.P))
       {
          gameStates.ActivateInGameOptions();
       } 
    }
}
