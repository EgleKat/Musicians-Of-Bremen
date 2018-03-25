using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    private readonly static string[] dialogue =
    {
        "?",
        "??",
        "???",
        "????",
    };

    private Conversation currentConversation;

    private Text dialogueText;

    private string inDialogueWith = null;
    private int dialoguePosition = 0;

    private void Awake()
    {
        dialogueText = GetComponentInChildren<Text>();

        EventManager.AddListener(EventType.StartDialogueWith, OnStartDialogueWith);
        EventManager.AddListener(EventType.DisplayDialogue, OnDisplayDialogue);

    }

    private void OnDisplayDialogue(object conversation)
    {
        currentConversation = (Conversation)conversation;
        EventManager.TriggerEvent(EventType.DisableMovement, null);
        gameObject.SetActive(true);
        dialoguePosition = 0;
        EventManager.AddListener(EventType.PressedInteractKey, AdvanceDialogue);
        AdvanceDialogue(null);

    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void OnStartDialogueWith(object npcName)
    {
        if (inDialogueWith == null)
        {
            gameObject.SetActive(true);
            inDialogueWith = (string)npcName;
            dialoguePosition = 0;
            EventManager.AddListener(EventType.PressedInteractKey, AdvanceDialogue);

            AdvanceDialogue(null);
        }
    }

    private void AdvanceDialogue(object _)
    {
        if (dialoguePosition < currentConversation.dialogue.Length)
        {
            dialogueText.text = currentConversation.dialogue[dialoguePosition].speaker + ":\n" + currentConversation.dialogue[dialoguePosition].text;
            dialoguePosition++;
        }
        else
        {
            gameObject.SetActive(false);
            EventManager.RemoveListener(EventType.PressedInteractKey, AdvanceDialogue);
            EventManager.TriggerEvent(EventType.EnableMovement, null);
            EventManager.TriggerEvent(EventType.EndDialogue, inDialogueWith);
            currentConversation = null;
        }
    }
}
