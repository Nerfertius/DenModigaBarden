using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer {

	private float startTime;
    private float duration;

    private bool active;

    private float pauseStartTime = 0;
    private float pauseEndTime = 0;

    public Timer(float duration) {
        active = false;
        SetDuration(duration);
    }

    public void Start() {
        startTime = Time.time;
        active = true;
    }

    public void InstantFinish() {
        startTime = -duration;
    }

    public void Pause() {
        pauseStartTime = Time.time;
        active = false;
    }

    public void UnPause() {
        pauseEndTime = Time.time;
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
            return Time.time - startTime - (Time.time - pauseStartTime);
        }
        else {
            return Time.time - startTime - (pauseEndTime - pauseStartTime);
        }
    }

    public float TimeLeft() {
        return duration - TimePassed();
    }

    public float TimePercentagePassed() {
        return TimePassed() / duration;
    }
}
