using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBullet : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x - 1, gameObject.transform.position.y, gameObject.transform.position.z);


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        EventManager.TriggerEvent(EventType.RemoveHeart, null);
        if (gameObject != null)
            Destroy(gameObject);
    }
}
