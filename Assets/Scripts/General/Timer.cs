using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer {

	private float startTime;
    private float duration;

    private bool active;

    private float pauseStartTime = 0;
    private float pauseEndTime = 0;

    private bool useTimeScale = true;

    public Timer(float duration, bool useTimeScale) {
        active = false;
        SetDuration(duration);
        this.useTimeScale = useTimeScale;
    }

    public Timer(float duration) : this(duration, true) {
        
    }

    public void Start() {
        startTime = Time();
        active = true;
    }

    public void InstantFinish() {
        startTime = -duration;
    }

    public void Pause() {
        pauseStartTime = Time();
        active = false;
    }

    public void UnPause() {
        pauseEndTime = Time();
        active = true;
        pauseEndTime = 0;
        pauseStartTime = 0;
    }

    public void SetDuration(float duration) {
        this.duration = duration;
    }

    public float GetDuration() {
        return duration;
    }

    public bool IsDone() {
        return TimePassed() >= duration;
    }

    public bool IsActive() {
        return active;
    }

    public float TimePassed() {
        if (!active) {
            return Time() - startTime - (Time() - pauseStartTime);
        }
        else {
            return Time() - startTime - (pauseEndTime - pauseStartTime);
        }
    }

    public float TimeLeft() {
        return duration - TimePassed();
    }

    public float TimePercentagePassed() {
        return TimePassed() / duration;
    }

    private float Time(){
        return useTimeScale ? UnityEngine.Time.time : UnityEngine.Time.unscaledTime;
    }
}
