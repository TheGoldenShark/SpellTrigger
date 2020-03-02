using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnnouncementText : MonoBehaviour
{
    public float duration = 1;
    private float transparencyIncrement;
    private Text text;
    void Start()
    {
        // Calculates the amount that transparency should be increased by per second
        transparencyIncrement = (1 / duration);
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        // Decrements the alpha component of text's color, by copying it to tempcolor, editing the alpha component based on how
        // much time passed since the previous frame, then copying it back
        Color tempColor = text.color;
        tempColor.a -= transparencyIncrement * Time.deltaTime;
        text.color = tempColor;
        // If the object is completely transparent, destroy it
        if (tempColor.a < 0)
        {
            Destroy(transform.parent.gameObject);
        }
    }
}
