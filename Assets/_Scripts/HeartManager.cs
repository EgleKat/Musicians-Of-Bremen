using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HeartManager : MonoBehaviour
{
    private int noOfHearts = 0;
    public GameObject[] hearts; //Heart 0 is the first heart from the right
    private GameObject[] shownHearts;
    private int numberOfShownHearts;
    private void Awake()
    {
        EventManager.AddListener(EventType.ShowHearts, OnShowHearts);
        EventManager.AddListener(EventType.StartCountingHearts, StartCounting);
        EventManager.AddListener(EventType.RemoveHeartFromDisplay, OnRemoveHeartFromDisplay);
        EventManager.AddListener(EventType.AddHeart, OnAddHeart);

    }

    private void OnAddHeart(object number)
    {
        noOfHearts += (int)number;
    }

    private void Start()
    {
        OnShowHearts(4);
    }
    private void OnRemoveHeartFromDisplay(object _)
    {
        if (shownHearts.Length > 0)
        {
            shownHearts[numberOfShownHearts - 1].SetActive(false);
            numberOfShownHearts--;
            GameObject[] newShownHearts = new GameObject[numberOfShownHearts];
            Array.Copy(shownHearts, 0, newShownHearts, 0, numberOfShownHearts);
            shownHearts = newShownHearts;
        }
        else
        {
            EventManager.TriggerEvent(EventType.StartInteraction, "GameOver");
        }
    }

    private void StartCounting(object a)
    {
    }

    private void OnShowHearts(object number)
    {
        noOfHearts = (int)number;
        numberOfShownHearts = noOfHearts;

        shownHearts = new GameObject[noOfHearts];
        Array.Copy(hearts, 0, shownHearts, 0, noOfHearts);

        for (int i = 0; i < shownHearts.Length; i++)
        {
            shownHearts[i].SetActive(true);
        }
    }








}
