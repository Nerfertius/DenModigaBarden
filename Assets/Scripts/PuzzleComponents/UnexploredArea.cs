using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnexploredArea : MonoBehaviour {

    public float fadeDuration;
    private Timer timer;
    private Color startColor;

    private bool isFading = false;
    private SpriteRenderer spriteRenderer;


    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        timer = new Timer(fadeDuration);
        startColor = spriteRenderer.color;
    }
	
	// Update is called once per frame
	void Update () {
        if (isFading) {
            spriteRenderer.color = new Color(startColor.r, startColor.g, startColor.b, startColor.a * (1 - timer.TimePercentagePassed()));
        }
        if (timer.IsDone()) {
            Destroy(this.gameObject);
        }
	}


    public void Reveal() {
        if (!isFading) {
            isFading = true;
            timer.Start();
        }
    }
}
