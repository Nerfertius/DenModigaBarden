using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleState : GameState
{
    private CameraFollow2D camScript;
    private Canvas battleCanvas;
    
    public delegate void BattleEndedEventHandler();
    public static event BattleEndedEventHandler BattleEnded;

    public BattleState(GameManager gm)
    {
        this.gm = gm;
        camScript = Camera.main.GetComponent<CameraFollow2D>();
    }

    public override void enter()
    {
        gm.PlayCanvas.enabled = true;
        Time.timeScale = 0;
        EnemyManager.PauseEnemies();
        playerControl = false;

        gm.StartCoroutine(StartBattle());
    }

    public override void exit()
    {
        if (BattleEnded != null)
        {
            BattleEnded.Invoke();
        }

        AudioManager.PlayDefaultBGM();
        CameraFX.FadeOut();
        camScript.enabled = true;
        camScript.UpdateToMapBounds();
    }

    public override void update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log("Exiting Battle State");
            GameManager.instance.switchState(new PlayState(GameManager.instance));
        }

        HP.GetInstance.Update();
    }

    IEnumerator StartBattle()
    {
        CameraFX.FadeIn();
        camScript.enabled = false;
        yield return new WaitForSecondsRealtime(1);
        CameraFX.FadeOut();
        camScript.enabled = false;
        Time.timeScale = 1;
        BattleScene.instance.StartBattle();
    }
}
