using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Canvases { MainMenu, Play, Pause, WorldSpace }

public class GMCanvasRegi : MonoBehaviour {

    public Canvases canvas;
    
	void Start () {
        Canvas canvas = GetComponent<Canvas>();
        switch (this.canvas) {
            case Canvases.MainMenu:
                GameManager.instance.MainMenuCanvas = canvas;
                break;
            case Canvases.Play:
                GameManager.instance.PlayCanvas = canvas;
                break;
            case Canvases.Pause:
                GameManager.instance.PauseCanvas = canvas;
                break;
            case Canvases.WorldSpace:
                GameManager.instance.WorldSpaceCanvas = canvas;
                break;
        }
	}
}
