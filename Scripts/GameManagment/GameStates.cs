using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public abstract class GameStates : MonoBehaviour
{
    public enum  gameStates 
   {
     Title, MainMenu, Options, GamePlay, ScoreScreen, GameOver, Credits 
   };

   public GameObject TitleScreenStateObject;
   public GameObject MainMenuStateObject;
   public GameObject OptionsStateObject;
   public GameObject GamePlayStateObject;
   public GameObject ScoreScreenStateObject;
   public GameObject GameOverStateObject;
   public GameObject CreditsStateObject;
   public GameObject InGameOptionsStateObject;
   public GameObject WinStateObject;

   
    public abstract void SetStates();

    public virtual void DeactivateAllStates()
    {
        TitleScreenStateObject.SetActive(false);
        MainMenuStateObject.SetActive(false);
        OptionsStateObject.SetActive(false);
        GamePlayStateObject.SetActive(false);
        ScoreScreenStateObject.SetActive(false);
        GameOverStateObject.SetActive(false);
        CreditsStateObject.SetActive(false);
        InGameOptionsStateObject.SetActive(false);
        WinStateObject.SetActive(false);
    }

//---------------------------------------------------------------------------------------------------------------------------------------
   public void ActivatWinScreen()
   {
    DeactivateAllStates();
    DeactivateCursorLock();
    WinStateObject.SetActive(true);
   }

    public virtual void ActivateTitleScreen()
    {
      DeactivateAllStates();
      DeactivateCursorLock();
      TitleScreenStateObject.SetActive(true);
    }
    public virtual void ActivateMainMenu()
    {
      DeactivateAllStates();
      DeactivateCursorLock();
      MainMenuStateObject.SetActive(true);
    }
    public virtual void ActivateOptions()
    {
      DeactivateAllStates();
      DeactivateCursorLock();
      OptionsStateObject.SetActive(true);
    }
    public virtual void ActivateGamePlay()
    {
      DeactivateAllStates();
      ActivateCursorLock();
      GamePlayStateObject.SetActive(true);
      if(GameManager.instance !=null)
      {
        if(!GameManager.instance.isMapGenerated)
        {
        GameManager.instance.GenerateMap();
        }
      }
    }
    public virtual void ActivateInGameOptions()
    {
      InGameOptionsStateObject.SetActive(true);
      DeactivateCursorLock();
    }
    public virtual void ActivateCursorLock()
    {
         Cursor.lockState = CursorLockMode.Locked;
    }

    public virtual void DeactivateCursorLock()
    {
      Cursor.lockState = CursorLockMode.None;
    }
    public virtual void DeactivateInGameOptions()
    {
      InGameOptionsStateObject.SetActive(false);
      ActivateCursorLock();
    }
    public virtual void ActivateScoreScreen()
    {
      DeactivateAllStates();
      ScoreScreenStateObject.SetActive(true);
    }
    public virtual void ActivateGameOver()
    {
      DeactivateAllStates();
      DeactivateCursorLock();
      ReloadGame();
      GameOverStateObject.SetActive(true);
      
    }
    public virtual void ActivateCredits()
    {
      DeactivateCursorLock();
      DeactivateAllStates();
      CreditsStateObject.SetActive(true);
    }

//---------------------------------------------------------------------------------------------------------------------------------------

 public virtual void QuitGame()
 {
  Application.Quit();
 }
 public virtual void ReloadGame()
 {
  for(int i = 0; i < GameManager.instance.LevelZones.Count; i++)
  {
    Destroy(GameManager.instance.LevelZones[i].gameObject);
  }
  GameManager.instance.LevelZones.Clear();
  for(int i = 0; i < GameManager.instance.players.Count; i++)
  {
    Destroy(GameManager.instance.players[i].gameObject);
  }
  GameManager.instance.players.Clear();
  for(int i = 0; i < GameManager.instance.aiPlayers.Count; i++)
  {
    Destroy(GameManager.instance.aiPlayers[i].gameObject);
  }
  GameManager.instance.aiPlayers.Clear();
  for(int i = 0; i < GameManager.instance.Vehicles.Count; i++)
  {
    Destroy(GameManager.instance.Vehicles[i].gameObject);
  }
  GameManager.instance.Vehicles.Clear();
  for(int i = 0; i < GameManager.instance.humans.Count; i++)
  {
    Destroy(GameManager.instance.humans[i].gameObject);
  }
  for(int i = 0; i < GameManager.instance.Pickups.Count; i++)
  {
    Destroy(GameManager.instance.Pickups[i].gameObject);
  }
  for(int i = 0; i < GameManager.instance.Destroyedtanks.Count; i++)
  {
    Destroy(GameManager.instance.Destroyedtanks[i].gameObject);
  }
  GameManager.instance.Pickups.Clear();
  GameManager.instance.Destroyedtanks.Clear();
  GameManager.instance.humans.Clear();
  GameManager.instance.AiSpawners.Clear();
  GameManager.instance.PlayerSpawners.Clear();
  GameManager.instance.Waypointcluster.Clear();
  GameManager.instance.isMapGenerated = false;
 }



//---------------------------------------------------------------------------------------------------------------------------------------
 
}
