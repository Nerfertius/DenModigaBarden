using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameState {
    protected bool playerControl = false;
    protected GameManager gm;

    public bool PlayerControl {
        get {
            return playerControl;
        }
    }

    public GameState() {}

    public GameState(GameManager gm) {
        this.gm = gm;
    }

    public virtual void enter() { }
    public virtual void update() { }
    public virtual void fixedUpdate() { }
    public virtual void exit() { }
}