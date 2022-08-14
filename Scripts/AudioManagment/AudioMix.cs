using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioMix : MonoBehaviour
{
    AudioMixer mixer;
    void Start()
    {
        mixer.SetFloat("Main Volume", 20.0f);
    }

    
    void Update()
    {
        
    }
}
