﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverState : GameState {

    public GameOverState(GameManager gm)
    {
        this.gm = gm;
    }
    
    public override void enter()
    {
        gm.player.CallRespawn();
        gm.switchState(new PlayState(gm));
    }
}