using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static MusicManager _instance;
    public static MusicManager Instance 
    {
        get
        {
            if (_instance) return _instance;
            _instance = FindObjectOfType<MusicManager>();
            DontDestroyOnLoad(_instance.gameObject);
            return _instance;
        }
    }
    
    private void Awake()
    {
        if(_instance && _instance.gameObject != gameObject)
        {
            Destroy(gameObject);
        }
    }

    public AudioSource bgMusic;

    public void ChangeTrack(AudioClip music)   
    {
        bgMusic.Stop();
        bgMusic.clip = music;
        bgMusic.Play();
    }     
}
