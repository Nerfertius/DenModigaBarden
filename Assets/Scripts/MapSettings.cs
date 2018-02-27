using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapSettings : MonoBehaviour
{
    private Color startColor;
    private MapBoundary mb;
    private Vector2 startPos;
    public bool beginningPlayed;

    public Sprite titleSprite;
    public Image titleObject;
    public Vector2 offset;
    public float fadeSpeed;
    public float displayLength;
    public bool beginningArea;

    public AudioClip backgroundMusic;

    void Start()
    {
        mb = GetComponent<MapBoundary>();
        startPos = titleObject.rectTransform.position;

        if (titleObject != null)
        {
            titleObject.color = new Color(titleObject.color.r, titleObject.color.g, titleObject.color.b, 0);
            startColor = titleObject.color;
        }

        StartMapFeatures();

        TransitionState.TransitionExited += StartMapFeatures;
    }

    private void OnDestroy()
    {
        TransitionState.TransitionExited -= StartMapFeatures;
    }

    private void StartMapFeatures()
    {
        if(MapBoundary.currentMapBoundary == mb || (beginningArea && !beginningPlayed))
        {
            if (!beginningArea || (beginningArea && !beginningPlayed))
            {
                if (titleObject != null)
                {
                    StopAllCoroutines();
                    titleObject.rectTransform.position = startPos + offset;
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

                if (!beginningPlayed)
                {
                    beginningPlayed = true;
                }
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

        c.a = 1;
        titleObject.color = c;
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

        c.a = 0;
        titleObject.color = c;
    }
}
