using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    public AudioClip danger;
    public AudioClip background;
    AudioSource audioSource;

    private void Awake()
    {
        EventManager.AddListener(EventType.ChangeMusic, OnChangeMusic);
        EventManager.AddListener(EventType.PlaySound, OnPlaySound);

    }
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        // audio.Play();
        //yield return new WaitForSeconds(audio.clip.length);

    }

    private void OnPlaySound(object nameAudio)
    {
        
    }

    private void OnChangeMusic(object nameAudio)
    {
        String audioName = (String)nameAudio;
        audioSource.Stop();
       // yield return new WaitForSeconds(1);

        if (audioName == "danger")
        {
            audioSource.clip = danger;
        }
        else if (audioName == "background")
        {
            audioSource.clip = background;
        }

        audioSource.Play();
    }

}
