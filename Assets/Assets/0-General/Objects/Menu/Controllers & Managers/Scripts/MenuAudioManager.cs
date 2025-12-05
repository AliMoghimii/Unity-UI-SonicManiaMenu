using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAudioManager : MonoBehaviour
{
    [Header("Music Properties :")]
    public AudioClip music;
    public bool nextMenuKeepMusic = false; //keeps the current soundtrack going while transitioning into the next target menu
    
    void OnEnable()
    {
        if(!nextMenuKeepMusic)
        {
            MusicManager.Instance.ChangeTrack(music);
        }
    }
}
