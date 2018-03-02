using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleScene : MonoBehaviour
{

    public static BattleScene instance;
    public AudioClip battleMusic;
    public AudioClip buttonMoveSound;
    public AudioClip buttonSelectSound;
    public int escapeChance;
    public int enemyHP;

    public GameObject battleTextbox;
    public GameObject[] enemies;
    public SpriteRenderer[] buttons;
    public Sprite[] activatedSprites;
    private List<Sprite> deactivatedSprites = new List<Sprite>();
    public Transform topLeft;
    public Transform bottomLeft;

    public delegate void EnemysTurnEventHandler();
    public static event EnemysTurnEventHandler EnemysTurn;

    private Text battleText;
    private bool playersTurn;
    private bool axisDown;
    private int currentButtonIndex;
    private int lastButtonIndex;
    private int enemyIndex;

    private SpriteRenderer sprRend;

    void Start()
    {
        instance = this;

        sprRend = GetComponent<SpriteRenderer>();
        BulletPattern.PatternEnded += StartPlayersTurn;
        BattleState.BattleEnded += ClearBattleScene;

        foreach (GameObject enemy in enemies)
        {
            enemy.SetActive(false);
        }

        foreach (SpriteRenderer button in buttons)
        {
            deactivatedSprites.Add(button.sprite);
        }

        battleText = battleTextbox.GetComponentInChildren<Text>();
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
        battleTextbox.SetActive(false);
        buttons[currentButtonIndex].sprite = deactivatedSprites[currentButtonIndex];
        currentButtonIndex = 0;
        lastButtonIndex = 0;
    }

    void Update()
    {
        if (playersTurn)
        {
            ButtonSelection();
            if (Input.GetButtonDown("Interact"))
            {
                AudioManager.PlayOneShot(buttonSelectSound);
                playersTurn = false;
                buttons[currentButtonIndex].sprite = deactivatedSprites[currentButtonIndex];

                if (currentButtonIndex == 0)
                {
                    StartCoroutine(ShowBattleText("You don't have proficiency in any weapons...", 1.5f, false));
                }
                else if (currentButtonIndex == 1)
                {
                    if (enemyHP > 0)
                    {
                        enemyHP--;
                        string[] texts = { "You played the song of WRYYY", enemies[enemyIndex].name + " is getting drowzy..." };
                        StartCoroutine(ShowBattleText(texts, 1.5f, false));
                    }
                    else if (enemyHP <= 0)
                    {
                        string[] texts = { "You played the song of WRYYY", enemies[enemyIndex].name + " fell asleep..." };
                        StartCoroutine(ShowBattleText(texts, 1.5f, true));
                    }
                }
                else if (currentButtonIndex == buttons.Length - 1)
                {
                    int roll = Random.Range(0, 99);

                    if (roll < escapeChance)
                    {
                        StartCoroutine(ShowBattleText("You ran away from " + enemies[enemyIndex].name, 1.5f, true));
                    }
                    else
                    {
                        StartCoroutine(ShowBattleText(enemies[enemyIndex].name + " is staring at you", 1.5f, false));
                    }
                }
            }
        }
    }
    
    IEnumerator ShowBattleText(string[] texts, float duration, bool leaveBattle)
    {
        battleTextbox.SetActive(true);
        foreach (string text in texts)
        {
            battleText.text = text;
            yield return new WaitForSeconds(duration);
        }
        battleTextbox.SetActive(false);

        if (EnemysTurn != null && !leaveBattle)
        {
            EnemysTurn.Invoke();
        }
        else if (leaveBattle)
        {
            GameManager.instance.switchState(new PlayState(GameManager.instance));
        }
    }

    IEnumerator ShowBattleText(string text, float duration, bool leaveBattle)
    {
        battleTextbox.SetActive(true);
        battleText.text = text;
        yield return new WaitForSeconds(duration);
        battleTextbox.SetActive(false);

        if (EnemysTurn != null && !leaveBattle)
        {
            EnemysTurn.Invoke();
        }
        else if (leaveBattle)
        {
            GameManager.instance.switchState(new PlayState(GameManager.instance));
        }
    }

    private void ButtonSelection()
    {
        buttons[currentButtonIndex].sprite = activatedSprites[currentButtonIndex];

        if ((Input.GetAxisRaw("Horizontal") > 0 || Input.GetAxisRaw("BattleHorizontal") > 0) && !axisDown)
        {
            axisDown = true;

            lastButtonIndex = currentButtonIndex;
            currentButtonIndex = Mathf.Clamp(currentButtonIndex + 1, 0, 2);
            if (currentButtonIndex != lastButtonIndex)
            {
                AudioManager.PlayOneShot(buttonMoveSound);
                buttons[lastButtonIndex].sprite = deactivatedSprites[lastButtonIndex];
            }
        }
        else if ((Input.GetAxisRaw("Horizontal") < 0 || Input.GetAxisRaw("BattleHorizontal") < 0) && !axisDown)
        {
            axisDown = true;

            lastButtonIndex = currentButtonIndex;
            currentButtonIndex = Mathf.Clamp(currentButtonIndex - 1, 0, 2);
            if (currentButtonIndex != lastButtonIndex)
            {
                AudioManager.PlayOneShot(buttonMoveSound);
                buttons[lastButtonIndex].sprite = deactivatedSprites[lastButtonIndex];
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
