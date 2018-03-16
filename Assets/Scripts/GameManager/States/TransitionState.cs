using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionState : GameState {

    public delegate void TransitionEnterEventHandler();
    public static event TransitionEnterEventHandler TransitionEntered;

    public delegate void TransitionExitEventHandler();
    public static event TransitionExitEventHandler TransitionExited;

    public TransitionState(GameManager gm)
    {
        this.gm = gm;
    }

    public override void enter()
    {
        Time.timeScale = 0;

        if (TransitionEntered != null)
        {
            TransitionEntered();
        }
        //gm.hideCanvas("PlayCanvas");
    }

    public override void exit()
    {
        Time.timeScale = 1;
        
        if (TransitionExited != null)
        {
            TransitionExited();
        }
        //gm.showCanvas("PlayCanvas");
    }

    public override void update()
    {
    }
}
