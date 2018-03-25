using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StoryManager : MonoBehaviour
{

    private Conversation donkOwnerConvo = new Conversation("DonkeyHouseOuter", new Monologue[] { new Monologue("Donkey", "Chopping some wood again, I see."), new Monologue("Owner", "????") });

    void Awake()
    {

        EventManager.AddListener(EventType.TriggerCollide, OnTriggerCollide);
    }

    private void OnTriggerCollide(object objName)
    {
        string name = (String)objName;

        if (name == "DonkeyHouseOuter")
        {
            //show owner
            EventManager.TriggerEvent(EventType.ShowObject, "Donkey Owner");
            //move owner down
            EventManager.TriggerEvent(EventType.Move, new MoveCommand("Donkey Owner", new Vector3(-200, -100, 0), MoveCommand.MoveType.Location));
            //start dialog
            Conversation converse = GetConversation(name);
            EventManager.TriggerEvent(EventType.DisplayDialogue, converse);
            UnityAction<object> onEndDialogue = null;
            onEndDialogue = delegate (object _)
            {
                EventManager.RemoveListener(EventType.EndDialogue, onEndDialogue);
                EventManager.TriggerEvent(EventType.AddFollower, new string[] { "Ass", "Donkey Owner" });
                UnityAction<object> onTriggerCollide = null;
                onTriggerCollide = delegate (object collideName)
                {
                    if ((String)collideName == "Donkey Owner")
                    {
                        EventManager.RemoveListener(EventType.TriggerCollide, onTriggerCollide);
                        EventManager.TriggerEvent(EventType.RemoveFollower, collideName);
                        EventManager.TriggerEvent(EventType.HideObject, "Donkey Owner");
                        EventManager.TriggerEvent(EventType.StopMoving, "Ass");
                        EventManager.TriggerEvent(EventType.Teleport, new MoveCommand("Ass", new Vector3(930, 1000, 0), MoveCommand.MoveType.Location));
                    }
                };
                EventManager.AddListener(EventType.TriggerCollide, onTriggerCollide);
            };
            EventManager.AddListener(EventType.EndDialogue, onEndDialogue);
        }
    }
    private Conversation GetConversation(string triggerName)
    {
        return donkOwnerConvo;
    }
}

public class Conversation
{
    public String conversationTrigger;
    public Monologue[] dialogue;
    //TODO have a condition variable
    public Conversation(String conversationTrigger, Monologue[] dialogue)
    {
        this.conversationTrigger = conversationTrigger;
        this.dialogue = dialogue;
    }

}

public class Monologue
{
    public String speaker;
    public String text;

    public Monologue(String speaker, string text)
    {
        this.speaker = speaker;
        this.text = text;
    }
}