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
    public AudioClip br_1;
    public AudioClip br_2;
    public AudioClip br_3;
    public AudioClip correctSimon;
    public AudioClip incorrectSimon;
    public AudioClip unlock;
    public AudioClip fight;
    public AudioClip happyEnd;
    public AudioClip heart;
    public AudioClip funeral;

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
        else if (audioName == "funeral")
        {
            audioSourceBackground.clip = funeral;

        }
        else if (audioName == "background")
        {
            audioSourceBackground.clip = background;
        }
        else if (audioName == "fight")
        {
            audioSourceBackground.clip = fight;

        }
        else if (audioName == "happyEnd")
        {
            audioSourceBackground.clip = happyEnd;

        }
        if (audioName != "stop")
            audioSourceBackground.Play();
    }
    private void OnPlaySound(object nameAudio)
    {
        String audioName = (String)nameAudio;
        audioSourceEffect.volume = 1;

        if (audioName == "hit")
        {
            audioSourceEffect.clip = hit;
        }
        else if (audioName == "ting")
        {
            audioSourceEffect.clip = ting;
        }
        else if (audioName == "BR_1")
        {
            audioSourceEffect.clip = br_1;

        }
        else if (audioName == "BR_2")
        {
            audioSourceEffect.clip = br_2;

        }
        else if (audioName == "BR_3")
        {
            audioSourceEffect.clip = br_3;

        }
        else if (audioName == "correctSimon")
        {

            audioSourceEffect.clip = correctSimon;

        }
        else if (audioName == "incorrectSimon")
        {
            audioSourceEffect.clip = incorrectSimon;

        }
        else if (audioName == "unlock")
        {
            audioSourceEffect.volume = 2;
            audioSourceEffect.clip = unlock;
        }
        else if (audioName == "heart")
        {
            audioSourceEffect.clip = heart;
        }

        audioSourceEffect.Play();
    }
}
