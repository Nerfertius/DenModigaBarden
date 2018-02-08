using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public delegate void StateChange(GameState newState);
    public static event StateChange ChangeState;

    public GameState current = null;

    public Sprite fullHeart, halfHeart, emptyHeart;

    void Start() {
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
}