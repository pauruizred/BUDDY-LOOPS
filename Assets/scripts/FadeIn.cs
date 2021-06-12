using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIn : MonoBehaviour
{
    public float timeToReachTarget;
    private float startOpacity;
    private float targetOpacity;
    private float percentFaded;
    private Color stickerColor;

    // Start is called before the first frame update
    void Start()
    {
        startOpacity = 0;
        targetOpacity = 1;
        stickerColor = this.GetComponentInChildren<SpriteRenderer>().material.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (percentFaded < 1f)
        {
            percentFaded += Time.deltaTime / timeToReachTarget;
            float opacity = Mathf.Lerp(startOpacity, targetOpacity, percentFaded);
            Color newColor = new Color(stickerColor[0], stickerColor[1], stickerColor[2], opacity);
            this.GetComponentInChildren<SpriteRenderer>().material.color = newColor;
        }
    }
}
