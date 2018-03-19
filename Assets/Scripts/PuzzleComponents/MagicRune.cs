using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicRune : MonoBehaviour
{
    private SpriteRenderer rend;

    public float cooldown;

    public ParticleSystem onTriggerEffect;

    private PlayerDamageData playerDamageData;

    private bool doOnce;

    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        playerDamageData = GetComponent<PlayerDamageData>();
        
        PlayerPlayMelody.PlayedMagicResist += SetToHarmless;
        PlayerPlayMelody.StoppedPlaying += SetToHarmful;
    }

    private void OnDestroy()
    {
        PlayerPlayMelody.PlayedMagicResist -= SetToHarmless;
        PlayerPlayMelody.StoppedPlaying -= SetToHarmful;
    }

    public void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.tag == "Player" && playerDamageData.harmful && !doOnce)
        {
            doOnce = true;
            StartCoroutine(SetAsNotHarmful());
            Instantiate(onTriggerEffect, transform.position, Quaternion.identity);
        }
    }

    private void SetToHarmless()
    {
        StopAllCoroutines();
        StartCoroutine(SetAsNotHarmful());
    }

    private void SetToHarmful()
    {
        StopAllCoroutines();
        StartCoroutine(FadeIn());
    }

    private IEnumerator SetAsNotHarmful()
    {
        yield return new WaitForSeconds(0.1f);
        playerDamageData.harmful = false;
        doOnce = false;
        Color newColor = rend.color;
        while (rend.color.r > 0)
        {
            newColor.r -= Time.deltaTime;
            newColor.g -= Time.deltaTime;
            newColor.b -= Time.deltaTime;
            rend.color = newColor;
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(cooldown);
        if (PlayerData.player.melodyData.currentMelody != Melody.MelodyID.MagicResistMelody)
        {
            StartCoroutine(FadeIn());
        }
    }

    IEnumerator FadeIn()
    {
        Color newColor = rend.color;
        while (rend.color.r < 1)
        {
            newColor.r += Time.deltaTime;
            newColor.g += Time.deltaTime;
            newColor.b += Time.deltaTime;
            rend.color = newColor;
            yield return new WaitForEndOfFrame();
        }
        playerDamageData.harmful = true;
    }
}
