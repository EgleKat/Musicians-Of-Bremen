using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobberShoot : MonoBehaviour
{
    private bool shot = false;
    public float shotDelay;
    public GameObject projectile;



    private void Awake()
    {
        EventManager.AddListener(EventType.StartShooting, StartShooting);
    }

    private void StartShooting(object _)
    {
        shot = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (shot)
        {
            shot = false;
            StartCoroutine(Shoot());
        }
    }

    IEnumerator Shoot()
    {
        Instantiate(projectile, gameObject.transform.position + new Vector3(0,16,0), gameObject.transform.rotation);
        yield return new WaitForSeconds(shotDelay);
        shot = true;
    }
}
