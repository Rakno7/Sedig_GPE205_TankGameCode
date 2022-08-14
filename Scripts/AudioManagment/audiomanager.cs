using UnityEngine.Audio;
using System;
using UnityEngine;
using Random = UnityEngine.Random;




[System.Serializable]
public class audiomanager : MonoBehaviour
{
   public AudioMixerGroup mixerGroup;
   public GameObject AudioSourcePrefab;
    public Sound[] sounds;
    
    public AudioSource SSource;

    public AudioClip clip;
    [Range(0f, 0.5f)]
    public float volume;
   
    [Range(0f, 0.5f)]
    public float pitch;

    [Range(0f, 0.5f)]
    public float volumerandom = 0.1f;
    
    [Range(0f, 0.5f)]
    public float pitchrandom = 0.1f; 

    //public float spatialBlend;

    public float pitchmin = 0.8f;
    
    public float pitchmax = 1f;
    
    public float volumemin = 0.3f;
    
    public float volumemax = 0.5f;

    public bool loop;


    void Start()
    {
        
        foreach (Sound s in sounds)
        {
             
           s.source = gameObject.AddComponent<AudioSource>();
           s.source.clip = s.clip;
           s.source.volume = s.volume; 
           s.source.pitch = s.pitch; 
           s.source.loop = s.loop;
           s.source.playOnAwake = false;
           s.source.outputAudioMixerGroup = mixerGroup;
           //s.source.spatialBlend = 0.5f;
        }
      
    }
  
    public void Stop (string name)
    {
       Sound s = Array.Find(sounds, sound => sound.name == name);
      s.source.pitch = s.pitch = Random.Range(pitchmin, pitchmax); 
      //s.source.volume = s.volume = Random.Range(volumemin, volumemax);
      s.source.Stop(); 
    }

    public void PlayMusic (string name)
    {
       Sound s = Array.Find(sounds, sound => sound.name == name);
      s.source.pitch = s.pitch = 1; 
      s.source.volume = 1;
      s.source.loop = true;
      s.source.Play();
     
    }

    public void Play (string name)
    {
       Sound s = Array.Find(sounds, sound => sound.name == name);
       s.source.volume = s.volume = 1f;
       s.source.pitch = s.pitch = 1; 
       s.source.PlayOneShot(s.clip); 
    }
    public void PlayAtPoint (string name, Vector3 Pos)
    {
     Sound s = Array.Find(sounds, sound => sound.name == name);
     GameObject AudioGameObject = Instantiate(AudioSourcePrefab, Pos,Quaternion.identity);
     AudioSource newSource = AudioGameObject.GetComponent<AudioSource>();
     newSource.clip = s.clip; 
     newSource.outputAudioMixerGroup = mixerGroup;
     newSource.Play();
     Destroy(AudioGameObject, s.clip.length);
    }
}
