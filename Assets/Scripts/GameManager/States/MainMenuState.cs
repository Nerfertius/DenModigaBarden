using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuState : GameState {

    private Button playBtn, optionsBtn, quitBtn;
    private string canvas = "MainMenuCanvas", play = "PlayBtn", options = "OptionsBtn", quit = "QuitBtn";

    public MainMenuState(GameManager gm)
    {
        this.gm = gm;
    }

    public override void enter()
    {
        playBtn = gm.MainMenuCanvas.transform.Find(play).GetComponent<Button>();
        optionsBtn = gm.MainMenuCanvas.transform.Find(options).GetComponent<Button>();
        quitBtn = gm.MainMenuCanvas.transform.Find(quit).GetComponent<Button>();

        gm.MainMenuCanvas.enabled = true;
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

    public override void exit()
    {
        gm.MainMenuCanvas.enabled = false;
        playBtn.onClick.RemoveAllListeners();
        optionsBtn.onClick.RemoveAllListeners();
        quitBtn.onClick.RemoveAllListeners();
    }

    void swPlayState()
    {
        gm.switchState(new CinematicState(gm));
    }

    void OptionsState() {
        Debug.Log("Options");
    }

    void QuitGame()
    {
        Debug.Log("Quit game");
        Application.Quit();
        //UnityEditor.EditorApplication.isPlaying = false;
    }
}