using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour
{
    public float alpha = 0;

    List<SpriteRenderer> spriteRenderers = new List<SpriteRenderer>();
    List<Image> images = new List<Image>();
    List<Text> texts = new List<Text>();

    bool fadingOut = false;
    bool fadingIn = false;

    private void Awake()
    {
        spriteRenderers.AddRange(GetComponents<SpriteRenderer>());
        spriteRenderers.AddRange(GetComponentsInChildren<SpriteRenderer>());

        images.AddRange(GetComponents<Image>());
        images.AddRange(GetComponentsInChildren<Image>());

        texts.AddRange(GetComponents<Text>());
        texts.AddRange(GetComponentsInChildren<Text>());

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
        fadingOut = true;
        fadingIn = false;
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
        fadingIn = true;
        fadingOut = false;
        FadeIn(command.timeToFade);
    }

    private async void FadeIn(float timeToFade)
    {
        while (alpha < 1 && fadingIn)
        {
            SetAlpha(alpha + 0.01f / timeToFade);
            await Wait.ForSeconds(0.01f);
        }
        fadingIn = false;
        EventManager.TriggerEvent(EventType.EndFadeIn, null);
    }

    private async void FadeOut(float timeToFade)
    {
        while (alpha > 0 && fadingOut)
        {
            SetAlpha(alpha - 0.01f / timeToFade);
            await Wait.ForSeconds(0.01f);
        }
        gameObject.SetActive(false);
        fadingOut = false;
        EventManager.TriggerEvent(EventType.EndFadeOut, null);
    }

    private void SetAlpha(float alpha)
    {
        this.alpha = alpha;
        foreach (SpriteRenderer sr in spriteRenderers)
        {
            Color c = sr.color;
            c.a = alpha;
            sr.color = c;
        }
        foreach (Image sr in images)
        {
            Color c = sr.color;
            c.a = alpha;
            sr.color = c;
        }
        foreach (Text sr in texts)
        {
            Color c = sr.color;
            c.a = alpha;
            sr.color = c;
        }
    }
}

