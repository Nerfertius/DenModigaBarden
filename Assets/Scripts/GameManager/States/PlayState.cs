using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayState : GameState {

    public StateController player;
    public Image hp;

    public PlayState(GameManager gm)
    {
        playerControl = true;
        this.gm = gm;
    }

    public override void enter() {
        findPlayer();
        hp = GameObject.Find("/PlayCanvas/HPBar").GetComponent<Image>();
    }

    public override void update() {
        if (player != null) {
            //TODO: change correct var
            if (player.data != null && ((PlayerData)player.data).health <= 0) {
                gm.switchState(new GameOverState(gm));
            }
            RectTransform rect = hp.rectTransform;
            rect.sizeDelta = new Vector2((100 * ((PlayerData)player.data).health / 10), 10);
        }

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
            gm.switchState(new PauseState(gm));
    }

    public void findPlayer() {
        player = GameObject.FindWithTag("Player").GetComponent<StateController>();
    }
}
