using UnityEngine.Audio;
using UnityEngine;

 [System.Serializable]
public class Sound 

{
    public string name;

    public AudioClip clip;

    [Range(0f, 1.0f)]
    public float volume;
   
    [Range(0f, 0.5f)]
    public float pitch;

    [Range(0f, 0.5f)]
    public float volumerandom = 0.1f;
    
    [Range(0f, 0.5f)]
    public float pitchrandom = 0.1f; 


  
    

    public bool loop;

    [HideInInspector]
    public AudioSource source;
}
