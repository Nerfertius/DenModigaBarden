using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateHandler : MonoBehaviour {

    private GameState lastState;

	// Use this for initialization
	void Start () {
        GameManager.ChangeState += ChangeGameState;

        GameManager gm = (GameManager) Object.FindObjectOfType(typeof(GameManager));
        if (!gm)
            Debug.Log("Could not find GameManager");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void ChangeGameState(GameState newState) {
        //TODO: newState.PlayerControl for control of character
        lastState = newState;
    }
}
