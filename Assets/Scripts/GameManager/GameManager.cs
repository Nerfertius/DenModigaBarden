using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public delegate void StateChange(GameState newState);
    public static event StateChange ChangeState;

    private GameState current = null;

    void Start() {
        switchState(new PlayState(this));
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
        ChangeState(current);
    }
}