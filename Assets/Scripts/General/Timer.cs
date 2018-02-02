using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer {

	private float startTime;
    private float duration;


    public Timer(float duration) {
        SetDuration(duration);
    }

    public void StartTimer() {
        startTime = Time.time;
    }

    public void SetDuration(float duration) {
        this.duration = duration;
    }

    public bool TimeUp() {
        return (Time.time >= startTime + duration) ;
    }
}
