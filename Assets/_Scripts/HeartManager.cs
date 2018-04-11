using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HeartManager : MonoBehaviour
{
    private int noOfHearts = 1;
    public int numberOfShownHearts;

    public GameObject[] hearts; //Heart 0 is the first heart from the right
    private GameObject[] shownHearts;
    private void Awake()
    {
        EventManager.AddListener(EventType.ShowHearts, OnShowHearts);
        EventManager.AddListener(EventType.RemoveHeartFromDisplay, OnRemoveHeartFromDisplay);
        EventManager.AddListener(EventType.AddHeart, OnAddHeart);

    }

    private void OnAddHeart(object number)
    {
        noOfHearts += (int)number;
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

            if (shownHearts.Length == 0) OutOfHearts();
        }
    }
    private void OutOfHearts()
    {
        Debug.Log("Out of hearts");
        EventManager.TriggerEvent(EventType.GameOver, "outOfHearts");
    }

    private void OnShowHearts(object _)
    {
        numberOfShownHearts = noOfHearts;

        shownHearts = new GameObject[numberOfShownHearts];
        Array.Copy(hearts, 0, shownHearts, 0, numberOfShownHearts);

        for (int i = 0; i < shownHearts.Length; i++)
        {
            shownHearts[i].SetActive(true);
        }
    }








}
