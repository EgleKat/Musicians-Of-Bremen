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

    //Cat house
    private Conversation catWindowConvo = new Conversation("CatWindow", new Monologue[] { new Monologue("Donkey", "No light in the window.") });
    private Conversation catWindowWithDogLightConvo = new Conversation("CatWindowLight", new Monologue[] { new Monologue("Donkey", "Cat's owners seem to be home."), new Monologue("???", "...The cat's old, he doesn't catch mice anymore."), new Monologue("???", "Let's get a new one, the old one is no use to us now.") });
    private Conversation catWindowLightConvo = new Conversation("CatWindowLight", new Monologue[] { new Monologue("Donkey", "Cat's owners seem to be home."), new Monologue("???", "????") });
    private Conversation catDoorConvo = new Conversation("CatDoor", new Monologue[] { new Monologue("Donkey", "Cat's house.") });

    //Dog
    private Conversation dogMazeEnterConvo = new Conversation("MazeStart", new Monologue[] { new Monologue("???", "Heeelp!"), new Monologue("Donkey", "Who is it?"), new Monologue("???", "It's me - Dog! My owner locked me in here and left me to die. Please, help me get out of here."), new Monologue("Donkey", "Why is it so dark in here?"), new Monologue("Dog", "All of the plants are obstructing the light. It's a real maze in here!") });
    private Conversation firstDogConvo = new Conversation("Dog", new Monologue[] { new Monologue("Dog", "Oh thank you!"), new Monologue("Donkey", "Do you want to come with me?"), new Monologue("Dog", "That would be great! Over the years, I've managed to understand some human language. Too bad I can't speak it though..."), new Monologue("Donkey", "Look, I think the sun is right above, it's getting brighter!") });
    private Conversation dogHouseConvo = new Conversation("DogHouse", new Monologue[] { new Monologue("Donkey", "It's Dog's house. Strange, I don't see her anywhere...") });


    //Rooster
    private Conversation cockCageConvo = new Conversation("Rooster", new Monologue[] { new Monologue("Rooster", "It's you!"), new Monologue("Donkey", "Why are you locked up?"), new Monologue("Rooster", "My owners wanted to eat me but I managed to fly away."), new Monologue("Rooster", "I couldn't fly any further and decided to rest here, but when I woke up, the door closed on me."), new Monologue("Rooster", " Can you find out the combination for the door?") });
    private Conversation freedCockConvo = new Conversation("Rooster", new Monologue[] { new Monologue("Donkey", "Well, that was a pickle"), new Monologue("Rooster", "Oh thank you! I thought I was gonna die here. Can I join you?"), new Monologue("Donkey", "Yes, of course!") });

    private bool firstTimeOwnerHouse = true;
    private bool firstTimeWitch = true;
    private bool haveDog = false;
    private bool translatedCatsOwners = false;
    private bool firstTimeCat = true;
    private bool firstTimeMaze = true;
    private bool cockSaved = false;

    private string endOfFollowerQueue = "Ass";

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
                EventManager.TriggerEvent(EventType.AddFollower, new String[] { endOfFollowerQueue, "Cat" });
                endOfFollowerQueue = "Cat";
                GameObject.Find("Cat").GetComponent<CircleCollider2D>().enabled = false;
            }

            firstTimeCat = false;
        }
        else if (interactionName == "MazeStart")
        {
            EventManager.TriggerEvent(EventType.ShowObject, "Darkness");
            if (firstTimeMaze)
            {
                Conversation converse = GetConversation(interactionName);
                EventManager.TriggerEvent(EventType.DisplayDialogue, converse);
                await EventManager.WaitForEvent(EventType.EndDialogue);
                firstTimeMaze = false;
            }
        }
        else if (interactionName == "MazeEnd" || interactionName == "MazeEndEarly")
        {
            EventManager.TriggerEvent(EventType.HideObject, "Darkness");
        }
        else if (interactionName == "Dog")
        {
            Conversation converse = GetConversation(interactionName);
            EventManager.TriggerEvent(EventType.DisplayDialogue, converse);
            await EventManager.WaitForEvent(EventType.EndDialogue);

            EventManager.TriggerEvent(EventType.AddFollower, new String[] { endOfFollowerQueue, "Dog" });
            endOfFollowerQueue = "Dog";
            GameObject.Find("Dog").GetComponent<CircleCollider2D>().enabled = false;
            haveDog = true;

        }
        else if (interactionName == "CatDoor" || interactionName == "CatWindow" || interactionName == "DogHouse" || interactionName == "Rooster")
        {
            Conversation converse = GetConversation(interactionName);
            EventManager.TriggerEvent(EventType.DisplayDialogue, converse);
            await EventManager.WaitForEvent(EventType.EndDialogue);
        }
        else if (interactionName == "CatWindowLight")
        {
            if (haveDog)
            {
                translatedCatsOwners = true;
            }
            Conversation converse = GetConversation(interactionName);
            EventManager.TriggerEvent(EventType.DisplayDialogue, converse);
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
        else if (triggerName == "MazeStart" && firstTimeMaze)
        {
            return dogMazeEnterConvo;
        }
        else if (triggerName == "Dog")
        {
            return firstDogConvo;
        }
        else if (triggerName == "CatWindow") { return catWindowConvo; }
        else if (triggerName == "CatWindowLight")
        {
            if (haveDog) return catWindowWithDogLightConvo;
            else return catWindowLightConvo;
        }
        else if (triggerName == "CatDoor") { return catDoorConvo; }
        else if (triggerName == "DogHouse")
        {
            return dogHouseConvo;
        }
        else if (triggerName == "Rooster")
        {
            if (cockSaved)
                return freedCockConvo;
            else return cockCageConvo;
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
