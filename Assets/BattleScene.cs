using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleScene : MonoBehaviour {

    public static BattleScene instance;
    public AudioClip battleMusic;

    public Transform topLeft;
    public Transform bottomLeft;

    public delegate void BattleStartedEventHandler();
    public static event BattleStartedEventHandler BattleStarted;

    private SpriteRenderer sprRend;

	void Start () {
        instance = this;

        sprRend = GetComponent<SpriteRenderer>();
    }

    void Update () {
		
	}
    
    public void SetBackground(Sprite spr)
    {
        sprRend.sprite = spr;
    }

    public void SetBattleMusic(AudioClip clip)
    {
        battleMusic = clip;
    }

    public void StartBattle()
    {
        if (battleMusic != null)
        {
            AudioManager.PlayBGM(battleMusic);
        }

        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z);
        TopDownController.controllable = true;

        if(BattleStarted != null) { 
            BattleStarted.Invoke();
        }
    }
}
