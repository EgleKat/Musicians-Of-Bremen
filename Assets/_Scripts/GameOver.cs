using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public Text variableText;
    public Text titleText;

    String gameOver = "Game Over";
    String fin = "Fin";

    String outOfHeartsText = "You died. The robbers had a nice feast.";
    String happyEndText = "They lived happily ever after. What happened to the actual thieves? That's a tale for another day...";
    string killRobbersAloneText = "The robbers were no more... The donkey stayed in the house and made it his home. He heard tales of other animals disappearing in the village, but he was safe here - all alone. ";
    string killRobbersTogetherText = "The robbers were no more... the friends stayed in the house and made it their home. They even started talking about making a band. But that's a tale for another day... ";


    private void Awake()
    {
        EventManager.AddListener(EventType.GameOver, OnGameOver);

    }

    private async void OnGameOver(object text)
    {
        await Wait.ForSeconds(0.5f);
        EventManager.TriggerEvent(EventType.FadeIn, new FadeCommand("GameOver", 2f));
        String type = (String)text;
        GameOverScreen(type);
        await EventManager.WaitForEvent(EventType.EndFadeIn);
        EventManager.TriggerEvent(EventType.ShowObject, "RestartButton");
    }

    private void GameOverScreen(String type)
    {


        gameObject.SetActive(true);
        EventManager.TriggerEvent(EventType.StopShooting, null);
        EventManager.TriggerEvent(EventType.HideObject, "Shot(Clone)");
        EventManager.TriggerEvent(EventType.HideObject, "Hearts");

        if (type == "outOfHearts")
        {
            titleText.text = gameOver;
            EventManager.TriggerEvent(EventType.ChangeMusic, "stop");
            variableText.text = outOfHeartsText;
        }
        else if (type == "happyEnding")
        {
            titleText.text = fin;
            EventManager.TriggerEvent(EventType.ChangeMusic, "happyEnd");
            variableText.text = happyEndText;
        }
        else if (type == "killRobbersAlone")
        {
            titleText.text = fin;
            EventManager.TriggerEvent(EventType.ChangeMusic, "happyEnd");
            variableText.text = killRobbersAloneText;
        }
        else if (type == "killRobbersTogether")
        {
            titleText.text = fin;
            EventManager.TriggerEvent(EventType.ChangeMusic, "happyEnd");
            variableText.text = killRobbersTogetherText;
        }
    }
}
