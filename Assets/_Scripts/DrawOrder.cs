using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawOrder : MonoBehaviour {

    [SerializeField]
    private int offset;
    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update () {
        sr.sortingOrder = Mathf.RoundToInt(transform.position.y / 3f) * -1 + offset;
    }
}
