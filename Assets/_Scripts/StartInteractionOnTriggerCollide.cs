using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartInteractionOnTriggerCollide : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        EventManager.TriggerEvent(EventType.StartInteraction, gameObject.name);
    }
}
