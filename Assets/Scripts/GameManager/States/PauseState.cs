using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseState : GameState {

    private Button resumeBtn, optionsBtn, quitBtn;
    private Slider audio, bgAudio, effectAudio;
    private Canvas pauseCanvas;
    private string canvas = "PauseCanvas", resume = "ResumeBtn", quit = "QuitBtn";
    private bool showOptions = false;

    private Animator menuAnimator;
    private Navigation[] navCloseOptions, navOpenOptions;

    public PauseState(GameManager gm)
    {
        this.gm = gm;
    }

    public override void enter() {
        Time.timeScale = 0;
        pauseCanvas = GameManager.PauseCanvas;
        pauseCanvas.enabled = true;

        Setup();
    }

    public override void update() {
        if (Input.GetButtonDown("Cancel"))
            ResumeState();
    }

    public override void exit()
    {
        Time.timeScale = 1;
        pauseCanvas.enabled = false;
        resumeBtn.onClick.RemoveAllListeners();
        optionsBtn.onClick.RemoveAllListeners();
        quitBtn.onClick.RemoveAllListeners();
        audio.onValueChanged.RemoveAllListeners();
        bgAudio.onValueChanged.RemoveAllListeners();
        effectAudio.onValueChanged.RemoveAllListeners();
    }

    void ResumeState() {
        gm.switchState(new PlayState(gm));
    }

    void OptionsState()
    {
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

    private void Setup() {
        resumeBtn = pauseCanvas.transform.Find(resume).GetComponent<Button>();
        optionsBtn = pauseCanvas.transform.Find("OptionsBtn").GetComponent<Button>();
        quitBtn = pauseCanvas.transform.Find(quit).GetComponent<Button>();

        audio = pauseCanvas.transform.Find("OptionsMenu/MasterAudio").GetComponent<Slider>();
        bgAudio = pauseCanvas.transform.Find("OptionsMenu/BGAudio").GetComponent<Slider>();
        effectAudio = pauseCanvas.transform.Find("OptionsMenu/EffectAudio").GetComponent<Slider>();
        audio.value = AudioListener.volume;
        bgAudio.value = AudioManager.Instance.bgmVolume;
        effectAudio.value = AudioManager.Instance.soundEffectVolume;

        Listeners();

        menuAnimator = pauseCanvas.GetComponent<Animator>();
        navCloseOptions = new Navigation[2];
        navOpenOptions = new Navigation[2];

        navCloseOptions[0] = optionsBtn.navigation;
        navCloseOptions[1] = quitBtn.navigation;

        Navigation temp = navCloseOptions[0];
        temp.selectOnDown = audio;
        navOpenOptions[0] = temp;

        temp = navCloseOptions[1];
        temp.selectOnUp = effectAudio;
        navOpenOptions[1] = temp;
    }

    private void Listeners()
    {
        if (resumeBtn)
            resumeBtn.onClick.AddListener(ResumeState);
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

    void QuitGame() {
        Debug.Log("Quit game");
        Application.Quit();
        //UnityEditor.EditorApplication.isPlaying = false;
    }

}
