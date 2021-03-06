﻿using System.Collections;
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

    public AudioSourceBuffer bgm;
    public AudioSourceBuffer notes;

    public AudioSourceBuffer ambience;

    public float masterVolume = 1;
    public float soundEffectVolume = 1;
    public float bgmVolume = 1;

    //public mainly for testing purpose but could be nice anyway
    public AudioClip defaultBGM;

    void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(transform.gameObject);
            instance = this;
        }
        else
        {
            Debug.LogError("There should only be one audioManager " + this.gameObject);
        }
    }

    private void Start()
    {
        audioSourcePool = new ComponentPool<AudioSource>(audioSourceObject, numberOfpooledObjects, this.transform);
        activeAudioSources = new LinkedList<AudioSource>();

        bgm.Init();
        notes.Init();
        notes.useSoundEffectVolume = true;

        /*
        bgm = new AudioSourceBuffer();
        bgm.Loop = true;
        bgm.AllowSameClip = false;
        bgm.fullFadeInDuration = 1;
        bgm.fullFadeOutDuration = 0.4f;
        bgm.partialFadeInDuration = 0.5f;
        bgm.partialFadeOutDuration = 0.5f;

        notes = new AudioSourceBuffer();
        notes.Loop = false;
        notes.fullFadeOutDuration = 0.3f;
        */
        oneShotSource = audioSourcePool.Get();
        ResetAudioSource(oneShotSource);

        SetBGMVolume(bgmVolume);
        SetSoundEffectVolume(soundEffectVolume);
        SetMasterVolume(masterVolume);
    }

    public static void SetBGMVolume(float volume) {
        instance.bgmVolume = volume;
        instance.bgm.SetDefaultVolume(instance.masterVolume * instance.bgmVolume);
        instance.ambience.SetDefaultVolume(instance.masterVolume * instance.bgmVolume);
    }
    public static void SetSoundEffectVolume(float volume) {
        instance.soundEffectVolume = volume;
        instance.oneShotSource.volume = instance.soundEffectVolume * instance.masterVolume;
        instance.notes.SetDefaultVolume(instance.masterVolume * instance.soundEffectVolume);
    }
    public static void SetMasterVolume(float volume) {
        instance.masterVolume = volume;

        SetBGMVolume(instance.bgmVolume);
        SetSoundEffectVolume(instance.soundEffectVolume);
    }

    public void Update() {
        bgm.Update();
        notes.Update();

        /*SetBGMVolume(bgmVolume);
        SetSoundEffectVolume(soundEffectVolume);
        SetMasterVolume(masterVolume);
        */
    }

    private static float getBGMVolume() {
        return instance.masterVolume * instance.bgmVolume;
    }

    private static float getSoundEffectVolume() {
        return instance.masterVolume * instance.soundEffectVolume;
    }

    // =========================== Pool stuff =================================================================================
    public static AudioSource GetAudioSource() {
        AudioSource source = instance.audioSourcePool.Get();
        ResetAudioSource(source);
        return source;
    }

    public static void FreeAudioSource(AudioSource source) {
        instance.audioSourcePool.Free(source);
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

    // =========================== Play OneShot =================================================================================
    public static void PlayOneShot(AudioClip ac) {
        PlayOneShot(ac, 1);
    }

    public static void PlayOneShot(AudioClip ac, float volume) {
        instance.oneShotSource.PlayOneShot(ac, volume * getSoundEffectVolume());
    }

    // =========================== BGM =================================================================================

    public static void PlayBGM(AudioClip music) {
        instance.bgm.Play(music); 
    }
    public static void PlayBGM(AudioClip music, bool fadeInOnPlay)
    {
        instance.bgm.FadeInOnPlay = fadeInOnPlay;
        instance.bgm.Play(music);
    }

    public static void PlayDefaultBGM() {
        PlayBGM(instance.defaultBGM);
    }

    public static void SetDefaultBGM(AudioClip clip) {
        AudioClip previous = instance.defaultBGM;
        instance.defaultBGM = clip;
        if (instance.bgm.getCurrentClip() == null || instance.bgm.getCurrentClip() == previous) {
            PlayDefaultBGM();
        }
    }

    public static void FadeBGM() {
        instance.bgm.UseFadedVolume();
    }
    public static void FadeBGMBackToNormal() {
        instance.bgm.UseDefaultVolume();
    }

 // =========================== Notes =================================================================================
    public static void PlayNote(AudioClip music) {
        instance.notes.Play(music);
    }

//  =========================== Ambience =================================================================================
    public static void PlayAmbience(AudioClip music) {
        instance.ambience.Play(music);
    }

    // =========================== Effects =================================================================================
    public static IEnumerator AudioFade(AudioSource audioSource, float startVolume, float endVolume, float duration) {
        audioSource.volume = startVolume;
        Timer timer = new Timer(duration, false);
        
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
        FreeAudioSource(audioSource);
    }

    // =========================== AudioSourceBuffer =================================================================================
    [System.Serializable]
    public class AudioSourceBuffer {

        private AudioSource current;

        //Settings
        private float volume = 1;
        [HideInInspector] public bool useSoundEffectVolume = false;
        [Range(0, 1)]
        [Tooltip("Lower volume when playing flute to this percentage of volume (not used for notes)")]
        [SerializeField] private float fadedVolume = 0.3f;

        [Tooltip("Volume fade speed per second when changing volume")]
        [SerializeField] private float volumeFadeSpeed = 1f;

        [Tooltip("Fade out duration on AudioClip change")]
        [SerializeField] private float fullFadeOutDuration = 0;

        [SerializeField] public bool FadeInOnPlay = false;
        [SerializeField] private bool Loop = false;

        [Tooltip("On true, cancels previous audioclip and plays it again")]
        [SerializeField] private bool AllowSameClip = true;

        private bool useFadedVolume = false;

        public void Init() {
            current = AudioManager.GetAudioSource();
        }

        public AudioClip getCurrentClip() {
            if(current != null) {
                return current.clip;
            }
            return null;
        }

        public void Play(AudioClip clip) {
            
            if (current != null && !AllowSameClip && current.clip == clip) {
                return;
            }
            if(current != null) {
                //fade out
                AudioManager.instance.StartCoroutine(AudioManager.AudioFadeAndStop(current, current.volume, 0, fullFadeOutDuration));
            }

            if (clip != null) {
                current = AudioManager.GetAudioSource();

                current.loop = Loop;
                current.clip = clip;
                current.Play();
                if (FadeInOnPlay) {
                    current.volume = 0;
                }
                else {
                    if (useFadedVolume) {
                        current.volume = fadedVolumeBasedOnDefault;
                    }
                    else {
                        current.volume = volume;
                    }
                }
            }
            else {
                current = null;
            }
        }

        public void Update() {
            if(current != null) {
                float targetVolume = useFadedVolume ? fadedVolumeBasedOnDefault : volume;
                if (current.volume != targetVolume) {
                    float volumeDifference = current.volume - targetVolume;
                    float deltaVolumeFadeSpeed = volumeFadeSpeed * Time.deltaTime;

                    if (Mathf.Abs(volumeDifference) <= deltaVolumeFadeSpeed) {
                        current.volume = targetVolume;
                    }
                    else if (volumeDifference > 0) {//fade down
                        current.volume -= deltaVolumeFadeSpeed;
                    }
                    else {//fade up
                        current.volume += deltaVolumeFadeSpeed;
                    }

                }
            }
        }

        private float fadedVolumeBasedOnDefault {
            get { return fadedVolume * volume; }
        }

        public void UseFadedVolume() {
            useFadedVolume = true;
        }

        public void UseDefaultVolume() {
            useFadedVolume = false;
        }

        public void SetDefaultVolume(float volume) {
            this.volume = volume;
        }
    }



    /*private class AudioSourceBuffer {

        private AudioSource current;

        //Settings

        [SerializeField] private float defaultVolume = 1;
        [SerializeField] private float fullFadeInDuration = 0;
        [SerializeField] private float fullFadeOutDuration = 0;
        [SerializeField] private bool Loop = false;
        [SerializeField] private bool AllowSameClip = true;

        private bool isFaded = false;
        [SerializeField] private float fadedVolume = 0.3f;
        [SerializeField] private float partialFadeInDuration = 0;
        [SerializeField] private float partialFadeOutDuration = 0;

        public AudioSourceBuffer() {
            current = AudioManager.GetAudioSource();
        }

        public AudioClip getCurrentClip() {
            return current.clip;
        }

        public void Play(AudioClip clip) {
            if(!AllowSameClip && current.clip == clip) {
                return;
            }
            AudioSource previous = current;

            //fade out
            AudioManager.instance.StartCoroutine(AudioManager.AudioFadeAndStop(current, current.volume, 0, fullFadeOutDuration));
            if(clip != null) {
                current = AudioManager.GetAudioSource();

                current.loop = Loop;
                current.clip = clip;
                current.Play();
                if (!isFaded) {
                    AudioManager.instance.StartCoroutine(AudioManager.AudioFade(current, 0, defaultVolume, fullFadeInDuration));
                }
                else {
                    AudioManager.instance.StartCoroutine(AudioManager.AudioFade(current, 0, fadedVolume, fullFadeInDuration));
                }
            }
        }

        public void FadePartialVolumeOut() {
            AudioManager.instance.StartCoroutine(AudioManager.AudioFade(current, current.volume, fadedVolume, partialFadeOutDuration));
        }

        public void FadeVolumeBackToNormal() {
            AudioManager.instance.StartCoroutine(AudioManager.AudioFade(current, current.volume, defaultVolume, partialFadeInDuration));
        }
    }
    */
}
