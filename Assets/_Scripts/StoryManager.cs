using System;
using System.Threading.Tasks;
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

    private async void OnTriggerCollide(object objName)
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
            await EventManager.WaitForEvent(EventType.EndDialogue);
            EventManager.TriggerEvent(EventType.AddFollower, new string[] { "Ass", "Donkey Owner" });
            EventManager.TriggerEvent(EventType.ChangeMusic, "danger");
            string colliderName;
            do
            {
                colliderName = (string)await EventManager.WaitForEvent(EventType.TriggerCollide);
            } while (colliderName != "Donkey Owner");
            EventManager.TriggerEvent(EventType.PlaySound, "hit");
            EventManager.TriggerEvent(EventType.RemoveFollower, "Donkey Owner");
            EventManager.TriggerEvent(EventType.HideObject, "Donkey Owner");
            EventManager.TriggerEvent(EventType.StopMoving, "Ass");
            EventManager.TriggerEvent(EventType.ChangeMusic, "stop");
            //white screen
            EventManager.TriggerEvent(EventType.FadeIn, 0.1f);
            await EventManager.WaitForEvent(EventType.EndFadeIn);
            EventManager.TriggerEvent(EventType.Teleport, new MoveCommand("Ass", new Vector3(930, 1000, 0), MoveCommand.MoveType.Location));
            await Task.Delay(TimeSpan.FromSeconds(1.5));
            EventManager.TriggerEvent(EventType.FadeOut, 6f);
            await EventManager.WaitForEvent(EventType.EndFadeOut);
            EventManager.TriggerEvent(EventType.ChangeMusic, "background");
        }
    }

    private Conversation GetConversation(string triggerName)
    {
        return donkOwnerConvo;
    }

    IEnumerator RunAfterTime(float time, Action action)
    {
        yield return new WaitForSeconds(time);
        action();
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
