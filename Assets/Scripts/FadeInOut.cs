using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInOut : MonoBehaviour
{


    float timeToFade = 0;
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        EventManager.AddListener(EventType.FadeIn, OnFadeIn);
        EventManager.AddListener(EventType.FadeOut, OnFadeOut);

    }

    private void OnFadeOut(object time)
    {
        timeToFade = (float)(time);

        gameObject.SetActive(true);

        Color color = spriteRenderer.color;
        color.a = 1;
        spriteRenderer.color = color;

        Invoke("FadeOut", 0f);
    }

    private void OnFadeIn(object time)
    {
        timeToFade = (float)(time);

        gameObject.SetActive(true);

        Color color = spriteRenderer.color;
        color.a = 0;
        spriteRenderer.color = color;

        Invoke("FadeIn", 0f);
    }

    private void FadeIn()
    {
        if (spriteRenderer.color.a < 1)
        {
            Color color = spriteRenderer.color;
            color.a += 0.01f / timeToFade;
            spriteRenderer.color = color;
            Invoke("FadeIn", timeToFade / (100f * timeToFade));
        }
        else
        {
            EventManager.TriggerEvent(EventType.EndFadeIn, null);
        }
    }

    private void FadeOut()
    {
        if (spriteRenderer.color.a > 0)
        {
            Color color = spriteRenderer.color;
            color.a -= 0.01f / timeToFade;
            spriteRenderer.color = color;
            Invoke("FadeOut", timeToFade / (100f * timeToFade));
        }
        else
        {
            EventManager.TriggerEvent(EventType.EndFadeOut, null);
            gameObject.SetActive(false);
        }
    }
}

