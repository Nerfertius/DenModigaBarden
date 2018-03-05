using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraFX : MonoBehaviour {

    private static CameraFX instance;
    public Image screenFade;
    public Transform renderScreen;
    private Vector3 rsStartScale;
    private Quaternion rsStartRot;

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
		rsStartScale = renderScreen.localScale;
		rsStartRot = renderScreen.rotation;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)){
            //FadeIn();
        } else if (Input.GetKeyDown(KeyCode.R)){
            //FadeOut();
        } else if (Input.GetKeyDown(KeyCode.N))
        {
            //Screenshake(0.10f, 0.025f, 0.025f);
            ZoomIn(1f);
        }
    }

    public static void FadeIn()
    {
        instance.StartCoroutine("FadeInFX");
    }

    public static void FadeOut()
    {
        instance.StartCoroutine("FadeOutFX");
    }

    public static void Screenshake(float duration, float xIntensity, float yIntensity)
    {
        instance.StartCoroutine(instance.ScreenshakeFX(duration, xIntensity, yIntensity));
    }

    public static void ZoomIn(float duration){
    	instance.StartCoroutine(instance.ZoomInFX(duration));
    }

    public static void ResetRenderScreen ()
	{
		instance.renderScreen.localScale = instance.rsStartScale;
		instance.renderScreen.rotation = instance.rsStartRot;
	}

    IEnumerator ZoomInFX (float duration)
	{
		while (timer < duration) {
			timer += 0.05f;

			renderScreen.localScale += new Vector3(500 * Time.unscaledDeltaTime, 500 * Time.unscaledDeltaTime, 0);
			renderScreen.Rotate(0,0,1000*Time.unscaledDeltaTime);
			yield return new WaitForSecondsRealtime(0.05f);
		}

		timer = 0;
		renderScreen.localScale = rsStartScale;
		renderScreen.rotation = rsStartRot;
	}

    IEnumerator ScreenshakeFX(float duration, float xIntensity, float yIntensity)
    {
        bool camScriptStatus = camScript.isActiveAndEnabled;
        camScript.enabled = false;
        
        Vector3 camPosition = transform.position;
        float posX = transform.position.x;
        float posY = transform.position.y;
        
        while(timer < duration)
        { 
            timer += 0.05f;

            transform.position = new Vector3(Random.Range(posX - xIntensity, posX + xIntensity), Random.Range(posY - yIntensity, posY + yIntensity), camPosition.z);

            yield return new WaitForSecondsRealtime(0.05f);
        }

        transform.position = camPosition;
        timer = 0;
        camScript.enabled = camScriptStatus;
    }

    IEnumerator FadeInFX()
    {
        Color c = screenFade.color;

        for (float value = screenFade.color.a; value <= 1; value += 0.01f * fadeSpeed)
        {
            c.a = Mathf.Clamp01(value);
            screenFade.color = c;

            yield return new WaitForSecondsRealtime(0.01f);
        }

        c.a = 1;
        screenFade.color = c;
    }

    IEnumerator FadeOutFX()
    {
        Color c = screenFade.color;

        for (float value = screenFade.color.a; value >= 0; value -= 0.01f * fadeSpeed)
        {
            c.a = Mathf.Clamp01(value);
            screenFade.color = c;

            yield return new WaitForSecondsRealtime(0.01f);
        }

        c.a = 0;
        screenFade.color = c;
    }
}
