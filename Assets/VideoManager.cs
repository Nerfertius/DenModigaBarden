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
        instance = this;

        audioSource = GetComponent<AudioSource>();
        vPlayer = GetComponent<VideoPlayer>();
    }

    public void SetVideoClip(VideoClip clip)
    {
        this.clip = clip;
    }

    public void Play()
    {
        vPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
        vPlayer.controlledAudioTrackCount = 1;
        vPlayer.EnableAudioTrack(0, true);
        vPlayer.SetTargetAudioSource(0, audioSource);
        vPlayer.source = VideoSource.VideoClip;
        vPlayer.clip = clip;
        vPlayer.Prepare();
        vPlayer.Play();
    }

    public bool IsPlaying()
    {
        return vPlayer.isPlaying;
    }
}
