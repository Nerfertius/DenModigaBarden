using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFX : MonoBehaviour {

    private static CameraFX instance;
    private static SpriteRenderer screenFade;

    public float fadeSpeed;

    private void Awake()
    {
        instance = this;
    }

	private void Start ()
    {
        screenFade = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    // For Debug
    /*
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)){
            FadeIn();
        } else if (Input.GetKeyDown(KeyCode.R)){
            FadeOut();
        }
    }
    */

    public static void FadeIn()
    {
        instance.StopAllCoroutines();
        instance.StartCoroutine("FadeInFX");
    }

    public static void FadeOut()
    {
        instance.StopAllCoroutines();
        instance.StartCoroutine("FadeOutFX");
    }

    IEnumerator FadeInFX()
    {
        Color c = screenFade.color;

        for (float value = screenFade.color.a; value <= 1; value += 0.01f * fadeSpeed)
        {
            c.a = Mathf.Clamp01(value);
            screenFade.color = c;

            yield return new WaitForSeconds(0.01f);
        }
    }
    IEnumerator FadeOutFX()
    {
        Color c = screenFade.color;

        for (float value = screenFade.color.a; value >= 0; value -= 0.01f * fadeSpeed)
        {
            c.a = Mathf.Clamp01(value);
            screenFade.color = c;

            yield return new WaitForSeconds(0.01f);
        }
    }
}
