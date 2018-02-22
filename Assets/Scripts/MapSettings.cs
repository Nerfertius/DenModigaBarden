using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapSettings : MonoBehaviour
{
    private Color startColor;
    private MapBoundary mb;

    public Sprite titleSprite;
    public Image titleObject;
    public float fadeSpeed;
    public float displayLength;

    public AudioClip backgroundMusic;

    void Start()
    {
        mb = GetComponent<MapBoundary>();

        if (titleObject != null)
        {
            titleObject.color = new Color(titleObject.color.r, titleObject.color.g, titleObject.color.b, 0);
            startColor = titleObject.color;
        }

        TransitionState.TransitionExited += ShowMapTitle;
    }

    private void OnDestroy()
    {
        TransitionState.TransitionExited -= ShowMapTitle;
    }

    private void ShowMapTitle()
    {
        if(MapBoundary.currentMapBoundary == mb)
        {
            if (titleObject != null)
            {
                StopAllCoroutines();
                titleObject.color = startColor;
                titleObject.sprite = titleSprite;
                titleObject.SetNativeSize();
                StartCoroutine(FadeIn());
                StartCoroutine(DelayedFadeOut());
            }

            if (backgroundMusic != null)
            {
                AudioManager.SetDefaultBGM(backgroundMusic);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (titleObject != null)
            {
                StopAllCoroutines();
                titleObject.color = startColor;
            }
        }
    }

    IEnumerator FadeIn()
    {
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
