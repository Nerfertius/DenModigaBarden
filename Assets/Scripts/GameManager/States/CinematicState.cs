using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CinematicState : GameState {

    public AsyncOperation levelLoad;
    private float skipProg = 0, addTime = 0.6f, remTime = 0.8f;
    private CanvasGroup skipGroup;

    private bool videoStartedPlaying = false;

    public CinematicState(GameManager gm) {
        this.gm = gm;
    }

    public override void enter()
    {
        VideoManager.instance.Play();
        gm.StartCoroutine(DeactivateCanvas());
        levelLoad = gm.loadScene(1);
        skipGroup = GameManager.CinematicCanvas.GetComponentInChildren<CanvasGroup>();
    }

    IEnumerator DeactivateCanvas()
    {
        while (!VideoManager.instance.IsPlaying())
        {
            yield return new WaitForSeconds(1f);
        }
        
        videoStartedPlaying = true;
        GameManager.MainMenuCanvas.enabled = false;
    }
    
    public override void update()
    {
        if (levelLoad != null && levelLoad.progress >= 0.9f)
        {
            if (gm.skipBtn != null)
            {
                if (skipGroup.alpha == 1)
                {
                    if (Input.GetButton("Interact"))
                    {
                        skipProg += addTime * Time.deltaTime;
                        if (skipProg >= 1)
                        {
                            VideoManager.instance.Stop();
                        }
                    }
                    else
                    {
                        skipProg -= remTime * Time.deltaTime;
                        skipProg = skipProg < 0 ? 0 : skipProg;
                    }
                    gm.progressBar.fillAmount = skipProg;
                } else
                {
                    if(Input.GetButton("Interact"))
                        skipGroup.alpha = 1;
                }
            }
                
            if (!VideoManager.instance.IsPlaying() && videoStartedPlaying)
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
        videoStartedPlaying = false;
    }
}