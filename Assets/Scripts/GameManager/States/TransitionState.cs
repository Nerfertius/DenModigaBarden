using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionState : GameState {

    public TransitionState(GameManager gm)
    {
        this.gm = gm;
    }

    public override void enter()
    {
        Time.timeScale = 0;
        //gm.hideCanvas("PlayCanvas");
    }

    public override void exit()
    {
        Time.timeScale = 1;
        //gm.showCanvas("PlayCanvas");
    }

    public override void update()
    {

    }
}
