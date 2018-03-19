using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonCinematic : MonoBehaviour {

    public Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            anim.enabled = true;
            StartCoroutine(Cinematic());
        }
    }

    IEnumerator Cinematic()
    {
        yield return new WaitForSeconds(1);
        CameraFX.Screenshake(1, 0.2f, 0.2f);
        yield return new WaitForSeconds(2);
        CameraFX.ZoomIn(1);
    }
}
