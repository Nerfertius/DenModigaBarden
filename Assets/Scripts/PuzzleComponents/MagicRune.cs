using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicRune : MonoBehaviour {

    private Collider2D collider;
    private SpriteRenderer renderer;

    public float cooldown;
    private Timer cooldownTimer;

    public Color inactiveColor;
    private Color defaultColor;

    public ParticleSystem onTriggerEffect;

    private PlayerDamageData playerDamageData;


    void Start() {
        cooldownTimer = new Timer(cooldown);
        cooldownTimer.Start();
        cooldownTimer.InstantFinish();
        collider = GetComponent<CircleCollider2D>();
        renderer = GetComponent<SpriteRenderer>();
        defaultColor = renderer.color;
        playerDamageData = GetComponent<PlayerDamageData>();
    }

    void Update() {
        if (cooldownTimer.IsDone()) {
            renderer.color = defaultColor;
            playerDamageData.harmful = true;
        }
    }

	public void OnTriggerStay2D(Collider2D coll) {
        if(coll.tag == "Player" && cooldownTimer.IsDone() && playerDamageData.harmful) {
            //coll.GetComponent<StateController>().OnTriggerStay2D(this.collider);
            cooldownTimer.Start();
            renderer.color = inactiveColor;
            StartCoroutine(setAsNotHarmful());
            Instantiate(onTriggerEffect, transform.position, Quaternion.identity);
        }
    }

    private IEnumerator setAsNotHarmful() {
        yield return new WaitForEndOfFrame();
        playerDamageData.harmful = false;
    }
}
