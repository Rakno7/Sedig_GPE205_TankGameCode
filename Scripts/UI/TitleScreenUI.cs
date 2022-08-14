using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleScreenUI : MonoBehaviour
{
    public GameFSM gameStates;
    public Text titleText;
    public float introTime;
    private AudioPlayer Audio;
    void Start()
    {
        Audio = GetComponent<AudioPlayer>();
        Audio.PlayShieldPickup();
    }

    
    void Update()
    {
        titleText.color = new Color(0 +Time.time /10 ,0,0,0 + Time.time / 2);
        titleText.rectTransform.position = new Vector3(titleText.rectTransform.position.x,titleText.rectTransform.position.y + Time.time / 100,titleText.rectTransform.position.z); 
        introTime -= Time.deltaTime;
        if(introTime < 0)
        {
            gameStates.ActivateMainMenu();
        }
    }
}
