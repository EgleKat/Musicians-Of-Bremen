using System;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StoryManager : MonoBehaviour
{
    //Donkey owner
    private Conversation donkOwnerConvo = new Conversation("DonkeyHouseOuter", new Monologue[] { new Monologue("Donkey", "Hmm.. What is owner doing? "), new Monologue("Owner", "????"), new Monologue("Donkey", "Strange... He chopped some wood yesterday.. What is he doing with the axe?") });
    private Conversation donkOwnerConvoTwo = new Conversation("DonkeyHouseOuter", new Monologue[] { new Monologue("Donkey", "My old home..."), new Monologue("Donkey", "I'll go back to it when I'm ready to die.") });
    private Conversation observeDonkeyHouseConvo = new Conversation("DonkeyHouse", new Monologue[] { new Monologue("Donkey", "My owner's home.. Wonder what he's doing now..") });

    //Witch
    private Conversation talkToWitchFirstConvo = new Conversation("Witch", new Monologue[] { new Monologue("Donkey", "Who.. Who are you?"), new Monologue("Witch", "???"), new Monologue("Donkey", "You saved me.. Thank you!"), new Monologue("Donkey", "I don't understand what you're saying. I can't understand human."), new Monologue("Witch", "???"), new Monologue("Donkey", "I guess I'll head off.") });
    private Conversation talkToWitchConvo = new Conversation("Witch", new Monologue[] { new Monologue("Donkey", "Thank you again for saving my life."), new Monologue("Witch", "???") });
    private Conversation talkToWitchConvoWithDog = new Conversation("Witch", new Monologue[] { new Monologue("Donkey", "Thank you again for saving my life."), new Monologue("Witch", "I can sense your future, little one"), new Monologue("Witch", "If you want your path to be easier, find the life's orange.") });

    //Cat
    private Conversation firstCatConvo = new Conversation("Cat", new Monologue[] { new Monologue("Cat", "Hello, Ass, what happened to you?"), new Monologue("Donkey", "My owner went crazy, tried to kill me! You should watch out,  your masters might do the same."), new Monologue("Donkey", "Come with me if you want to  have a chance of life."), new Monologue("Cat", "Pftt my owners would never  do such a thing."), new Monologue("Cat", "Unlike you, I am a superior being and they worship me."), new Monologue("Donkey", "Hmm, If only I could make you believe.") });
    private Conversation catConvo = new Conversation("Cat", new Monologue[] { new Monologue("Cat", "Get lost.."), new Monologue("Donkey", "If only I could make you believe.") });
    private Conversation catJoinConvo = new Conversation("Cat", new Monologue[] { new Monologue("Donkey", "Hey, I know you don't  think  your owners  want any harm to you, but  hear us out. "), new Monologue("Cat", "Fine, go on with it."), new Monologue("Dog", "Your owners... they said they don't want you anymore, you're too old for them."), new Monologue("Cat", "w...w...what? This is absurd!"), new Monologue("Dog", "It's true, I can understand human language."), new Monologue("Cat", "Oh no...  And all this time I thought they  adored me... Is there still space in your travel group Donkey?"), new Monologue("Donkey", "Of course, let's go.") });

    private bool firstTimeOwnerHouse = true;
    private bool firstTimeWitch = true;
    private bool haveDog = false;
    private bool translatedCatsOwners = false;
    private bool firstTimeCat = true;


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
                await Task.Delay(TimeSpan.FromSeconds(2f));
                EventManager.TriggerEvent(EventType.StartInteraction, "Witch");
                await EventManager.WaitForEvent(EventType.EndFadeOut);
                EventManager.TriggerEvent(EventType.ChangeMusic, "background");
                await EventManager.WaitForEvent(EventType.EndDialogue);

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
        else if (interactionName == "Witch")
        {
            //start dialog
            Conversation converse = GetConversation(interactionName);
            EventManager.TriggerEvent(EventType.DisplayDialogue, converse);
            await EventManager.WaitForEvent(EventType.EndDialogue);

            firstTimeWitch = false;
        }
        else if (interactionName == "Cat")
        {
            //start dialog
            Conversation converse = GetConversation(interactionName);
            EventManager.TriggerEvent(EventType.DisplayDialogue, converse);
            await EventManager.WaitForEvent(EventType.EndDialogue);

            if (!firstTimeCat && translatedCatsOwners)
            {
                EventManager.TriggerEvent(EventType.AddFollower, new String[] { "Ass", "Cat" });
                GameObject.Find("Cat").GetComponent<CircleCollider2D>().enabled = false;
            }

            firstTimeCat = false;
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
        else if (triggerName == "Witch")
        {
            if (firstTimeWitch)
                return talkToWitchFirstConvo;
            else if (haveDog)
                return talkToWitchConvoWithDog;
            else return talkToWitchConvo;
        }
        else if (triggerName == "Cat")
        {

            if (firstTimeCat)
                return firstCatConvo;
            else if (translatedCatsOwners)
                return catJoinConvo;
            else return catConvo;
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
