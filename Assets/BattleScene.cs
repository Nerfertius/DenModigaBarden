using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleScene : MonoBehaviour {

    public static BattleScene instance;
    public AudioClip battleMusic;
    public int escapeChance;

    public GameObject[] enemies;
    public SpriteRenderer[] buttons;
    public Transform topLeft;
    public Transform bottomLeft;

    public delegate void EnemysTurnEventHandler();
    public static event EnemysTurnEventHandler EnemysTurn;

    private bool playersTurn;
    private bool axisDown;
    private int currentButtonIndex;
    private int lastButtonIndex;
    private int enemyIndex;

    private SpriteRenderer sprRend;

	void Start () {
        instance = this;

        sprRend = GetComponent<SpriteRenderer>();
        BulletPattern.PatternEnded += StartPlayersTurn;
        BattleState.BattleEnded += ClearBattleScene;
        
        foreach (GameObject enemy in enemies)
        {
            enemy.SetActive(false);
        }
    }

    void OnDestroy()
    {
        BulletPattern.PatternEnded -= StartPlayersTurn;
        BattleState.BattleEnded -= ClearBattleScene;
    }

    private void ClearBattleScene()
    {
        foreach (GameObject enemy in enemies)
        {
            enemy.SetActive(false);
        }

        playersTurn = false;
        buttons[currentButtonIndex].color = Color.black;
        currentButtonIndex = 0;
        lastButtonIndex = 0;
    }

    void Update () {
		if (playersTurn)
        {
            ButtonSelection();
            if (Input.GetButtonDown("Interact"))
            {
                playersTurn = false;
                buttons[currentButtonIndex].color = Color.black;

                if (currentButtonIndex == buttons.Length - 1)
                {
                    int roll = Random.Range(0, 99);

                    if (roll < escapeChance)
                    {
                        GameManager.instance.switchState(new PlayState(GameManager.instance));
                        return;
                    }
                }

                if (EnemysTurn != null)
                {
                    EnemysTurn.Invoke();
                }
            }
        }
	}

    private void ButtonSelection()
    {
        buttons[currentButtonIndex].color = Color.blue;

        if ((Input.GetAxisRaw("Horizontal") > 0 || Input.GetAxisRaw("BattleHorizontal") > 0) && !axisDown)
        {
            axisDown = true;

            lastButtonIndex = currentButtonIndex;
            currentButtonIndex = Mathf.Clamp(currentButtonIndex + 1, 0, 2);
            if (currentButtonIndex != lastButtonIndex)
            {
                buttons[lastButtonIndex].color = Color.black;
            }
        }
        else if ((Input.GetAxisRaw("Horizontal") < 0 || Input.GetAxisRaw("BattleHorizontal") < 0) && !axisDown)
        {
            axisDown = true;

            lastButtonIndex = currentButtonIndex;
            currentButtonIndex = Mathf.Clamp(currentButtonIndex - 1, 0, 2);
            if (currentButtonIndex != lastButtonIndex)
            {
                buttons[lastButtonIndex].color = Color.black;
            }
        }

        if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("BattleHorizontal") == 0)
        {
            axisDown = false;
        }
    }
    
    public void SetBackground(Sprite spr)
    {
        sprRend.sprite = spr;
    }

    public void SetBattleMusic(AudioClip clip)
    {
        battleMusic = clip;
    }

    public void SetEnemy(int index)
    {
        enemyIndex = Mathf.Clamp(index, 0, enemies.Length - 1);
    }

    public void StartBattle()
    {
        if (battleMusic != null)
        {
            AudioManager.PlayBGM(battleMusic);
        }

        enemies[enemyIndex].SetActive(true);

        playersTurn = true;
        currentButtonIndex = 0;

        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z);
    }

    private void StartPlayersTurn()
    {
        playersTurn = true;
    }
}
