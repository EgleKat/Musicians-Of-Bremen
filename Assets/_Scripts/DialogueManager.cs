using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    private Conversation currentConversation;

    private Text dialogueText;

    private int dialoguePosition = 0;

    private void Awake()
    {
        dialogueText = GetComponentInChildren<Text>();

        EventManager.AddListener(EventType.DisplayDialogue, OnDisplayDialogue);
    }

    private void OnDisplayDialogue(object conversation)
    {
        currentConversation = (Conversation)conversation;
        EventManager.TriggerEvent(EventType.DisableMovement, null);
        EventManager.TriggerEvent(EventType.FadeIn, new FadeCommand("Dialogue Box", 0.2f));
        dialoguePosition = 0;
        EventManager.AddListener(EventType.PressedInteractKey, AdvanceDialogue);
        AdvanceDialogue(null);

    }

    private void AdvanceDialogue(object _)
    {
        if (currentConversation != null && dialoguePosition < currentConversation.dialogue.Length)
        {
            dialogueText.text = currentConversation.dialogue[dialoguePosition].speaker + ":\n" + currentConversation.dialogue[dialoguePosition].text;
            dialoguePosition++;
        }
        else
        {
            EventManager.TriggerEvent(EventType.FadeOut, new FadeCommand("Dialogue Box", 0.2f));
            EventManager.RemoveListener(EventType.PressedInteractKey, AdvanceDialogue);
            currentConversation = null;
            EventManager.TriggerEvent(EventType.EnableMovement, null);
            EventManager.TriggerEvent(EventType.EndDialogue, null);
        }
    }
}
