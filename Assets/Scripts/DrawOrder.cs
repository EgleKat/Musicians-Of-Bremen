using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawOrder : MonoBehaviour {

    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update () {
        sr.sortingOrder = Mathf.RoundToInt(transform.position.y / 3f) * -1;
    }
}
