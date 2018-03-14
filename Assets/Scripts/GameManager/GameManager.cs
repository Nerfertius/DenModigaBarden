using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public delegate void StateChange(GameState newState);
    public static event StateChange ChangeState;

    public static GameManager instance;

    public GameState current = null;

    [HideInInspector] public PlayerData player;

    public Sprite fullHeart, halfHeart, emptyHeart;
    public Sprite[] notes = new Sprite[5];

    public float bgAudio = 1, effectAudio = 1;

    [HideInInspector]
    public static Canvas MainMenuCanvas, PlayCanvas, PauseCanvas, WorldSpaceCanvas, GameOverCanvas;

    private AsyncOperation async;

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        instance = this;
    }

    void Start() {
        player = PlayerData.player;

        if (current == null)
        {
            if (player)
            {
                switchState(new PlayState(this));
            }
            else {
                switchState(new MainMenuState(this));
            }
        }
        else {
            current.enter();
            if (ChangeState != null)
                ChangeState(current);
        }
    }

    void Update() {
        if (current != null)
            current.update();
    }

    public void switchState(GameState next) {
        if(current != null)
            current.exit();
        current = next;
        current.enter();
        if(ChangeState != null)
            ChangeState(current);
    }

    public void hideCanvas(string canvas) {
        GameObject go = GameObject.Find("/" + canvas);
        go.GetComponent<CanvasGroup>().alpha = 0;
    }

    public void showCanvas(string canvas) {
        GameObject go = GameObject.Find("/" + canvas);
        go.GetComponent<CanvasGroup>().alpha = 1;
    }

    public AsyncOperation loadScene(int buildIndex) {
        StartCoroutine(loadRoutine(buildIndex));
        return async;
    }

    private IEnumerator loadRoutine(int buildindex) {
        async = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(buildindex);

        yield return async;
    }
}