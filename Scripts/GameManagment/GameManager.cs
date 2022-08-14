using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool isMultiplayer;
    public GameObject listenerObj;
    public audiomanager musicAudioManager;
    public audiomanager uiAudioManager;
    public audiomanager sfxAudioManager;
    public bool isUseSeed;
    public string mapStringSeed;
    public bool isUseDateSeed;
    private int seed;
    public int MaxPlayers;
    public int MaxAIPlayers;
 
    public MapGenerator levelGen;
    public GameFSM gameFSM;
    public List<Spawner> PlayerSpawners;
    public List<Spawner> AiSpawners;
    public List<GameObject> LevelZones;
    //public List<TankPawn> Destroyedtanks;
    //public List<HumanPawn> DeadPlayers;
    //public List<HumanPawn> DeadAIPlayers;
    //private GameObject newAiPawn;
    public static GameManager instance;
    public List<PlayerController> players;
    public List<AiController> aiPlayers;
    public List<HumanPawn> humans;
    public List<TankPawn> Vehicles;
    public List<WayPointCluster> Waypointcluster;
    public bool isMapGenerated = false;
    private void Awake()
    {
        if(isUseSeed && isUseDateSeed)
        {
            Debug.LogWarning("WARNING: date seed and string seed are both set in the Game manager. Only select one or the other. Jerk");
        }
        //if there isnt already a gamemanager, 
        //create an instance that wont be destroyed when a new scene is loaded
        if(instance == null)
        {
           instance = this;
           DontDestroyOnLoad(gameObject);
        }//otherwise, destroy this gameobject so there arent multiple GameManagers.
        else 
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        //This is how we will change and set states in the game state machine. Except via buttons and keypresses.
        gameFSM.currentState = GameStates.gameStates.Title;
        gameFSM.SetStates();
    }
    public void GenerateMap()
    {
        levelGen.GenerateMap();
        ResetPlayerSpawns();
        ResetAiSpawns();
        isMapGenerated = true;
    }

    public void ResetPlayerSpawns()
    {
        //create an array of ints the same size of the number of spawners in the world.
        int[] spawnPointsToEnable = new int[PlayerSpawners.Count];
        //first set all items of the array to -1, because they default to 0, which will cause an extra spawn point to be enabled when iterating through later
        //because indexes in an array or list start at 0.
         for(int x = 0; x < spawnPointsToEnable.Length; x++)
        {
            //create amount of random numbers equal to the max players
            spawnPointsToEnable[x] = -1;
        }
        
        for(int x = 0; x < MaxPlayers; x++)
        {
            //create amount of random numbers equal to the max players
            spawnPointsToEnable[x] = UnityEngine.Random.Range(0,PlayerSpawners.Count);
            Debug.Log(spawnPointsToEnable[x]);
        }
            //loop for the number equal to the amount of spawners for this item
            for(int i = 0; i < PlayerSpawners.Count; i++)
            {  
            bool isEqual = false;
            //iterate through the random numbers and see which ones match the current index
                for(int x = 0; x < spawnPointsToEnable.Length; x++)
                {
                   if(spawnPointsToEnable[x] == i)
                   {
                    isEqual = true;
                   }
                }
                //if the current index matches one of the random numbers set it active. 
             if(isEqual)
                {
                    PlayerSpawners[i].gameObject.SetActive(true);
                }
                else
                {
                   PlayerSpawners[i].gameObject.SetActive(false);
                }
            }   
    }
    public void SetPlayerNumbers()
    {
        if(MaxPlayers == 1) return;
        for(int i = 0; i < players.Count; i++)
        {
            if(i == 0)
            {
            players[i].playerNumber = 1;
            Camera cam = players[i].CurrentCamera.GetComponentInChildren<CameraMovement1>().GetComponent<Camera>();
            cam.rect = new Rect(0.0f, 0.0f, 0.5f, 1.0f);
            }
             if(i == 1)
            {
            players[i].playerNumber = 2;
            Camera cam = players[i].CurrentCamera.GetComponentInChildren<CameraMovement1>().GetComponent<Camera>();
            cam.rect = new Rect(0.5f, 0.0f, 0.5f, 1.0f);
            }
        }
    }
    public void ResetSplitScreenCameraSettings()
    {
         if(MaxPlayers == 1) return;
        for(int i = 0; i < players.Count; i++)
        {
            if(players[i].playerNumber == 1)
            {
            Camera cam = players[i].CurrentCamera.GetComponentInChildren<CameraMovement1>().GetComponent<Camera>();
            cam.rect = new Rect(0.0f, 0.0f, 0.5f, 1.0f);
            }
             if(players[i].playerNumber == 2)
            {
            Camera cam = players[i].CurrentCamera.GetComponentInChildren<CameraMovement1>().GetComponent<Camera>();
            cam.rect = new Rect(0.5f, 0.0f, 0.5f, 1.0f);
            }
            
        }
    }


    public void ResetAiSpawns()
    {
        //create an array of ints the same size of the number of spawners in the world.
        int[] AispawnPointsToEnable = new int[AiSpawners.Count];
        //first set all items of the array to -1, because they default to 0, which will cause an extra spawn point to be enabled when iterating through later
         for(int x = 0; x < AispawnPointsToEnable.Length; x++)
        {
            //create amount of random numbers equal to the max players
            AispawnPointsToEnable[x] = -1;
        }
        for(int x = 0; x < MaxAIPlayers; x++)
        {
            //create amount of random numbers equal to the max players
            AispawnPointsToEnable[x] = UnityEngine.Random.Range(0,AiSpawners.Count + 1);
        }
          
            //loop for the number equal to the amount of spawners for this item
            for(int i = 0; i < AiSpawners.Count; i++)
            {  
                bool isEqual = false;
                    //iterate through the random numbers and see which ones match the current index
                    for(int x = 0; x < AispawnPointsToEnable.Length; x++)
                    {
                       if(AispawnPointsToEnable[x] == i)
                       {
                        isEqual = true;
                       }
                    }
                //if the current index matches one of the random numbers set it active. 
                if(isEqual)
                {
                    AiSpawners[i].gameObject.SetActive(true);
                }
                else
                {
                    if(MaxPlayers ==1)
                    {
                      AiSpawners[i].gameObject.SetActive(false);
                    }
                    if(MaxPlayers ==2)
                    {
                      Destroy(AiSpawners[i].gameObject);
                    }
                }
            }   
    }
     public void SetStringSeed()
    {
         seed = mapStringSeed.GetHashCode();
         UnityEngine.Random.InitState(seed);
    }
    public void SetMapOfTheDaySeed()
    {
         UnityEngine.Random.InitState(DateToInt(DateTime.Now.Date));
         int thisDate = (DateToInt(DateTime.Now.Date));
         Debug.Log("This is the date:" + thisDate);
    }
     public void RerandomizeSeed()
    { 
         UnityEngine.Random.InitState(DateToInt(DateTime.Now));
    }
    public int DateToInt(DateTime dateToUse)
    {
        return (int)dateToUse.ToBinary();
    }
}
