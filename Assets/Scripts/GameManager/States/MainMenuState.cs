using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainMenuState : GameState
{

    private Button playBtn, optionsBtn, quitBtn;
    private Slider audio, bgAudio, effectAudio;
    private string canvas = "MainMenuCanvas", play = "Interact/PlayBtn", options = "Interact/OptionsBtn", quit = "Interact/QuitBtn";
    private bool showOptions = false, setup = false;

    private GameObject lastSelected = null;

    private Transform optionsTrans;

    private Animator menuAnimator;

    private AudioSource selectedSound;

    private Navigation[] navCloseOptions, navOpenOptions;
    private CanvasGroup cg;

    public MainMenuState(GameManager gm)
    {
        this.gm = gm;
    }

    public override void enter()
    {
        Setup();
    }

    public override void update()
    {
        if (!setup)
            Setup();
        if (cg.alpha == 1 && !cg.interactable)
        {
            UIVisible();
        }
        else
        {
            cg.alpha += 0.4f * Time.deltaTime;
        }
        GameObject selected = EventSystem.current.currentSelectedGameObject;
        if (selected == null)
        {
            if (lastSelected != null)
                EventSystem.current.SetSelectedGameObject(lastSelected);
        }
        else {
            lastSelected = selected;
        }
    }

    public override void exit()
    {
        playBtn.onClick.RemoveAllListeners();
        optionsBtn.onClick.RemoveAllListeners();
        quitBtn.onClick.RemoveAllListeners();
    }

    public void UIVisible()
    {
        cg.interactable = true;

        Listeners();

        lastSelected = playBtn.gameObject;
        lastSelected.GetComponent<Animator>().SetBool("Highlighted", true);
    }

    void SwPlayState()
    {
        CameraFX.FadeIn();
        Camera.main.GetComponent<AudioSource>().Stop();
        GameManager.MainMenuCanvas.GetComponent<AudioSource>().Play();
        GameManager.instance.StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        yield return new WaitForSecondsRealtime(1f);
        gm.switchState(new CinematicState(gm));
    }

    void OptionsState()
    {
        GameManager.MainMenuCanvas.GetComponent<AudioSource>().Play();
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

    private void Setup()
    {
        if (GameManager.MainMenuCanvas == null)
            return;
        playBtn = GameManager.MainMenuCanvas.transform.Find(play).GetComponent<Button>();
        optionsBtn = GameManager.MainMenuCanvas.transform.Find(options).GetComponent<Button>();
        quitBtn = GameManager.MainMenuCanvas.transform.Find(quit).GetComponent<Button>();

        cg = GameManager.MainMenuCanvas.transform.Find("Interact").GetComponent<CanvasGroup>();

        audio = GameManager.MainMenuCanvas.transform.Find("Interact/OptionsMenu/MasterAudio").GetComponent<Slider>();
        audio.value = AudioListener.volume;
        bgAudio = GameManager.MainMenuCanvas.transform.Find("Interact/OptionsMenu/BGAudio").GetComponent<Slider>();
        bgAudio.value = GameManager.instance.bgAudio;
        effectAudio = GameManager.MainMenuCanvas.transform.Find("Interact/OptionsMenu/EffectAudio").GetComponent<Slider>();
        effectAudio.value = GameManager.instance.effectAudio;

        menuAnimator = GameManager.MainMenuCanvas.GetComponent<Animator>();
        navCloseOptions = new Navigation[2];
        navOpenOptions = new Navigation[2];

        GameManager.MainMenuCanvas.enabled = true;

        navCloseOptions[0] = optionsBtn.navigation;
        navCloseOptions[1] = quitBtn.navigation;

        Navigation temp = navCloseOptions[0];
        temp.selectOnDown = audio;
        navOpenOptions[0] = temp;

        temp = navCloseOptions[1];
        temp.selectOnUp = effectAudio;
        navOpenOptions[1] = temp;

        //selectedSound = cg.GetComponent<AudioSource>();

        setup = true;
    }

    private void Listeners()
    {
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
        GameManager.MainMenuCanvas.GetComponent<AudioSource>().Play();
        Application.Quit();
        //UnityEditor.EditorApplication.isPlaying = false;
    }
}