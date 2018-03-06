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
        if (TransitionEntered != null)
        {
            TransitionEntered();
        }

        Time.timeScale = 0;
    }

    public override void exit()
    {
        Time.timeScale = 1;
        
        if (TransitionExited != null)
        {
            TransitionExited();
        }
    }

    public override void update()
    {
    }
}
