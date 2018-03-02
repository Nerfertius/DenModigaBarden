using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuState : GameState {

    private Button playBtn, optionsBtn, quitBtn;
    private Slider audio, bgAudio, effectAudio;
    private string canvas = "MainMenuCanvas", play = "PlayBtn", options = "OptionsBtn", quit = "QuitBtn";

    public MainMenuState(GameManager gm)
    {
        this.gm = gm;
    }

    public override void enter()
    {
        Listeners();
    }

    public override void update()
    {
        if(quitBtn == null)
            Listeners();
    }

    public override void exit()
    {
        GameManager.MainMenuCanvas.enabled = false;
        playBtn.onClick.RemoveAllListeners();
        optionsBtn.onClick.RemoveAllListeners();
        quitBtn.onClick.RemoveAllListeners();
    }

    void SwPlayState()
    {
        gm.switchState(new CinematicState(gm));
    }

    void OptionsState() {
        
    }

    private void Listeners() {
        if (GameManager.MainMenuCanvas == null)
            return;
        playBtn = GameManager.MainMenuCanvas.transform.Find(play).GetComponent<Button>();
        optionsBtn = GameManager.MainMenuCanvas.transform.Find(options).GetComponent<Button>();
        quitBtn = GameManager.MainMenuCanvas.transform.Find(quit).GetComponent<Button>();

        audio = GameManager.MainMenuCanvas.transform.Find("MasterAudio").GetComponent<Slider>();
        audio.value = AudioListener.volume;
        bgAudio = GameManager.MainMenuCanvas.transform.Find("BGAudio").GetComponent<Slider>();
        bgAudio.value = GameManager.instance.bgAudio;
        effectAudio = GameManager.MainMenuCanvas.transform.Find("EffectAudio").GetComponent<Slider>();
        effectAudio.value = GameManager.instance.effectAudio;

        GameManager.MainMenuCanvas.enabled = true;
        if (GameManager.instance)
        if (playBtn)
            playBtn.onClick.AddListener(SwPlayState);
        if (optionsBtn)
            optionsBtn.onClick.AddListener(OptionsState);
        if (quitBtn)
            quitBtn.onClick.AddListener(QuitGame);
        if (audio)
            audio.onValueChanged.AddListener(volume => AudioListener.volume = volume);

        if (bgAudio)
            bgAudio.onValueChanged.AddListener(volume => GameManager.instance.bgAudio = volume);
        if (effectAudio)
            effectAudio.onValueChanged.AddListener(volume => GameManager.instance.effectAudio = volume);
    }

    void QuitGame()
    {
        Application.Quit();
        //UnityEditor.EditorApplication.isPlaying = false;
    }
}