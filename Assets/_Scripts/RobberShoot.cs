using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobberShoot : MonoBehaviour
{
    private bool shot = false;

    public GameObject projectile;

    // Use this for initialization
    void Start()
    {

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
        Instantiate(projectile, gameObject.transform.position, gameObject.transform.rotation);
        yield return new WaitForSeconds(2f);
        shot = true;
    }
    public void StartShooting()
    {
        shot = true;
    }
}
