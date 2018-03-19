using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class DragonCinematic : MonoBehaviour {

    public GameObject videoManager; //Can be discarded in final build
    private Animator anim;
    public VideoClip clip;
    public AudioClip dragonRoarSound;

    private void Start()
    {
        anim = GetComponent<Animator>();

        if (VideoManager.instance == null)
        {
            GameObject.Instantiate(videoManager);
        }
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
        AudioManager.PlayOneShot(dragonRoarSound);
        yield return new WaitForSeconds(1.5f);
        CameraFX.FadeSpeed = 2f;
        CameraFX.FadeIn();
        yield return new WaitForSeconds(1f);
        VideoManager.instance.SetVideoClip(clip);
        VideoManager.instance.Play();
        PlayerData.player.controller.enabled = false;

        while (!VideoManager.instance.IsPlaying())
        {
            yield return new WaitForEndOfFrame();
        }

        GameManager.PlayCanvas.enabled = false;
        CameraFX.FadeSpeed = 4f;
        CameraFX.FadeOut();

        while (VideoManager.instance.IsPlaying())
        {
            yield return new WaitForSeconds(1f);
        }
        for (int i = 0; i < GameManager.PlayCanvas.transform.childCount; i++)
        {
            if(GameManager.PlayCanvas.transform.GetChild(i).gameObject.name != "ScreenFade")
            GameManager.PlayCanvas.transform.GetChild(i).gameObject.SetActive(false);
        }
        GameManager.PlayCanvas.enabled = true;
        CameraFX.FadeIn();
        yield return new WaitForSeconds(1f);
        VideoManager.instance.Stop();
        AsyncOperation levelLoad = GameManager.instance.loadScene(0);
        levelLoad.allowSceneActivation = true;

    }
}
