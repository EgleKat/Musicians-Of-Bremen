using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class FadeInOut : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        EventManager.AddListener(EventType.FadeIn, OnFadeIn);
        EventManager.AddListener(EventType.FadeOut, OnFadeOut);

    }

    private void OnFadeOut(object time)
    {
        float timeToFade = (float)(time);

        gameObject.SetActive(true);

        Color color = spriteRenderer.color;
        color.a = 1;
        spriteRenderer.color = color;

        FadeOut(timeToFade);
    }

    private void OnFadeIn(object time)
    {
        float timeToFade = (float)(time);

        gameObject.SetActive(true);

        Color color = spriteRenderer.color;
        color.a = 0;
        spriteRenderer.color = color;

        FadeIn(timeToFade);
    }

    private async void FadeIn(float timeToFade)
    {
        while (spriteRenderer.color.a < 1)
        {
            Color color = spriteRenderer.color;
            color.a += 0.01f / timeToFade;
            spriteRenderer.color = color;
            await Task.Delay(TimeSpan.FromSeconds(0.01));
        }
        EventManager.TriggerEvent(EventType.EndFadeIn, null);
    }

    private async void FadeOut(float timeToFade)
    {
        while (spriteRenderer.color.a > 0)
        {
            Color color = spriteRenderer.color;
            color.a -= 0.01f / timeToFade;
            spriteRenderer.color = color;
            await Task.Delay(TimeSpan.FromSeconds(0.01));
        }
        EventManager.TriggerEvent(EventType.EndFadeOut, null);
        gameObject.SetActive(false);
    }
}

