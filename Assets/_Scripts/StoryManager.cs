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
    private Conversation talkToWitchFirstConvo = new Conversation("Witch", new Monologue[] { new Monologue("Donkey", "Who.. Who are you?"), new Monologue("Witch", "???"), new Monologue("Donkey", "You saved me.. Thank you!"), new Monologue("Donkey", "I don't understand what you're saying. I can't understand human."), new Monologue("Witch", "???"), new Monologue("Donkey", "I guess I'll head off. I need to work out what I want to do with my life, now that I'm homeless.") });
    private Conversation talkToWitchConvo = new Conversation("Witch", new Monologue[] { new Monologue("Donkey", "Thank you again for saving my life."), new Monologue("Witch", "???") });
    private Conversation talkToWitchConvoWithDog = new Conversation("Witch", new Monologue[] { new Monologue("Donkey", "Thank you again for saving my life."), new Monologue("Witch", "You have a strong spirit Donkey, don't waste it. I saved your life so you can save others'."), new Monologue("Witch", "Every life you save will aid you in the future."), new Monologue("Witch", "Oh, and I hid something for you around your owners' house. Look in the bushes.") });

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
    private Conversation dogHouseConvoWithDog = new Conversation("DogHouse", new Monologue[] { new Monologue("Donkey", "It is Dog's house... well, was...") });


    //Rooster
    private Conversation cockCageConvo = new Conversation("Rooster", new Monologue[] { new Monologue("Rooster", "It's you!"), new Monologue("Donkey", "Why are you locked up?"), new Monologue("Rooster", "My owners wanted to eat me but I managed to fly away."), new Monologue("Rooster", "I couldn't fly any further and decided to rest here, but when I woke up, the door closed on me."), new Monologue("Rooster", " Can you find out the combination for the door? Match the patterns of the buttons.") });
    private Conversation freedCockConvo = new Conversation("Rooster", new Monologue[] { new Monologue("Donkey", "Well, that was a pickle."), new Monologue("Rooster", "Oh thank you! I thought I was gonna die here. Can I join you?"), new Monologue("Donkey", "Yes, of course!"), new Monologue("Rooster", "My speaking skills might come in handy. I can speak in the human language."), new Monologue("Rooster", "Although I don't really understand it, I've learnt to imitate it.") });
    private Conversation cockCageWithDogConvo = new Conversation("Rooster", new Monologue[] { new Monologue("Rooster", "It's you!"), new Monologue("Dog", "Oh, a hen!"), new Monologue("Rooster", "I'm a cock, not a hen! I'm just a bit chubby. "), new Monologue("Donkey", "Why are you locked up?"), new Monologue("Rooster", "My owners wanted to eat me but I managed to fly away."), new Monologue("Rooster", "I couldn't fly any further and decided to rest here, but when I woke up, the door closed on me."), new Monologue("Rooster", " Can you find out the combination for the door?") });

    //Extra
    private Conversation dangerSignConvo = new Conversation("DangerSign", new Monologue[] { new Monologue("Sign", "   DANGER    AHEAD") });
    private Conversation donkeySaysUseful = new Conversation("", new Monologue[] { new Monologue("Donkey", "This might prove useful later.") });
    private Conversation donkeyNothingToSeeHere = new Conversation("", new String[] { "Donkey", "Nothing to see here." });
    private Conversation robberHouseDoorNoEntry = new Conversation("", new string[] { "Donkey", "The door is locked." });

    //Robber house
    private Conversation robberHouseStartConvo1 = new Conversation("RobberHouseStart1", new string[] { "Donkey", "Look, it's a house. There's a light in the window." });
    private Conversation robberHouseStartConvo2Dog = new Conversation("RobberHouseStart2Dog", new string[] { "Donkey", "Hmm, dog can you try to  hear anything useful?", "Dog", "I'll try.", "???", "That was a nice load we got from that caravan, hahaha.", "Dog", "I think they are robbers.", "Donkey", "Hmm, I have an idea, what if we take their house when they leave?", "Dog", "WAIT! There's more!", "Robber", "I found out where our animals are kept, we can finally take them back from those  thieves. Oh I remember my sweet donkey, I used to love him with all my heart...", "Dog", "Wait What!??! The robbers have been  ROBBED. Someone stole a donkey from them!", "Donkey", "Wait a second! Our owners are the thieves! We have to go in and talk to them.", "Dog", "The door is locked, so we can't go in and it's made of wool so knocking won't help.", "Donkey", "Let's wait until they come out." });
    private Conversation robberHouseStartConvo2CockOnly = new Conversation("RobberHouseStart2CockOnly", new string[] { "Rooster", "Looks a bit shabby.", "Donkey", "Hmm, I have an idea, what if we take their house when they leave?", "Rooster", "We have to make sure that they don't come back.", "Donkey", "I know! Get on my back. In the shadows we will look frightening!", "Rooster", "Good idea!" });
    private Conversation robberHouseStartConvo2CatOnly = new Conversation("RobberHouseStart2CatOnly", new string[] { "Donkey", "Hmm, I have an idea, what if we take their house when they leave?", "Cat", "The door's locked so I suppose we'll have to wait for them to come out." });
    private Conversation robberHouseStartConvo3CatNotOnly = new Conversation("RobberHouseStart3Cat", new string[] { "Cat", "Wait, I can sneak in through the window and unlock it on the inside." });

    //Robber Tree
    private Conversation robberTreeWCat = new Conversation("RobberTree", new Monologue[] { new Monologue("Cat", "I can fit through here."), new Monologue("Donkey", "See what you can find.") });
    private Conversation robberTree = new Conversation("RobberTree", new Monologue[] { new Monologue("Donkey", "What a small hole. It would need someone very small and flexible to fit through.") });

    private static readonly string[] RobberNames = { "Rob", "Bab", "Bob", "Rab" };
    private static readonly string[] RobberNamesOutside = { "RobOutside", "BabOutside", "BobOutside", "RabOutside" };

    private bool firstTimeOwnerHouse = true;
    private bool firstTimeWitch = true;
    public bool haveDog = false;
    public bool haveCock = false;
    public bool haveCat = false;
    private bool translatedCatsOwners = false;
    private bool firstTimeCat = true;
    private bool firstTimeMaze = true;
    private bool cockSaved = false;
    private bool simonSaysStarted = false;
    private bool robberHouseStarted = false;
    private bool haveBushHeart = false;

    private string endOfFollowerQueue = "Ass";
    private bool treeComplete = false;

    private Vector3 robberFrontDoor = new Vector3(5054, -23, 0);
    private Vector3 robberNearFrontDoor = new Vector3(4976, -60, 0);

    public Vector3[] robberPositionsToMoveTo;
    public Vector3[] animalPositionstoMove;

    public Camera mainCamera;

    void Awake()
    {
        EventManager.AddListener(EventType.StartInteraction, OnStartInteraction);

    }
    private void Start()
    {
        EventManager.TriggerEvent(EventType.DisplayInfoMessage, "Use WASD to move and E to interact.");
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
                EventManager.TriggerEvent(EventType.StopMoving, "Ass");
                EventManager.TriggerEvent(EventType.ChangeMusic, "stop");
                //white screen
                EventManager.TriggerEvent(EventType.FadeIn, new FadeCommand("White", 0.1f));
                await EventManager.WaitForEvent(EventType.EndFadeIn);
                EventManager.TriggerEvent(EventType.HideObject, "Donkey Owner");
                EventManager.TriggerEvent(EventType.Teleport, new MoveCommand("Ass", new Vector3(1200, 706, 0), MoveCommand.MoveType.Location));
                await Wait.ForSeconds(1.5f);

                //make beginner marker a collider (not a trigger)
                GameObject donkeyOuter = GameObject.Find("DonkeyHouseOuter");
                donkeyOuter.GetComponent<BoxCollider2D>().isTrigger = false;

                EventManager.TriggerEvent(EventType.FadeOut, new FadeCommand("White", 6f));
                await Wait.ForSeconds(2f);
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

            //Display info message
            EventManager.TriggerEvent(EventType.DisplayInfoMessage, "Explore the world.");


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
                haveCat = true;
                EventManager.TriggerEvent(EventType.AddHeart, 1);
                EventManager.TriggerEvent(EventType.DisplayInfoMessage, "Cat can climb into small spaces.");

            }
            if (firstTimeCat)
            {
                EventManager.TriggerEvent(EventType.DisplayInfoMessage, "Cat doesn't believe you.");
            }

            firstTimeCat = false;
        }
        else if (interactionName == "BushHeart")
        {
            if (!haveBushHeart)
            {
                EventManager.TriggerEvent(EventType.AddHeart, 1);
                EventManager.TriggerEvent(EventType.DisplayInfoMessage, "Received a heart.");

                EventManager.TriggerEvent(EventType.DisplayDialogue, donkeySaysUseful);
                await EventManager.WaitForEvent(EventType.EndDialogue);
                haveBushHeart = true;
            }
        }
        else if (interactionName == "MazeStart")
        {
            EventManager.TriggerEvent(EventType.FadeIn, new FadeCommand("Darkness", 1f));

            if (firstTimeMaze)
            {
                Conversation converse = GetConversation(interactionName);
                EventManager.TriggerEvent(EventType.DisplayDialogue, converse);
                await EventManager.WaitForEvent(EventType.EndDialogue);
                EventManager.TriggerEvent(EventType.DisplayInfoMessage, "Finish the maze and save dog.");
                firstTimeMaze = false;
            }
        }
        else if (interactionName == "MazeEnd" || interactionName == "MazeEndEarly")
        {
            if (!firstTimeMaze)
            {
                EventManager.TriggerEvent(EventType.FadeOut, new FadeCommand("Darkness", 1f));

            }
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
            EventManager.TriggerEvent(EventType.AddHeart, 1);
            EventManager.TriggerEvent(EventType.DisplayInfoMessage, "Dog can understand human language.");



        }
        else if (interactionName == "CatDoor" || interactionName == "CatWindow" || interactionName == "DogHouse" || interactionName == "DangerSign")
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
        else if (interactionName == "SimonSaysStart")
        {
            if (!simonSaysStarted)
            {
                simonSaysStarted = true;
                SimonSaysManager simonSaysManager = new SimonSaysManager();
                SimonSaysManager.ReturnType returnType;
                EventManager.TriggerEvent(EventType.DisplayInfoMessage, "Repeat the colour code without any mistakes 4 times.");
                do
                {
                    returnType = await simonSaysManager.Start(4);
                } while (returnType != SimonSaysManager.ReturnType.Success);

                EventManager.TriggerEvent(EventType.StartInteraction, "SimonSaysEnd");
                cockSaved = true;
            }
        }
        else if (interactionName == "Rooster")
        {
            Conversation converse = GetConversation(interactionName);
            EventManager.TriggerEvent(EventType.DisplayDialogue, converse);
            await EventManager.WaitForEvent(EventType.EndDialogue);

            if (cockSaved)
            {
                haveCock = true;
                EventManager.TriggerEvent(EventType.AddHeart, 1);

                EventManager.TriggerEvent(EventType.AddFollower, new String[] { endOfFollowerQueue, "Rooster" });
                endOfFollowerQueue = "Rooster";
                GameObject.Find("Rooster").GetComponent<CircleCollider2D>().enabled = false;
                EventManager.TriggerEvent(EventType.DisplayInfoMessage, "Rooster can speak human language.");

            }
            else
            {
                EventManager.TriggerEvent(EventType.StartInteraction, "SimonSaysStart");
            }
        }
        else if (interactionName == "RobberHouseStart")
        {
            if (!robberHouseStarted)
            {
                robberHouseStarted = true;

                if (haveDog && haveCock && haveCat)
                {
                    // listen at window
                    // go inside, talk to robbers nicely
                    await WaitForDialogue(YdogYcockYcat1);

                    await WaitForCatUnlock();
                    await EventManager.WaitForEvent(EventType.EndGoInside);
                    ToggleInteriorRobbers(true);

                    await WaitForDialogue(YdogYcockYcat2);

                    EventManager.TriggerEvent(EventType.GameOver, "happyEnding");
                }
                else if (haveDog && haveCock && !haveCat)
                {
                    // listen at window
                    // wait outside, talk to robbers nicely
                    await WaitForDialogue(YdogYcockNcat1);

                    await FadeInOutBlackNoMovement(3f, 2f);

                    // move robbers into position
                    ToggleExteriorRobbers(true);
                    await MoveMultipleSprites(robberNearFrontDoor, RobberNamesOutside, 40);
                    await MoveMultipleSprites(robberNearFrontDoor + new Vector3(0, -1, 0), RobberNamesOutside, 40);

                    await WaitForDialogue(YdogYcockNcat2);
                    EventManager.TriggerEvent(EventType.GameOver, "happyEnding");

                }
                else if (haveDog && !haveCock && haveCat)
                {
                    // listen at window
                    // go inside, try to talk to robbers, fight robbers
                    await WaitForDialogue(YdogNcockYcat1);

                    await WaitForCatUnlock();

                    await EventManager.WaitForEvent(EventType.EndGoInside);
                    ToggleInteriorRobbers(true);

                    await WaitForDialogue(YdogNcockYcat2);

                    EventManager.TriggerEvent(EventType.StartInteraction, "BossBattle");
                }
                else if (!haveDog && haveCock && !haveCat)
                {
                    // wait outside, fight robbers to steal house
                    await WaitForDialogue(NdogYcockNcat1);

                    await FadeInOutBlackNoMovement(3f, 2f);

                    //activate scare mode - TODO: make this scarier
                    EventManager.TriggerEvent(EventType.RemoveFollower, "Rooster");
                    Vector3 donkeyPosition = GameObject.Find("Ass").transform.position;
                    GameObject.Find("Rooster").transform.position = donkeyPosition + new Vector3(16, 16, 0);
                    EventManager.TriggerEvent(EventType.StartRotating, "Rooster");

                    // robbers come outside
                    UnlockRobberHouse();
                    ToggleExteriorRobbers(true);
                    MoveMultipleSpritesInstantly(robberFrontDoor, RobberNamesOutside, 0);
                    await MoveMultipleSprites(robberNearFrontDoor, RobberNamesOutside, 40);
                    await MoveMultipleSprites(robberNearFrontDoor + new Vector3(0, -1, 0), RobberNamesOutside, 40);

                    await WaitForDialogue(NdogYcockNcat2);

                    //robbers run away
                    MoveMultipleSpritesNoWaiting(Vector3.zero, RobberNamesOutside, 0);

                    EventManager.TriggerEvent(EventType.AddFollower, new string[] { "Ass", "Rooster" });
                    EventManager.TriggerEvent(EventType.EndRotating, "Rooster");

                    await EventManager.WaitForEvent(EventType.EndGoInside);
                    // ToggleInteriorRobbers(false);

                    // nap
                    await WaitForDialogue(NdogYcockNcat3);
                    await FadeInOutBlackNoMovement(3f, 2f);

                    //robbers come in again
                    ToggleInteriorRobbers(true);
                    MoveMultipleSpritesInstantly(robberFrontDoor, RobberNames, 0);
                    await MoveMultipleSprites(robberNearFrontDoor, RobberNames, 40);
                    await MoveMultipleSprites(robberNearFrontDoor + new Vector3(0, 1, 0), RobberNames, 40);

                    await WaitForDialogue(NdogYcockNcat4);

                    EventManager.TriggerEvent(EventType.StartInteraction, "BossBattle");
                }
                else if (haveDog && !haveCock && !haveCat)
                {
                    // listen at window
                    // wait outside, try to talk to robbers but they don't understand, end up fighting
                    await WaitForDialogue(YdogNcockNcat1);

                    await FadeInOutBlackNoMovement(3f, 2f);

                    // robbers come out
                    UnlockRobberHouse();
                    ToggleExteriorRobbers(true);
                    await MoveMultipleSprites(robberNearFrontDoor, RobberNamesOutside, 40);
                    await MoveMultipleSprites(robberNearFrontDoor + new Vector3(0, -1, 0), RobberNamesOutside, 40);
                    await WaitForDialogue(YdogNcockNcat2);
                    //go back in
                    await MoveMultipleSprites(robberFrontDoor, RobberNamesOutside, 0);
                    await Wait.ForSeconds(0.5f);
                    ToggleExteriorRobbers(false);

                    await EventManager.WaitForEvent(EventType.EndGoInside);
                    ToggleInteriorRobbers(true);

                    await WaitForDialogue(YdogNcockNcat3);

                    EventManager.TriggerEvent(EventType.StartInteraction, "BossBattle");
                }
                else if (!haveDog && !haveCock && !haveCat)
                {
                    // wait outside, robbers fight because they don't understand
                    await WaitForDialogue(NdogNcockNcat1);

                    await FadeInOutBlackNoMovement(3f, 2f);

                    // robbers come out
                    UnlockRobberHouse();
                    ToggleExteriorRobbers(true);
                    await MoveMultipleSprites(robberNearFrontDoor, RobberNamesOutside, 40);
                    await MoveMultipleSprites(robberNearFrontDoor + new Vector3(0, -1, 0), RobberNamesOutside, 40);
                    await WaitForDialogue(NdogNcockNcat2);
                    //go back in
                    await MoveMultipleSprites(robberFrontDoor, RobberNamesOutside, 0);
                    await Wait.ForSeconds(0.5f);
                    ToggleExteriorRobbers(false);

                    await EventManager.WaitForEvent(EventType.EndGoInside);
                    ToggleInteriorRobbers(true);


                    await WaitForDialogue(NdogNcockNcat3);

                    EventManager.TriggerEvent(EventType.StartInteraction, "BossBattle");
                }
            }
        }
        else if (interactionName == "RobberHouseDoorNoEntry")
        {
            EventManager.TriggerEvent(EventType.DisplayDialogue, robberHouseDoorNoEntry);
            await EventManager.WaitForEvent(EventType.EndDialogue);
        }
        else if (interactionName == "RobberTree")
        {
            if (!treeComplete)
            {
                if (haveCat)

                {
                    EventManager.TriggerEvent(EventType.DisplayDialogue, robberTreeWCat);
                    EventManager.TriggerEvent(EventType.AddHeart, 1);
                    await EventManager.WaitForEvent(EventType.EndDialogue);

                    EventManager.TriggerEvent(EventType.DisplayInfoMessage, "Received a heart.");

                    EventManager.TriggerEvent(EventType.DisplayDialogue, donkeySaysUseful);
                    treeComplete = true;

                }
                else
                    EventManager.TriggerEvent(EventType.DisplayDialogue, robberTree);

            }
            else
            {
                EventManager.TriggerEvent(EventType.DisplayDialogue, donkeyNothingToSeeHere);
            }
            await EventManager.WaitForEvent(EventType.EndDialogue);


        }
        else if (interactionName == "BossBattle")
        {

            //Display hearts
            EventManager.TriggerEvent(EventType.ShowHearts, null);

            EventManager.TriggerEvent(EventType.DisableMovement, null);

            ToggleInteriorRobbers(true);

            //Move owners to the side
            EventManager.TriggerEvent(EventType.Move, new MoveCommand("Rob", robberPositionsToMoveTo[0], MoveCommand.MoveType.Location));
            EventManager.TriggerEvent(EventType.Move, new MoveCommand("Bab", robberPositionsToMoveTo[1], MoveCommand.MoveType.Location));
            EventManager.TriggerEvent(EventType.Move, new MoveCommand("Bob", robberPositionsToMoveTo[2], MoveCommand.MoveType.Location));
            EventManager.TriggerEvent(EventType.Move, new MoveCommand("Rab", robberPositionsToMoveTo[3], MoveCommand.MoveType.Location));


            mainCamera.transform.SetParent(GameObject.Find("Bob").transform);
            mainCamera.transform.localPosition = new Vector3(0, 0, -10);

            await EventManager.WaitForEventUntil(EventType.EndMove, "Rob");
            await Wait.ForSeconds(0.5f);
            EventManager.TriggerEvent(EventType.Move, new MoveCommand("Rob", robberPositionsToMoveTo[0] + new Vector3(-1, 0, 0), MoveCommand.MoveType.Location));
            EventManager.TriggerEvent(EventType.Move, new MoveCommand("Bab", robberPositionsToMoveTo[1] + new Vector3(-1, 0, 0), MoveCommand.MoveType.Location));
            EventManager.TriggerEvent(EventType.Move, new MoveCommand("Bob", robberPositionsToMoveTo[2] + new Vector3(-1, 0, 0), MoveCommand.MoveType.Location));
            EventManager.TriggerEvent(EventType.Move, new MoveCommand("Rab", robberPositionsToMoveTo[3] + new Vector3(-1, 0, 0), MoveCommand.MoveType.Location));

            mainCamera.transform.SetParent(GameObject.Find("Ass").transform);
            mainCamera.transform.localPosition = new Vector3(0, 0, -10);
            //Display info message
            EventManager.TriggerEvent(EventType.DisplayInfoMessage, "Avoid bullets and finish the maze.");

            //Start Music
            EventManager.TriggerEvent(EventType.ChangeMusic, "fight");

            //Detach animals
            if (haveCock)
            {
                EventManager.TriggerEvent(EventType.RemoveFollower, "Rooster");
                EventManager.TriggerEvent(EventType.Move, new MoveCommand("Rooster", animalPositionstoMove[0], MoveCommand.MoveType.Location));

            }
            if (haveDog)
            {
                EventManager.TriggerEvent(EventType.RemoveFollower, "Dog");
                EventManager.TriggerEvent(EventType.Move, new MoveCommand("Dog", animalPositionstoMove[1], MoveCommand.MoveType.Location));

            }
            if (haveCat)
            {
                EventManager.TriggerEvent(EventType.RemoveFollower, "Cat");
                EventManager.TriggerEvent(EventType.Move, new MoveCommand("Cat", animalPositionstoMove[2], MoveCommand.MoveType.Location));

            }

            if (haveCat || haveDog || haveCock)
            {
                EventManager.TriggerEvent(EventType.DisplayDialogue, new Conversation(null, new Monologue[] { new Monologue("Donkey", "I have to do this alone.") }));
            }

            //Enable movement
            EventManager.TriggerEvent(EventType.EnableMovement, null);

            //Start shooting
            EventManager.TriggerEvent(EventType.StartShooting, null);
            //Move animals to side
        }
        else if (interactionName == "MurderRock")
        {
            EventManager.TriggerEvent(EventType.DisableMovement, null);
            EventManager.TriggerEvent(EventType.HideObject, "MurderRockCollide");

            mainCamera.transform.SetParent(GameObject.Find("MurderRock").transform);

            EventManager.TriggerEvent(EventType.Move, new MoveCommand("MurderRock", new Vector3(5947, 462, 0), MoveCommand.MoveType.Location));
            await EventManager.WaitForEventUntil(EventType.EndMove, "MurderRock");

            EventManager.TriggerEvent(EventType.Move, new MoveCommand("MurderRock", new Vector3(5947, 46, 0), MoveCommand.MoveType.Location));
            await EventManager.WaitForEventUntil(EventType.EndMove, "MurderRock");

            mainCamera.transform.SetParent(GameObject.Find("Ass").transform);
            mainCamera.transform.localPosition = new Vector3(0, 0, -10);


            if (haveCock || haveDog || haveCat)
            {
                EventManager.TriggerEvent(EventType.GameOver, "killRobbersTogether");
            }
            else EventManager.TriggerEvent(EventType.GameOver, "killRobbersAlone");


        }


        EventManager.TriggerEvent(EventType.EndInteraction, interactionName);
    }

    private async Task MoveMultipleSprites(Vector3 location, string[] spriteNames, int xGap)
    {
        int gap = 0;
        foreach (string spriteName in spriteNames)
        {
            EventManager.TriggerEvent(EventType.Move, new MoveCommand(spriteName, location + new Vector3(gap, 0, 0), MoveCommand.MoveType.Location));
            await EventManager.WaitForEventUntil(EventType.EndMove, spriteName);
            gap += xGap;
        }
    }
    private void MoveMultipleSpritesNoWaiting(Vector3 location, string[] spriteNames, int xGap, int yGap = 0)
    {
        int gap = 0;
        int ygap = 0;
        foreach (string spriteName in spriteNames)
        {
            EventManager.TriggerEvent(EventType.Move, new MoveCommand(spriteName, location + new Vector3(gap, ygap, 0), MoveCommand.MoveType.Location));
            gap += xGap;
            ygap += yGap;
        }
    }
    private void MoveMultipleSpritesInstantly(Vector3 location, string[] spriteNames, int xGap)
    {
        int gap = 0;
        foreach (string spriteName in spriteNames)
        {
            GameObject.Find(spriteName).transform.position = location + new Vector3(gap, 0, 0);
            gap += xGap;
        }
    }

    private async Task FadeInOutBlackNoMovement(float inOutTime, float pauseTime)
    {
        EventManager.TriggerEvent(EventType.DisableMovement, null);
        EventManager.TriggerEvent(EventType.FadeIn, new FadeCommand("Black", inOutTime));
        await EventManager.WaitForEvent(EventType.EndFadeIn);
        await Wait.ForSeconds(pauseTime);
        EventManager.TriggerEvent(EventType.FadeOut, new FadeCommand("Black", inOutTime));
        await EventManager.WaitForEvent(EventType.EndFadeOut);
        EventManager.TriggerEvent(EventType.EnableMovement, null);
    }

    private async Task WaitForCatUnlock()
    {
        EventManager.TriggerEvent(EventType.DisableMovement, null);

        EventManager.TriggerEvent(EventType.Move, new MoveCommand("Cat", new Vector3(5193, -22, 0), MoveCommand.MoveType.Location));
        await EventManager.WaitForEventUntil(EventType.EndMove, "Cat");

        EventManager.TriggerEvent(EventType.Move, new MoveCommand("Cat", new Vector3(5193, 53, 0), MoveCommand.MoveType.Location));
        await EventManager.WaitForEventUntil(EventType.EndMove, "Cat");

        UnlockRobberHouse();

        EventManager.TriggerEvent(EventType.EnableMovement, null);
    }

    private void UnlockRobberHouse()
    {
        EventManager.TriggerEvent(EventType.HideObject, "RobberHouseDoorNoEntry");
        EventManager.TriggerEvent(EventType.ShowObject, "RobberHouseDoor");

        EventManager.TriggerEvent(EventType.PlaySound, "unlock");
    }

    private async Task WaitForDialogue(Conversation conversation)
    {
        EventManager.TriggerEvent(EventType.DisplayDialogue, conversation);
        await EventManager.WaitForEvent(EventType.EndDialogue);
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
            {
                return talkToWitchConvoWithDog;
            }
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
            if (!haveDog)
                return dogHouseConvo;
            else return dogHouseConvoWithDog;
        }
        else if (triggerName == "Rooster")
        {
            if (cockSaved)
                return freedCockConvo;
            else if (haveDog)
                return cockCageWithDogConvo;
            else
                return cockCageConvo;
        }
        else if (triggerName == "DangerSign")
        {
            return dangerSignConvo;
        }
        else if (triggerName == "RobberHouseStart1")
        {
            return robberHouseStartConvo1;
        }
        else if (triggerName == "RobberHouseStart2Dog")
        {
            return robberHouseStartConvo2Dog;
        }
        else if (triggerName == "RobberHouseStart2CockOnly")
        {
            return robberHouseStartConvo2CockOnly;
        }
        else if (triggerName == "RobberHouseStart2CatOnly")
        {
            return robberHouseStartConvo2CatOnly;
        }
        else if (triggerName == "RobberHouseStart3CatNotOnly")
        {
            return robberHouseStartConvo3CatNotOnly;
        }
        else if (triggerName == "RobberTree")
        {
            return robberTree;
        }


        else
        {
            Debug.LogError("Can't find dialogue.");
            return null;
        }
    }
    private void ToggleInteriorRobbers(bool toggle)
    {
        if (!toggle)
        {
            EventManager.TriggerEvent(EventType.HideObject, "Bob");
            EventManager.TriggerEvent(EventType.HideObject, "Rob");
            EventManager.TriggerEvent(EventType.HideObject, "Bab");
            EventManager.TriggerEvent(EventType.HideObject, "Rab");
        }
        else
        {

            EventManager.TriggerEvent(EventType.ShowObject, "Bob");

            EventManager.TriggerEvent(EventType.ShowObject, "Rob");

            EventManager.TriggerEvent(EventType.ShowObject, "Rab");

            EventManager.TriggerEvent(EventType.ShowObject, "Bab");

        }
    }
    private void ToggleExteriorRobbers(bool toggle)
    {
        if (!toggle)
        {
            EventManager.TriggerEvent(EventType.HideObject, "BobOutside");
            EventManager.TriggerEvent(EventType.HideObject, "RobOutside");
            EventManager.TriggerEvent(EventType.HideObject, "BabOutside");
            EventManager.TriggerEvent(EventType.HideObject, "RabOutside");
        }
        else
        {
            EventManager.TriggerEvent(EventType.ShowObject, "BobOutside");
            EventManager.TriggerEvent(EventType.ShowObject, "RobOutside");
            EventManager.TriggerEvent(EventType.ShowObject, "RabOutside");
            EventManager.TriggerEvent(EventType.ShowObject, "BabOutside");
        }
    }
    // dog, cock, cat
    private Conversation YdogYcockYcat1 = new Conversation(new Monologue[] {
new Monologue("Donkey", "Look, it's a house. There's a light in the window."),
new Monologue("Rooster", "Looks a bit shabby."),
new Monologue("Donkey", "Hmm, dog can you try to  hear anything useful?"),
new Monologue("Dog", "I'll try."),
new Monologue("???", "That was a nice load we got from that caravan, hahaha."),
new Monologue("Dog", "I think they are robbers."),
new Monologue("Donkey", "Hmm, I have an idea, what if we take their house when they leave?"),
new Monologue("Dog", "WAIT! There's more!"),
new Monologue("Robber", "I found out where our animals are kept, we can finally take them back from those  thieves. Oh I remember my sweet donkey, I used to love him with all my heart..."),
new Monologue("Dog", "Wait What!??! The robbers have been  ROBBED. Someone stole a donkey from them!"),
new Monologue("Donkey", "Wait a second! Our owners are the thieves! We have to go in and talk to them."),
new Monologue("Dog", "The door is locked, so we can't go in and it's made of wool so knocking won't help."),
new Monologue("Donkey", "Let's wait until they come out."),
new Monologue("Cat", "Wait, I can sneak in through the window and unlock it on the inside."),
});
    // 2
    private Conversation YdogYcockYcat2 = new Conversation(new Monologue[] {
new Monologue("Rooster", "Don't shoot! We're here to talk!"),
new Monologue("Robber", "A talking chicken, I'm intrigued, continue..."),
new Monologue("Rooster", "I'm a rooster! We were rejected by our owners and now we are travelling together."),
new Monologue("Robber", "I used to have a rooster like you...  Come to think of it, Bob used to have a donkey and Rob used to have a dog and Bab used to have a cat. Wait a second... Are you...?"),
new Monologue("Rooster", "Yes!"),
new Monologue("Robber", "Oh my god! We were gonna go get you but we didn't know who took you. It's so nice to see you all."),
});

    // dog, cock,!cat
    private Conversation YdogYcockNcat1 = new Conversation(new Monologue[] {
new Monologue("Donkey", "Look, it's a house. There's a light in the window."),
new Monologue("Rooster", "Looks a bit shabby."),
new Monologue("Donkey", "Hmm, dog can you try to  hear anything useful?"),
new Monologue("Dog", "I'll try."),
new Monologue("???", "That was a nice load we got from that caravan, hahaha."),
new Monologue("Dog", "I think they are robbers."),
new Monologue("Donkey", "Hmm, I have an idea, what if we take their house when they leave?"),
new Monologue("Dog", "WAIT! There's more!"),
new Monologue("Robber", "I found out where our animals are kept, we can finally take them back from those  thieves. Oh I remember my sweet donkey, I used to love him with all my heart..."),
new Monologue("Dog", "Wait What!??! The robbers have been  ROBBED. Someone stole a donkey from them!"),
new Monologue("Donkey", "Wait a second! Our owners are the thieves! We have to go in and talk to them."),
new Monologue("Dog", "The door is locked, so we can't go in and it's made of wool so knocking won't help."),
new Monologue("Donkey", "Let's wait until they come out."),
});
    // 2
    private Conversation YdogYcockNcat2 = new Conversation(new Monologue[] {
new Monologue("Rooster", "Don't shoot! We're here to talk!"),
new Monologue("Robber", "A talking chicken, I'm intrigued, continue..."),
new Monologue("Rooster", "I'm a rooster! We were rejected by our owners and now we are travelling together."),
new Monologue("Robber", "I used to have a rooster like you...  Come to think of  , Bob used to have a donkey and Rob used to have a dog and Bab used to have a cat. Wait a second... Are you...?"),
new Monologue("Rooster", "Yes!"),
new Monologue("Robber", "Oh my god! We were gonna go get you but we didn't know who took you. It's so nice to see you all."),
});

    // dog,!cock, cat
    private Conversation YdogNcockYcat1 = new Conversation(new Monologue[] {
new Monologue("Donkey", "Look, it's a house. There's a light in the window."),
new Monologue("Donkey", "Hmm, dog can you try to  hear anything useful?"),
new Monologue("Dog", "I'll try."),
new Monologue("???", "That was a nice load we got from that caravan, hahaha."),
new Monologue("Dog", "I think they are robbers."),
new Monologue("Donkey", "Hmm, I have an idea, what if we take their house when they leave?"),
new Monologue("Dog", "WAIT! There's more!"),
new Monologue("Robber", "I found out where our animals are kept, we can finally take them back from those  thieves. Oh I remember my sweet donkey, I used to love him with all my heart..."),
new Monologue("Dog", "Wait What!??! The robbers have been  ROBBED. Someone stole a donkey from them!"),
new Monologue("Donkey", "Wait a second! Our owners are the thieves! We have to go in and talk to them."),
new Monologue("Dog", "The door is locked, so we can't go in and it's made of wool so knocking won't help."),
new Monologue("Donkey", "Let's wait until they come out."),
});
    // 2
    private Conversation YdogNcockYcat2 = new Conversation(new Monologue[] {
new Monologue("Dog", "Hey, it's us, your long lost animals!"),
new Monologue("Robber", "What the heck are those animals doing in here!?"),
new Monologue("Cat", "Don't you remember me? The person who stole me was going to kill me because I'm too old now!"),
new Monologue("Robber", "Get out! Scram!"),
new Monologue("Donkey", "They can't understand us, we'll have to fight them..."),
});

    //!dog, cock, cat
    //!dog,!cock, cat


    //!dog, cock,!cat
    private Conversation NdogYcockNcat1 = new Conversation(new Monologue[] {
new Monologue("Donkey", "Look, it's a house. There's a light in the window."),
new Monologue("Rooster", "Looks a bit shabby."),
new Monologue("Donkey", "Hmm, I have an idea, what if we take their house when they leave?"),
new Monologue("Rooster", "We'll have to wait outside until they come out. I can try and convince them to leave the house."),
});
    // 2
    private Conversation NdogYcockNcat2 = new Conversation(new Monologue[] {
new Monologue("Donkey", "Boo!"),
new Monologue("Rooster", "Boo!!!"),
new Monologue("???", "Ahh!"),
new Monologue("???", "Eek!"),
new Monologue("???", "EEEE!!"),
new Monologue("???", "AAAAHHHH!!!!!!"),
});
    // 3
    private Conversation NdogYcockNcat3 = new Conversation(new Monologue[] {
new Monologue("Donkey", "Phew, we did it!"),
new Monologue("Rooster", "Time for a nap"),
});
    // 4
    private Conversation NdogYcockNcat4 = new Conversation(new Monologue[] {
new Monologue("???", "!!!!!!"),
new Monologue("???", "!!!!!!!!!!!!!!!"),
new Monologue("Donkey", "I think they realised we aren't a real monster..."),
new Monologue("Rooster", "Bring it on!"),
});

    // dog,!cock,!cat
    private Conversation YdogNcockNcat1 = new Conversation(new Monologue[] {
new Monologue("Donkey", "Look, it's a house. There's a light in the window."),
new Monologue("Donkey", "Hmm, dog can you try to  hear anything useful?"),
new Monologue("Dog", "I'll try."),
new Monologue("???", "That was a nice load we got from that caravan, hahaha."),
new Monologue("Dog", "I think they are robbers."),
new Monologue("Donkey", "Hmm, I have an idea, what if we take their house when they leave?"),
new Monologue("Dog", "WAIT! There's more!"),
new Monologue("Robber", "I found out where our animals are kept, we can finally take them back from those  thieves. Oh I remember my sweet donkey, I used to love him with all my heart..."),
new Monologue("Dog", "Wait What!??! The robbers have been  ROBBED. Someone stole a donkey from them!"),
new Monologue("Donkey", "Wait a second! Our owners are the thieves! We have to go in and talk to them."),
new Monologue("Dog", "The door is locked, so we can't go in and it's made of wool so knocking won't help."),
new Monologue("Donkey", "Let's wait until they come out."),
});
    // 2
    private Conversation YdogNcockNcat2 = new Conversation(new Monologue[] {
new Monologue("Donkey", "Please, don't hurt us, we're your friends. You once looked after us!"),
new Monologue("Robber", "What are these animals doing here?"),
new Monologue("Robber", "Shoo! go away!"),
});
    // 3
    private Conversation YdogNcockNcat3 = new Conversation(new Monologue[] {
new Monologue("Robber", "Why are you coming in here? Scram!"),
new Monologue("Donkey", "They can't understand us, we'll have to fight them..."),
});

    //!dog,!cock,!cat
    private Conversation NdogNcockNcat1 = new Conversation(new Monologue[] {
new Monologue("Donkey", "It's a house. There's a light in the window."),
new Monologue("Donkey", "I'll have to wait outside until they come out. Maybe they can give me somewhere to stay."),
});
    // 2
    private Conversation NdogNcockNcat2 = new Conversation(new Monologue[] {
new Monologue("Donkey", "Hello, I was wondering if you had any place that I could stay, my owner kicked me out."),
new Monologue("???", "????????"),
new Monologue("???", "????????????"),
});
    // 3
    private Conversation NdogNcockNcat3 = new Conversation(new Monologue[] {
new Monologue("???", "????!!!!?!"),
new Monologue("Donkey", "I don't think they understand me, I'll have to fight them..."),
});


}

public class Conversation
{
    public String conversationTrigger;
    public Monologue[] dialogue;
    public Conversation(String conversationTrigger, Monologue[] dialogue)
    {
        this.conversationTrigger = conversationTrigger;
        this.dialogue = dialogue;
    }
    public Conversation(Monologue[] dialogue)
    {
        this.conversationTrigger = "";
        this.dialogue = dialogue;
    }

    public Conversation(string conversationTrigger, string[] dialogueString)
    {
        this.conversationTrigger = conversationTrigger;
        this.dialogue = new Monologue[dialogueString.Length / 2];
        for (int i = 0; i < dialogue.Length; i++)
        {
            this.dialogue[i] = new Monologue(dialogueString[i * 2], dialogueString[i * 2 + 1]);
        }
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

