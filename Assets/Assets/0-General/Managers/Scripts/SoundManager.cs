using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager _instance;
    public static SoundManager Instance 
    {
        get
        {
            if (_instance) return _instance;
            _instance = FindObjectOfType<SoundManager>();
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
    
    public AudioSource sfxPlayer;
    public AudioClip buttonAcceptClip;
    public AudioClip buttonSwapClip;
    public void PlayButtonAccept()
    {
        playSFX(buttonAcceptClip);
    }
    public void PlayButtonSwap()
    {
        playSFX(buttonSwapClip);
    }
    public void playSFX(AudioClip sfxClip)   
    {
        sfxPlayer.clip = sfxClip;
        sfxPlayer.Play();
    } 
}
