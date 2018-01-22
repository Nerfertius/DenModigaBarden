using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayState : GameState {

    public StateController player;

    public PlayState(GameManager gm)
    {
        playerControl = true;
        this.gm = gm;
    }

    public override void enter() {
        findPlayer();
    }

    public override void update() {
        if (player != null) {
            //TODO: change correct var
            if (player.currentState.name == "PlayerDead") {
                gm.switchState(new GameOverState(gm));
            }
        }
        if(Input.GetKeyDown(KeyCode.Escape))
            gm.switchState(new PauseState(gm));
    }

    public void findPlayer() {
        player = GameObject.FindWithTag("Player").GetComponent<StateController>();
    }
}
