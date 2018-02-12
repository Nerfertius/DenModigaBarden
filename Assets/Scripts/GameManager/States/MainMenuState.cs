using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuState : GameState {

    private Button playBtn, optionsBtn, quitBtn;
    private Canvas pauseCanvas;
    private string canvas = "MainMenuCanvas", play = "PlayBtn", options = "OptionsBtn", quit = "QuitBtn";

    public MainMenuState(GameManager gm)
    {
        this.gm = gm;
    }

    public override void enter()
    {
        pauseCanvas = GameObject.Find("/" + canvas).GetComponent<Canvas>();
        playBtn = GameObject.Find("/" + canvas + "/" + play).GetComponent<Button>();
        optionsBtn = GameObject.Find("/" + canvas + "/" + options).GetComponent<Button>();
        quitBtn = GameObject.Find("/" + canvas + "/" + quit).GetComponent<Button>();

        pauseCanvas.enabled = true;
        if (playBtn)
            playBtn.onClick.AddListener(swPlayState);
        if (optionsBtn)
            optionsBtn.onClick.AddListener(OptionsState);
        if (quitBtn)
            quitBtn.onClick.AddListener(QuitGame);
    }

    public override void update()
    {
        
    }

    void swPlayState()
    {
        gm.switchState(new PlayState(gm));
    }

    void OptionsState() {

    }

    void QuitGame()
    {
        Debug.Log("Quit game");
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }
}