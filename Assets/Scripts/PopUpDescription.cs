using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpDescription : MonoBehaviour
{
    private bool hasShowed;
    private SpriteRenderer rend;

    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
    }

    public void Description()
    {
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        Color newColor = rend.color;
        while (rend.color.a < 1)
        {
            newColor.a += Time.deltaTime;
            rend.color = newColor;
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(2f);
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        Color newColor = rend.color;
        while (rend.color.a > 0)
        {
            transform.localPosition += Vector3.up * Time.deltaTime;
            newColor.a -= Time.deltaTime;
            rend.color = newColor;
            yield return new WaitForEndOfFrame();
        }
    }
}
