﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public delegate void StateChange(GameState newState);
    public static event StateChange ChangeState;

    public static GameManager instance;

    public GameState current = null;

    public Sprite fullHeart, halfHeart, emptyHeart;

    void Start() {
        instance = this;
        if (current == null)
        {
            switchState(new PlayState(this));
        }
        else {
            current.enter();
            if (ChangeState != null)
                ChangeState(current);
        }
    }

    void Update() {
        if(current != null)
            current.update();
    }

    public void switchState(GameState next) {
        if(current != null)
            current.exit();
        current = next;
        current.enter();
        if(ChangeState != null)
            ChangeState(current);
    }

    public void hideCanvas(string canvas) {
        GameObject go = GameObject.Find("/" + canvas);
        go.GetComponent<CanvasGroup>().alpha = 0;
    }

    public void showCanvas(string canvas) {
        GameObject go = GameObject.Find("/" + canvas);
        go.GetComponent<CanvasGroup>().alpha = 1;
    }
}