using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public GameObject audioSourceObject;
    public int numberOfpooledObjects;

    public static AudioManager instance;

    private ComponentPool<AudioSource> audioSourcePool;
    private LinkedList<AudioSource> activeAudioSources;

    private AudioSourceBuffer bgm;

    private AudioSourceBuffer note;

    private AudioClip defaultBGM;
    
    private void Start()
    {
        if(instance == null) {
            instance = this;
        }
        else {
            Debug.LogError("There should only be one audioManager " + this.gameObject);
        }

        audioSourcePool = new ComponentPool<AudioSource>(audioSourceObject, numberOfpooledObjects, this.transform);
        activeAudioSources = new LinkedList<AudioSource>();

        bgm = new AudioSourceBuffer(2);
        bgm.SetFadeIn(0, 1, 1);
        bgm.SetFadeOut(1, 0, 0.4f);

        note = new AudioSourceBuffer(10);
        note.SetFadeOut(1, 0, 0.3f);
    }

    private void Update() {
        foreach(AudioSource s in activeAudioSources) {
            if(s.time >= s.clip.length) {
                audioSourcePool.Free(s);
                activeAudioSources.Remove(s);
            }
        }
    }

    public void PlayOneShot(AudioClip ac) {
        AudioSource source = audioSourcePool.Get();
        ResetAudioSource(source);
        source.clip = ac;
        source.Play();
        activeAudioSources.AddLast(source);
    }

    public AudioSource GetAudioSource() {
        AudioSource source = audioSourcePool.Get();
        ResetAudioSource(source);
        return source;
    }

    public void FreeAudioSource(AudioSource source) {
        audioSourcePool.Free(source);
    }

    public static void ResetAudioSource(AudioSource source) {
        source.volume = 1;
        source.loop = false;
    }


    public void PlayBGM(AudioClip music)
    {
        if(music == null) {
            bgm.Play(defaultBGM);
        }

        bgm.Play(music); 
    }

    public void SetDefaultBGM(AudioClip clip) {
        defaultBGM = clip;
    }

    public void PlayNote(AudioClip music) {
        note.Play(music);
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

    public static IEnumerator AudioFadeAndStop(AudioSource audioSource, float startVolume, float endVolume, float duration) {
        yield return AudioFade(audioSource, startVolume, endVolume, duration);

        audioSource.Stop();
    }


    private class AudioSourceBuffer {

        private List<AudioSource> sources;
        private int current;

        
        private float fadeInStartVolume = 1;
        private float fadeInEndVolume = 1;
        private float fadeInDuration = 0;

        private float fadeOutStartVolume = 1;
        private float fadeOutEndVolume = 1;
        private float fadeOutDuration = 0;

        public AudioSourceBuffer(int size) {
            sources = new List<AudioSource>(size);
            current = 0;
            for (int i = 0; i < size; i++) {
                sources.Add(AudioManager.instance.audioSourcePool.Get());
            }
        }

        public void SetFadeOut(float fadeOutStartVolume, float fadeOutEndVolume, float fadeOutDuration) {
            this.fadeOutStartVolume = fadeOutStartVolume;
            this.fadeOutEndVolume = fadeOutEndVolume;
            this.fadeOutDuration = fadeOutDuration;
        }

        public void SetFadeIn(float fadeInStartVolume, float fadeInEndVolume, float fadeInDuration) {
            this.fadeInStartVolume = fadeInStartVolume;
            this.fadeInEndVolume = fadeInEndVolume;
            this.fadeInDuration = fadeInDuration;
        }

        public void Play(AudioClip clip) {
            //fade out
            AudioManager.instance.StartCoroutine(AudioManager.AudioFadeAndStop(sources[current], fadeOutStartVolume, fadeOutEndVolume, fadeOutDuration));
            if(clip != null) {
                current++;
                if (current >= sources.Count) {
                    current = 0;
                }

                AudioManager.ResetAudioSource(sources[current]);
                sources[current].clip = clip;
                sources[current].loop = true;
                sources[current].Play();
                AudioManager.instance.StartCoroutine(AudioManager.AudioFade(sources[current], fadeInStartVolume, fadeInEndVolume, fadeInDuration));
            }
        }
    }
}
