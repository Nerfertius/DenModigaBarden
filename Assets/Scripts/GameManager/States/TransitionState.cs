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
        gm.hideCanvas("PlayCanvas");
    }

    public override void exit()
    {
        gm.showCanvas("PlayCanvas");
    }

    public override void update()
    {

    }
}
