using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{

    // Use this for initialization
    async void Start()
    {
        //Move to chicken
        EventManager.TriggerEvent(EventType.Move, new MoveCommand("Ass", new Vector3(303, -254, 0), MoveCommand.MoveType.Location));


        //Move Dog To Cat
        EventManager.TriggerEvent(EventType.Move, new MoveCommand("Dog", new Vector3(-225 + 32, -331, 0), MoveCommand.MoveType.Location));
        await Wait.ForSeconds(3f);
        MoveRandomly();



        //Fade in start button
        EventManager.TriggerEvent(EventType.FadeIn, new FadeCommand("StartButton", 10f));

        //Fade in Text
        EventManager.TriggerEvent(EventType.FadeIn, new FadeCommand("Name", 10f));
        EventManager.TriggerEvent(EventType.FadeIn, new FadeCommand("Text", 10f));

    }

    private async void MoveRandomly()
    {

        System.Random random = new System.Random();
        while (true)
        {
            float randomx1 = random.Next(-250, 350);
            float randomy1 = random.Next(-450, -150);

            float randomx2 = random.Next(-250, 350);
            float randomy2 = random.Next(-450, -150);

            EventManager.TriggerEvent(EventType.Move, new MoveCommand("Ass", new Vector3(randomx1, randomy1, 0), MoveCommand.MoveType.Location));
            EventManager.TriggerEvent(EventType.Move, new MoveCommand("Rooster", new Vector3(randomx1 + 32, randomy1, 0), MoveCommand.MoveType.Location));


            EventManager.TriggerEvent(EventType.Move, new MoveCommand("Dog", new Vector3(randomx2, randomy2, 0), MoveCommand.MoveType.Location));
            EventManager.TriggerEvent(EventType.Move, new MoveCommand("Cat", new Vector3(randomx2 + 32, randomy2, 0), MoveCommand.MoveType.Location));



            await Wait.ForSeconds(2f);

        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
