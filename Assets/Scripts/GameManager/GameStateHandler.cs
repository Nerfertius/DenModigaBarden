using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateHandler : MonoBehaviour {

    private GameState lastState;
    private StateController con;

    // Use this for initialization
    void Start () {
        con = GetComponent<StateController>();
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
        if (con)
            con.enabled = newState.PlayerControl;
        lastState = newState;
    }
}
