using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicState : GameState {

    public AsyncOperation levelLoad;

    public CinematicState(GameManager gm) {
        this.gm = gm;
    }

    public override void enter()
    {
        VideoManager.instance.Play();
        gm.StartCoroutine(DeactivateCanvas());
        levelLoad = gm.loadScene(1);
    }

    IEnumerator DeactivateCanvas()
    {
        while (!VideoManager.instance.IsPlaying())
        {
            yield return new WaitForSeconds(1f);
        }
        GameManager.MainMenuCanvas.enabled = false;
    }
    
    public override void update()
    {
        if (levelLoad != null && levelLoad.progress >= 0.9f)
        {
            if (!VideoManager.instance.IsPlaying())
            {
                levelLoad.allowSceneActivation = true;

                if (levelLoad.isDone)
                {
                    gm.switchState(new PlayState(gm));
                }
            }

        }

    }

    public override void exit()
    {
    }
}