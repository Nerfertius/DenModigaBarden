using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseState : GameState {

    private Button resumeBtn, quitBtn;
    private Canvas pauseCanvas;
    private string canvas = "UICanvas", resume = "ResumeBtn", quit = "QuitBtn";

    public PauseState(GameManager gm)
    {
        this.gm = gm;
    }

    public override void enter() {
        Time.timeScale = 0;
        pauseCanvas = GameObject.Find("/" + canvas).GetComponent<Canvas>();
        resumeBtn = GameObject.Find("/" + canvas + "/" + resume).GetComponent<Button>();
        quitBtn = GameObject.Find("/" + canvas + "/" + quit).GetComponent<Button>();

        pauseCanvas.enabled = true;
        if (resumeBtn)
            resumeBtn.onClick.AddListener(ResumeState);
        if (quitBtn)
            quitBtn.onClick.AddListener(QuitGame);
    }

    public override void update() {
        if (Input.GetKeyDown(KeyCode.Escape))
            ResumeState();
    }

    public override void exit()
    {
        Time.timeScale = 1;
        pauseCanvas.enabled = false;
    }

    void ResumeState() {
        gm.switchState(new PlayState(gm));
    }

    void QuitGame() {
        Debug.Log("Quit game");
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }

}
