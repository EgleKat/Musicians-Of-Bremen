using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    AudioSource audioSourceBackground;
    AudioSource audioSourceEffect;
    public AudioClip danger;
    public AudioClip background;
    public AudioClip hit;
    public AudioClip ting;



    private void Awake()
    {
        EventManager.AddListener(EventType.ChangeMusic, OnChangeMusic);
        EventManager.AddListener(EventType.PlaySound, OnPlaySound);

    }
    private void Start()
    {
        audioSourceBackground = GetComponents<AudioSource>()[0];
        audioSourceEffect = GetComponents<AudioSource>()[1];

    }

    private void OnChangeMusic(object nameAudio)
    {
        String audioName = (String)nameAudio;
        audioSourceBackground.Stop();
        // yield return new WaitForSeconds(1);

        if (audioName == "danger")
        {
            audioSourceBackground.clip = danger;
        }
        else if (audioName == "background")
        {
            audioSourceBackground.clip = background;
        }

        if (audioName != "stop")
            audioSourceBackground.Play();
    }
    private void OnPlaySound(object nameAudio)
    {
        String audioName = (String)nameAudio;

        if (audioName == "hit")
        {
            audioSourceEffect.clip = hit;
        }
        else if (audioName == "ting")
        {
            audioSourceEffect.clip = ting;
        }

        audioSourceEffect.Play();
    }
}
