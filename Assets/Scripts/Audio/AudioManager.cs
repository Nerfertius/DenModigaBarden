using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public GameObject audioSourceObject;
    public int numberOfpooledObjects;

    private static AudioManager instance;

    private ComponentPool<AudioSource> audioSourcePool;
    private LinkedList<AudioSource> activeAudioSources;

    private AudioSource BGMSource1;
    private AudioSource BGMSource2;
    private bool currentBGMSource1;

    private AudioSource[] noteSources;
    private int activeNote;

    
    private void Start()
    {
        if(instance != null) {
            instance = this;
        }
        else {
            Debug.LogError("There should only be one audioManager " + this.gameObject);
        }

        audioSourcePool = new ComponentPool<AudioSource>(audioSourceObject, numberOfpooledObjects);
        activeAudioSources = new LinkedList<AudioSource>();
        BGMSource1 = new AudioSource();
        BGMSource2 = new AudioSource();
    }

    private void Update() {
        foreach(AudioSource s in activeAudioSources) {
            if(s.time >= s.clip.length) {
                audioSourcePool.Free(s);
                activeAudioSources.Remove(s);
            }
        }
    }

    public void PlayOneShot(AudioClip ac, float volume) {
        AudioSource source = audioSourcePool.Get();
        resetAudioSource(source);
        source.clip = ac;
        source.Play();
        activeAudioSources.AddLast(source);
    }

    public void PlayOneShot(AudioClip ac) {
        PlayOneShot(ac, 1);
    }

    private void resetAudioSource(AudioSource source) {
        source.volume = 1;
        source.loop = false;
    }


    public void ChangeBGM(AudioClip music)
    {
        AudioSource current;
        AudioSource next;
        if (currentBGMSource1) {
            current = BGMSource1;
            next = BGMSource2;
            currentBGMSource1 = false;
        }
        else {
            current = BGMSource2;
            next = BGMSource1;
            currentBGMSource1 = true;
        }

        IEnumerator fadeOut = AudioFade(current, current.volume, 0, 0.5f);
        StartCoroutine(fadeOut);

        next.clip = music;
        IEnumerator fadeIn = AudioFade(next, 0, 1, 0.5f);
        StartCoroutine(fadeIn);
    }

    public static IEnumerator AudioFade(AudioSource audioSource, float startVolume, float endVolume, float duration) {
        audioSource.volume = startVolume;
        Timer timer = new Timer(duration);
        float volumeDiff = endVolume - startVolume;
        timer.Start();
        while (!timer.IsDone()) {
            audioSource.volume = startVolume + volumeDiff * timer.TimePercentagePassed();
            yield return new WaitForEndOfFrame();
        }
        audioSource.volume = endVolume;
    }
}
