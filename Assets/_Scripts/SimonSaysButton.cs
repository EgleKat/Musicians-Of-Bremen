using System;
using System.Threading.Tasks;
using UnityEngine;

public class SimonSaysButton : MonoBehaviour
{
    private bool triggerable = false;
    private SpriteRenderer spriteRenderer;
    Color originalColor;
    Color alertColor;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        alertColor = originalColor + new Color(0.2f, 0.2f, 0.2f, 0);

        EventManager.AddListener(EventType.StartSimonSaysRound, OnStartSimonSaysRound);
        EventManager.AddListener(EventType.EndSimonSays, OnEndSimonSays);
        EventManager.AddListener(EventType.StartSimonSaysRecall, OnStartSimonSaysRecall);
        EventManager.AddListener(EventType.StartAlertSimonSays, OnStartAlertSimonSays);
    }

    private async void OnStartAlertSimonSays(object buttonName)
    {
        if ((string)buttonName == gameObject.name)
        {
            spriteRenderer.color = alertColor;
            //play a sound

            await Task.Delay(TimeSpan.FromSeconds(0.2));
            spriteRenderer.color = originalColor;
            EventManager.TriggerEvent(EventType.EndAlertSimonSays, buttonName);
        }
    }

    private void OnStartSimonSaysRecall(object _)
    {
        triggerable = true;
    }

    private void OnEndSimonSays(object _)
    {
        triggerable = false;
    }

    private void OnStartSimonSaysRound(object _)
    {
        triggerable = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (triggerable)
        {
            EventManager.TriggerEvent(EventType.PlaySound, gameObject.name);
            EventManager.TriggerEvent(EventType.StartAlertSimonSays, gameObject.name);
        }
    }

}
