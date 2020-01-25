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
        transparencyIncrement = (1 / duration);
        text = this.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        Color tempColor = text.color;
        tempColor.a -= transparencyIncrement * Time.deltaTime;
        text.color = tempColor;
        if (tempColor.a < 0)
        {
            Destroy(transform.parent.gameObject);
        }
    }
}
