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
        {
            if (newState.PlayerControl)
            {
                con.rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            } else
            {
                con.rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            }
            con.enabled = newState.PlayerControl;
        }
        lastState = newState;
    }
}
