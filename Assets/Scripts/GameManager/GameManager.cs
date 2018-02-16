using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public delegate void StateChange(GameState newState);
    public static event StateChange ChangeState;

    public static GameManager instance;

    public GameState current = null;

    public PlayerData player;

    public Sprite fullHeart, halfHeart, emptyHeart;
    public Sprite[] notes = new Sprite[5];

    public PlayerData player;

    [HideInInspector]
    public Canvas MainMenuCanvas, PlayCanvas, PauseCanvas, WorldSpaceCanvas;

    private AsyncOperation async;

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        instance = this;
    }

    void Start() {
        if (current == null)
        {
            switchState(new MainMenuState(this));
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
        async.allowSceneActivation = false;

        Debug.Log("loading");

        yield return async;

        Debug.Log("Loaded");
    }
}