using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEnterExit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        EventManager.TriggerEvent(EventType.TriggerCollide, gameObject.name);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        EventManager.TriggerEvent(EventType.EndTriggerCollide, gameObject.name);
    }
}
