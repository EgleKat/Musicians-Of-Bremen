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

    private void OnFadeOut(object fadeCommand)
    {
        FadeCommand command = (FadeCommand)(fadeCommand);

        if (command.gameObjectToFade != gameObject.name)
        {
            return;
        }

        gameObject.SetActive(true);

        FadeOut(command.timeToFade);
    }

    private void OnFadeIn(object fadeCommand)
    {
        FadeCommand command = (FadeCommand)(fadeCommand);

        if (command.gameObjectToFade != gameObject.name)
        {
            return;
        }

        gameObject.SetActive(true);

        FadeIn(command.timeToFade);
    }

    private async void FadeIn(float timeToFade)
    {
        while (spriteRenderer.color.a < 1)
        {
            Color color = spriteRenderer.color;
            color.a += 0.01f / timeToFade;
            spriteRenderer.color = color;
            await Wait.ForSeconds(0.01f);
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
            await Wait.ForSeconds(0.01f);
        }
        EventManager.TriggerEvent(EventType.EndFadeOut, null);
        gameObject.SetActive(false);
    }
}

