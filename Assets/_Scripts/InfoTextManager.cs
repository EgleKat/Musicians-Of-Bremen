using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoTextManager : MonoBehaviour
{
    private Text dialogueText;
    private float waitForSeconds = 4;

    private void Awake()
    {
        dialogueText = GetComponentInChildren<Text>();
        EventManager.AddListener(EventType.DisplayInfoMessage, OnDisplayInfoMessage);
    }

    private async void OnDisplayInfoMessage(object text)
    {
        //Change text
        String textToDisplay = (String)text;
        dialogueText.text = textToDisplay;

        //Fade in
        // EventManager.TriggerEvent(EventType.FadeIn, new FadeCommand("InfoBox", 0.5f));
        //await EventManager.WaitForEvent(EventType.EndFadeIn);
        gameObject.SetActive(true);

        //Wait for seconds
        await Wait.ForSeconds(waitForSeconds);

        //Fade out
        //EventManager.TriggerEvent(EventType.FadeOut, new FadeCommand("InfoBox", 0.5f));
        gameObject.SetActive(false);

    }

}
