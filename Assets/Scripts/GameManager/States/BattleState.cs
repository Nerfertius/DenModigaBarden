using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleState : GameState
{
    private CameraFollow2D camScript;
    private Canvas battleCanvas;

    public delegate void BattleEnteredEventHandler();
    public static event BattleEnteredEventHandler BattleEntered;

    public delegate void BattleEndedEventHandler();
    public static event BattleEndedEventHandler BattleEnded;

    public BattleState(GameManager gm)
    {
        this.gm = gm;
        camScript = Camera.main.GetComponent<CameraFollow2D>();
    }

    public override void enter()
    {
        if (BattleEntered != null)
        {
            BattleEntered.Invoke();
        }

        PlayerData.player.CancelPlayingMelody();
        BattleScene.instance.StartBattleMusic();
        GameManager.PlayCanvas.enabled = true;
        EnemyManager.PauseEnemies();
        playerControl = false;
        Time.timeScale = 0;
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
        CameraFX.ZoomIn(1f);
        yield return new WaitForSecondsRealtime(0.5f);
        CameraFX.FadeIn();
        camScript.enabled = false;
        yield return new WaitForSecondsRealtime(1f);
        CameraFX.ResetRenderScreen();
        CameraFX.FadeOut();
        camScript.enabled = false;
        Time.timeScale = 1;
        BattleScene.instance.StartBattle();
    }
}
