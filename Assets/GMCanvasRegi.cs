using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Canvases { MainMenu, Play, Pause, WorldSpace, GameOver }

public class GMCanvasRegi : MonoBehaviour {

    public Canvases canvas;
    
	void Start () {
        Canvas canvas = GetComponent<Canvas>();
        switch (this.canvas) {
            case Canvases.MainMenu:
                GameManager.MainMenuCanvas = canvas;
                break;
            case Canvases.Play:
                GameManager.PlayCanvas = canvas;
                break;
            case Canvases.Pause:
                GameManager.PauseCanvas = canvas;
                break;
            case Canvases.WorldSpace:
                GameManager.WorldSpaceCanvas = canvas;
                break;
            case Canvases.GameOver:
                GameManager.GameOverCanvas = canvas;
                break;
        }
	}
}
