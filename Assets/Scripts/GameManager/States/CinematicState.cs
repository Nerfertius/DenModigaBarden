﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CinematicState : GameState {

    public AsyncOperation levelLoad;

    public CinematicState(GameManager gm) {
        this.gm = gm;
    }

    public override void enter()
    {
        levelLoad = gm.loadScene(1);
    }

    public override void update()
    {
        if (levelLoad != null && levelLoad.progress >= 0.9f) {
            levelLoad.allowSceneActivation = true;
            if(levelLoad.isDone)
                gm.switchState(new PlayState(gm));
        }

    }

    public override void exit()
    {
        
    }
}