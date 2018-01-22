using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFX : MonoBehaviour {

    private static SpriteRenderer screenFade;
    private static float alpha;

	private void Start ()
    {
        screenFade = transform.GetChild(0).GetComponent<SpriteRenderer>();
	}

    private void Update()
    {
        alpha = Mathf.PingPong(Time.time, 1);
        StartFadeProcess(1);

        Debug.Log(alpha);
    }

    public static void StartFadeProcess(float duration)
    {
        screenFade.color = new Color(screenFade.color.r, screenFade.color.g, screenFade.color.b, alpha);
    }
}
