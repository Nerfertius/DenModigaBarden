using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer {

	private float startTime;
    private float duration;


    public Timer(float duration) {
        SetDuration(duration);
    }

    public void Start() {
        startTime = Time.time;
    }

    public void SetDuration(float duration) {
        this.duration = duration;
    }

    public bool IsDone() {
        return (Time.time >= startTime + duration) ;
    }

    public float TimePassed() {
        return Time.time - startTime;
    }

    public float TimePercentagePassed() {
        return TimePassed() / duration;
    }
}
