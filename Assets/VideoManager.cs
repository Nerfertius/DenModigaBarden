using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour {

    public static VideoManager instance;

    private AudioSource audioSource;
    private VideoPlayer vPlayer;

    public VideoClip clip;
        
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

	void Start () {
        if (instance == null) { 
            instance = this;
        } else
        {
            Debug.LogWarning("You have 2 VideoManagers! This one will be destroyed");
            Destroy(this.gameObject);
        }

        audioSource = GetComponent<AudioSource>();
        vPlayer = GetComponent<VideoPlayer>();
    }

    public void SetVideoClip(VideoClip clip)
    {
        this.clip = clip;
    }

    public void Play()
    {
        AudioManager.SetDefaultBGM(null);
        AudioManager.PlayBGM(null);
        vPlayer.targetCamera = Camera.main;
        vPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
        vPlayer.controlledAudioTrackCount = 1;
        vPlayer.EnableAudioTrack(0, true);
        vPlayer.SetTargetAudioSource(0, audioSource);
        vPlayer.source = VideoSource.VideoClip;
        vPlayer.clip = clip;
        vPlayer.Prepare();
        vPlayer.Play();
    }

    public void Stop() {
        vPlayer.Stop();
    }

    public bool IsPlaying()
    {
        return vPlayer.isPlaying;
    }
}
