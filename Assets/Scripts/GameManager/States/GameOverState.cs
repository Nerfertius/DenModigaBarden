using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverState : GameState {

    Canvas gameOver;
    Button continueBtn, quitBtn;

    public GameOverState(GameManager gm)
    {
        this.gm = gm;
    }
    
    public override void enter()
    {
        GameManager.GameOverCanvas.enabled = true;
        gameOver = GameManager.GameOverCanvas;
        continueBtn = gameOver.transform.Find("Continue").GetComponent<Button>();
        quitBtn = gameOver.transform.Find("Quit").GetComponent<Button>();

        if (continueBtn)
            continueBtn.onClick.AddListener(ContinueState);
        if (quitBtn)
            quitBtn.onClick.AddListener(QuitGame);
    }

    public override void update()
    {

    }

    public override void exit()
    {
        GameManager.GameOverCanvas.enabled = false;
    }

    void ContinueState()
    {
        gm.player.CallRespawn();
    }

    void QuitGame()
    {
        Debug.Log("Quit game");
        Application.Quit();
        //UnityEditor.EditorApplication.isPlaying = false;
    }
}