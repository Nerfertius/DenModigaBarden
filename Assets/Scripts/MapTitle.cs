using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapTitle : MonoBehaviour {
    private Color startColor;

    public Sprite titleSprite;
    public Image titleObject;
    public float fadeSpeed;
    public float displayLength;
    
    void Start () {
        titleObject.sprite = titleSprite;
        titleObject.color = new Color(titleObject.color.r, titleObject.color.g, titleObject.color.b, 0);
        startColor = titleObject.color;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            StopAllCoroutines();
            StartCoroutine(FadeIn());
            StartCoroutine(DelayedFadeOut());
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            titleObject.color = startColor;
        }
    }

    IEnumerator FadeIn()
    {
        yield return new WaitForSeconds(0.25f);

        Color c = titleObject.color;

        for (float value = titleObject.color.a; value <= 1; value += 0.01f * fadeSpeed)
        {
            c.a = Mathf.Clamp01(value);
            titleObject.color = c;

            yield return new WaitForSeconds(0.01f);
        }
    }
    IEnumerator DelayedFadeOut()
    {
        yield return new WaitForSeconds(displayLength);

        Color c = titleObject.color;

        for (float value = titleObject.color.a; value >= 0; value -= 0.01f * fadeSpeed)
        {
            c.a = Mathf.Clamp01(value);
            titleObject.color = c;

            yield return new WaitForSeconds(0.01f);
        }
    }
}
