using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateHandler : MonoBehaviour {

    private GameState lastState;

	// Use this for initialization
	void Start () {
        
	}

    private void OnEnable()
    {
        GameManager.ChangeState += ChangeGameState;
    }

    private void OnDisable()
    {
        GameManager.ChangeState -= ChangeGameState;
    }

    // Update is called once per frame
    void Update () {
		
	}

    private void ChangeGameState(GameState newState) {
        //TODO: newState.PlayerControl for control of character
        StateController con = GetComponent<StateController>();
        if (con)
            con.enabled = newState.PlayerControl;
        lastState = newState;
    }
}
