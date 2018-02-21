using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraFX : MonoBehaviour {

    private static CameraFX instance;
    public Image screenFade;

    private CameraFollow2D camScript;
    private float timer = 0;

    public float fadeSpeed;
    
    private void Awake()
    {
        instance = this;
    }

	private void Start ()
    {
        camScript = GetComponent<CameraFollow2D>();
    }

    // For Debug
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)){
            FadeIn();
        } else if (Input.GetKeyDown(KeyCode.R)){
            FadeOut();
        } else if (Input.GetKeyDown(KeyCode.N))
        {
            Screenshake(0.10f, 0.025f, 0.025f);
        }
    }
    

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

    public static void Screenshake(float duration, float xIntensity, float yIntensity)
    {
        instance.StopAllCoroutines();
        instance.StartCoroutine(instance.ScreenshakeFX(duration, xIntensity, yIntensity));
    }

    IEnumerator ScreenshakeFX(float duration, float xIntensity, float yIntensity)
    {
        camScript.enabled = false;
        
        Vector3 camPosition = transform.position;
        float posX = transform.position.x;
        float posY = transform.position.y;
        
        while(timer < duration)
        { 
            timer += 0.05f;

            transform.position = new Vector3(Random.Range(posX - xIntensity, posX + xIntensity), Random.Range(posY - yIntensity, posY + yIntensity), camPosition.z);

            yield return new WaitForSeconds(0.05f);
        }

        transform.position = camPosition;
        timer = 0;
        camScript.enabled = true;
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
