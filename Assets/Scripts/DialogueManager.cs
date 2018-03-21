using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {
    private readonly static string[] dialogue =
    {
        "?",
        "??",
        "???",
        "????",
    };

    private Text dialogueText;

    private string inDialogueWith = null;
    private int dialoguePosition = 0;

    private void Awake()
    {
        dialogueText = GetComponentInChildren<Text>();

        EventManager.AddListener(EventType.StartDialogueWith, OnStartDialogueWith);
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
        if (dialoguePosition < dialogue.Length)
        {
            dialogueText.text = inDialogueWith + ":\n" + dialogue[dialoguePosition];
            dialoguePosition++;
        }
        else
        {
            gameObject.SetActive(false);
            EventManager.RemoveListener(EventType.PressedInteractKey, AdvanceDialogue);
            EventManager.TriggerEvent(EventType.EndDialogueWith, inDialogueWith);
            inDialogueWith = null;
        }
    }
}
