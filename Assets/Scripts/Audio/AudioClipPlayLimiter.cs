using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// <summary> Used as a wrapper for an audioclip if it might be played multiple times at the same time </summary>
public class AudioClipPlayLimiter {

    public AudioClip clip;

    private Timer timer;

    public AudioClipPlayLimiter(AudioClip clip) {
        this.clip = clip;
        timer = new Timer(0.1f, false);
        timer.Start();
        timer.InstantFinish();
    }

    public void Play() {
        if (timer.IsDone()) {
            AudioManager.PlayOneShot(clip);
            timer.Start();
        }
    }
}
