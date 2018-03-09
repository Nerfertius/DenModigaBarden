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
        PlayClickSound();
        gm.switchState(new CinematicState(gm));
    }

    void OptionsState() {

        
    }

    private void Listeners() {

        PlayClickSound();
        showOptions = !showOptions;
        menuAnimator.SetBool("options", showOptions);
        if (showOptions)
        {
            optionsBtn.navigation = navOpenOptions[0];
            quitBtn.navigation = navOpenOptions[1];
        }
        else
        {
            optionsBtn.navigation = navCloseOptions[0];
            quitBtn.navigation = navCloseOptions[1];
        }
    }

    private void PlayClickSound() {
        AudioSource click = GameManager.MainMenuCanvas.GetComponent<AudioSource>();
        if (click)
        {
            click.Play();
        }
        else
        {
            Debug.LogWarning("No AudioSource on MainMenuCanvas");
        }
    }

    private void Setup() {

        if (GameManager.MainMenuCanvas == null)
            return;
        playBtn = GameManager.MainMenuCanvas.transform.Find(play).GetComponent<Button>();
        optionsBtn = GameManager.MainMenuCanvas.transform.Find(options).GetComponent<Button>();
        quitBtn = GameManager.MainMenuCanvas.transform.Find(quit).GetComponent<Button>();

        audio = GameManager.MainMenuCanvas.transform.Find("OptionsMenu/MasterAudio").GetComponent<Slider>();
        audio.value = AudioListener.volume;
        bgAudio = GameManager.MainMenuCanvas.transform.Find("OptionsMenu/BGAudio").GetComponent<Slider>();
        bgAudio.value = GameManager.instance.bgAudio;
        effectAudio = GameManager.MainMenuCanvas.transform.Find("OptionsMenu/EffectAudio").GetComponent<Slider>();
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
            bgAudio.onValueChanged.AddListener(volume => AudioManager.SetBGMVolume(volume));
        if (effectAudio)
            effectAudio.onValueChanged.AddListener(volume => AudioManager.SetSoundEffectVolume(volume));
    }

    void QuitGame()
    {
        PlayClickSound();
        Application.Quit();
        //UnityEditor.EditorApplication.isPlaying = false;
    }
}