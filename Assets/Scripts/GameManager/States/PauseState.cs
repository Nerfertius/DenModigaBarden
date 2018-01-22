using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseState : GameState {

    public PauseState(GameManager gm)
    {
        this.gm = gm;
    }

    public override void enter() {
        Time.timeScale = 0;
    }

    public override void update() {
        if (Input.GetKeyDown(KeyCode.Escape))
            gm.switchState(new PlayState(gm));
    }

    public override void exit()
    {
        Time.timeScale = 1;
    }

}
