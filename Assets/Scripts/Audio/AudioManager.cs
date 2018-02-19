using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public GameObject audioSourceObject;
    public int numberOfpooledObjects;

    private static AudioManager instance;
    public static AudioManager Instance {
        get { return instance; }
    }

    private ComponentPool<AudioSource> audioSourcePool;
    private AudioSource oneShotSource;
    private LinkedList<AudioSource> activeAudioSources;

    private AudioSourceBuffer bgm;
    private AudioSourceBuffer note;

    //public mainly for testing purpose but could be nice anyway
    public AudioClip defaultBGM;
    
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

        bgm = new AudioSourceBuffer();
        bgm.Loop = true;
        bgm.AllowSameClip = false;
        bgm.SetFadeIn(0, 1, 1);
        bgm.SetFadeOut(1, 0, 0.4f);

        note = new AudioSourceBuffer();
        note.Loop = false;
        note.SetFadeOut(1, 0, 0.3f);

        oneShotSource = audioSourcePool.Get();
        ResetAudioSource(oneShotSource);
    }
    /*
    private void Update() {
        LinkedListNode<AudioSource> node = activeAudioSources.First;

        while(node != null){
            if(node.Value.time >= node.Value.clip.length) {
                audioSourcePool.Free(node.Value);
                activeAudioSources.Remove(node.Value);
            }
            node = node.Next;
        }
    }

    public void PlayOneShot(AudioClip ac) {
        AudioSource source = audioSourcePool.Get();
        ResetAudioSource(source);
        source.clip = ac;
        source.Play();
        activeAudioSources.AddLast(source);
    }
    */
    public void PlayOneShot(AudioClip ac) {
        oneShotSource.PlayOneShot(ac);
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
        source.mute = false;
        source.bypassEffects = false;
        source.bypassListenerEffects = false;
        source.bypassReverbZones = false;
        source.playOnAwake = true;
        source.loop = false;
        source.priority = 128;
        source.volume = 1;
        source.pitch = 1;
        source.panStereo = 0;
        source.spatialBlend = 0;
        source.reverbZoneMix = 1;   
    }


    public void PlayBGM(AudioClip music)
    {
        bgm.Play(music); 
    }

    public void PlayDefaultBGM() {
        PlayBGM(defaultBGM);
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
        instance.FreeAudioSource(audioSource);
    }

    private class AudioSourceBuffer {

        private AudioSource current;
        
        //Settings
        private float fadeInStartVolume = 1;
        private float fadeInEndVolume = 1;
        private float fadeInDuration = 0;

        private float fadeOutStartVolume = 1;
        private float fadeOutEndVolume = 1;
        private float fadeOutDuration = 0;

        public bool Loop = false;
        public bool AllowSameClip = true;

        public AudioSourceBuffer() {
            current = AudioManager.instance.GetAudioSource();
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
            if(!AllowSameClip && current.clip == clip) {
                return;
            }

            //fade out
            AudioManager.instance.StartCoroutine(AudioManager.AudioFadeAndStop(current, fadeOutStartVolume, fadeOutEndVolume, fadeOutDuration));
            if(clip != null) {
                current = AudioManager.instance.GetAudioSource();

                current.loop = Loop;
                current.clip = clip;
                current.Play();
                AudioManager.instance.StartCoroutine(AudioManager.AudioFade(current, fadeInStartVolume, fadeInEndVolume, fadeInDuration));
            }
        }
    }

    /*
    private class AudioSourceBuffer {

        private List<AudioSource> sources;
        private int current;
        
        //Settings
        private float fadeInStartVolume = 1;
        private float fadeInEndVolume = 1;
        private float fadeInDuration = 0;

        private float fadeOutStartVolume = 1;
        private float fadeOutEndVolume = 1;
        private float fadeOutDuration = 0;

        public bool Loop = false;
        public bool AllowSameClip = true;

        public AudioSourceBuffer(int size) {
            sources = new List<AudioSource>(size);
            current = 0;
            for (int i = 0; i < size; i++) {
                sources.Add(AudioManager.instance.audioSourcePool.Get());
                AudioManager.ResetAudioSource(sources[i]);
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
            if(!AllowSameClip && sources[current].clip == clip) {
                return;
            }

            //fade out
            AudioManager.instance.StartCoroutine(AudioManager.AudioFadeAndStop(sources[current], fadeOutStartVolume, fadeOutEndVolume, fadeOutDuration));
            if(clip != null) {
                current++;
                if (current >= sources.Count) {
                    current = 0;
                }

                AudioManager.ResetAudioSource(sources[current]);
                sources[current].loop = Loop;
                sources[current].clip = clip;
                sources[current].Play();
                AudioManager.instance.StartCoroutine(AudioManager.AudioFade(sources[current], fadeInStartVolume, fadeInEndVolume, fadeInDuration));
            }
        }
    }
    */
}
