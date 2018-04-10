using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlphaOnStart : MonoBehaviour
{
    void Awake()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Image image = GetComponent<Image>();

        if (sr)
        {
            Color c = sr.color;
            c.a = 1;
            sr.color = c;
        }
        if (image)
        {
            Color c = image.color;
            c.a = 1;
            image.color = c;
        }
    }
}
