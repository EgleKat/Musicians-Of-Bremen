using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobberManager : MonoBehaviour
{
    private bool shooting = false;
    private bool shot = true;
    public float shotDelay;
    public GameObject projectile;
    public Sprite deathSprite;

    private void Awake()
    {
        EventManager.AddListener(EventType.StartShooting, StartShooting);
        EventManager.AddListener(EventType.StopShooting, StopShooting);
    }

    private void StartShooting(object _)
    {
        shooting = true;
    }
    private void StopShooting(object _)
    {
        shooting = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (shooting)
        {
            if (shot)
            {
                shot = false;
                StartCoroutine(Shoot());
            }
        }
    }

    IEnumerator Shoot()
    {
        Instantiate(projectile, gameObject.transform.position + new Vector3(0, 16, 0), gameObject.transform.rotation);
        yield return new WaitForSeconds(shotDelay);
        shot = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name != "MurderRock")
        {
            return;
        }

        EventManager.TriggerEvent(EventType.PlaySound, "hit");
        Die();

    }

    private void Die()
    {

        gameObject.GetComponent<SpriteRenderer>().sprite = deathSprite;
        StopShooting(null);
    }
}
