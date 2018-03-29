using System;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StoryManager : MonoBehaviour
{

    private Conversation donkOwnerConvo = new Conversation("DonkeyHouseOuter", new Monologue[] { new Monologue("Donkey", "Hmm.. What is owner doing? "), new Monologue("Owner", "????"), new Monologue("Donkey", "Strange... He chopped some wood yesterday.. What is he doing with the axe?") });
    private Conversation donkOwnerConvoTwo = new Conversation("DonkeyHouseOuter", new Monologue[] { new Monologue("Donkey", "My old home..."), new Monologue("Donkey", "I'll go back to it when I'm ready to ready to die.") });
    private Conversation observeDonkeyHouseConvo = new Conversation("DonkeyHouse", new Monologue[] { new Monologue("Donkey", "My owner's home.. Wonder what he's doing now..") });
    private bool firstTimeOwnerHouse = true;

    void Awake()
    {
        EventManager.AddListener(EventType.StartInteraction, OnStartInteraction);
    }

    private async void OnStartInteraction(object objName)
    {
        string interactionName = (String)objName;

        if (interactionName == "DonkeyHouseOuter")
        {
            if (firstTimeOwnerHouse)
            {
                //show owner
                EventManager.TriggerEvent(EventType.ShowObject, "Donkey Owner");
                //move owner down
                EventManager.TriggerEvent(EventType.Move, new MoveCommand("Donkey Owner", new Vector3(-200, -100, 0), MoveCommand.MoveType.Location));
                //start dialog
                Conversation converse = GetConversation(interactionName);
                EventManager.TriggerEvent(EventType.DisplayDialogue, converse);
                await EventManager.WaitForEvent(EventType.EndDialogue);

                firstTimeOwnerHouse = false;

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
                EventManager.TriggerEvent(EventType.Teleport, new MoveCommand("Ass", new Vector3(1190, 710, 0), MoveCommand.MoveType.Location));
                await Task.Delay(TimeSpan.FromSeconds(1.5));

                //make beginner marker a collider (not a trigger)
                // TODO: use events?
                GameObject donkeyOuter = GameObject.Find("DonkeyHouseOuter");
                donkeyOuter.GetComponent<BoxCollider2D>().isTrigger = false;

                EventManager.TriggerEvent(EventType.FadeOut, 6f);
                await EventManager.WaitForEvent(EventType.EndFadeOut);
                EventManager.TriggerEvent(EventType.ChangeMusic, "background");
            }
            else
            {
                //start dialog
                Conversation converse = GetConversation(interactionName);
                EventManager.TriggerEvent(EventType.DisplayDialogue, converse);
                await EventManager.WaitForEvent(EventType.EndDialogue);

            }
        }
        else if (interactionName == "DonkeyHouse")
        {
            //observe house
            EventManager.TriggerEvent(EventType.DisplayDialogue, GetConversation(interactionName));
            await EventManager.WaitForEvent(EventType.EndDialogue);
        }

        EventManager.TriggerEvent(EventType.EndInteraction, interactionName);
    }

    private Conversation GetConversation(string triggerName)
    {
        if (triggerName == "DonkeyHouseOuter")
        {
            if (firstTimeOwnerHouse)
                return donkOwnerConvo;
            else return donkOwnerConvoTwo;
        }
        else if (triggerName == "DonkeyHouse")
        {
            return observeDonkeyHouseConvo;
        }
        else
        {
            Debug.LogError("Can't find dialogue.");
            return null;
        }
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
