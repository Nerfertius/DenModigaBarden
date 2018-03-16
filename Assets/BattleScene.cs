using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleScene : MonoBehaviour
{
    public static BattleScene instance;
    public static GameObject caller; //The enemy that initiated the battle
    public AudioClip battleMusic;
    public AudioClip buttonMoveSound;
    public AudioClip buttonSelectSound;
    [HideInInspector] public int escapeChance;

    public SpriteRenderer topBackground;
    public GameObject battleTextbox;
    public Image enemyHPBar;
    public GameObject[] enemies;
    private List<Animator> enemiesAnim = new List<Animator>();
    private List<BattleText> textStrings = new List<BattleText>();
    public SpriteRenderer[] buttons;
    public Sprite[] activatedSprites;
    private List<Sprite> deactivatedSprites = new List<Sprite>();
    public Transform center, topLeft, topRight, playArea;
    public Bounds playAreaBounds;

    public delegate void EnemysTurnEventHandler();
    public static event EnemysTurnEventHandler EnemysTurn;

    private Text battleText;
    private bool firstBattle = true;
    private bool firstTextShown = false;
    private float enemyMaxHP;
    private float enemyCurrentHP;
    private bool playersTurn;
    private bool axisDown;
    private int currentButtonIndex;
    private int lastButtonIndex;
    private int enemyIndex;
    private int fightCount = -1;

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
            textStrings.Add(enemy.GetComponent<BattleText>());
            enemiesAnim.Add(enemy.GetComponent<Animator>());
        }

        foreach (SpriteRenderer button in buttons)
        {
            deactivatedSprites.Add(button.sprite);
        }

        if (playArea != null)
        {
            playAreaBounds = playArea.GetComponent<BoxCollider2D>().bounds;
        } else
        {
            Debug.LogWarning("PlayArea not set, some bullet patterns might not work as intended");
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

        foreach (Animator anim in enemiesAnim)
        {
            anim.SetBool("Sleeping", false);
        }

        playersTurn = false;
        battleTextbox.SetActive(false);
        enemyHPBar.gameObject.SetActive(false);
        enemyHPBar.fillAmount = 1;
        buttons[currentButtonIndex].sprite = deactivatedSprites[currentButtonIndex];
        currentButtonIndex = 0;
        lastButtonIndex = 0;
    }

    void Update()
    {
        
        if (playersTurn)
        {
            if (firstBattle) {
                StartCoroutine(ShowText(new string[] { "You got engaged in a battle!", "The enemy will try to attack you", "Try your best to avoid his attacks!", "Select your move!" }, 1.5f));
                firstBattle = false;
            }

            if (!firstTextShown) return;

            ButtonSelection();
            if (Input.GetButtonDown("Interact"))
            {
                AudioManager.PlayOneShot(buttonSelectSound);
                playersTurn = false;
                buttons[currentButtonIndex].sprite = deactivatedSprites[currentButtonIndex];

                if (currentButtonIndex == 0)
                {
                    if (fightCount < 4)
                    {
                        fightCount++;
                    }
                    else {
                        fightCount = 0;
                    }
                    switch (fightCount)
                    {
                        case 0:
                            StartCoroutine(ShowBattleText("It is useless to fight.", 1.5f, false));
                            break;
                        case 1:
                            StartCoroutine(ShowBattleText("You are a bard, you cannot fight.", 1.5f, false));
                            break;
                        case 2:
                            StartCoroutine(ShowBattleText("You don't have proficiency in any weapons...", 1.5f, false));
                            break;
                        case 3:
                            StartCoroutine(ShowBattleText("Stop fighting and play a song!", 1.5f, false));
                            break;
                        case 4:
                            StartCoroutine(ShowBattleText(new string[] { "What are you trying to do?", "Hit it with your flute?" }, 1.5f, false));
                            break;
                        default:
                            break;
                    }
                }
                else if (currentButtonIndex == 1)
                {
                    enemyCurrentHP--;
                    StartCoroutine(HealthReduction());
                    if (enemyCurrentHP > 0)
                    {
                        string[] texts = textStrings[enemyIndex].songText;
                        StartCoroutine(ShowBattleText(texts, 1.5f, false));
                    }
                    else if (enemyCurrentHP <= 0)
                    {
                        string[] texts = textStrings[enemyIndex].songWinText;
                        StartCoroutine(ShowBattleText(texts, 1.5f, true));
                        enemiesAnim[enemyIndex].SetBool("Sleeping", true);
                    }
                }
                else if (currentButtonIndex == buttons.Length - 1)
                {
                    int roll = Random.Range(0, 99);

                    if (roll < escapeChance)
                    {
                        StartCoroutine(ShowBattleText(textStrings[enemyIndex].runSuccessText, 1.5f, true));
                        caller.SetActive(false);
                    }
                    else
                    {
                        StartCoroutine(ShowBattleText(textStrings[enemyIndex].runFailedText, 1.5f, false));
                    }
                }
            }
        }
    }

    IEnumerator HealthReduction()
    {
        for (float value = enemyHPBar.fillAmount; value >= enemyCurrentHP / enemyMaxHP; value -= 0.01f)
        {
            enemyHPBar.fillAmount = value;
            yield return new WaitForSeconds(0.02f);
        }
    }
    IEnumerator ShowText(string[] texts, float duration)
    {
        battleTextbox.SetActive(true);
        foreach (string text in texts)
        {
            battleText.text = text;
            yield return new WaitForSeconds(duration);
        }
        battleTextbox.SetActive(false);
        firstTextShown = true;
    }
    IEnumerator ShowText(string text, float duration)
    {
        battleTextbox.SetActive(true);
        battleText.text = text;
        yield return new WaitForSeconds(duration);
        battleTextbox.SetActive(false);
        firstTextShown = true;
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

    public void SetTopBackground(Sprite spr)
    {
        topBackground.sprite = spr;
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

    public void SetEnemyHP(int hp)
    {
        enemyMaxHP = hp;
        enemyCurrentHP = hp;
    }

    public void StartBattleMusic()
    {
        if (battleMusic != null)
        {
            AudioManager.PlayBGM(battleMusic, false);
        }
    }

    public void StartBattle()
    {        
        enemies[enemyIndex].SetActive(true);

        enemyHPBar.gameObject.SetActive(true);
        playersTurn = true;
        currentButtonIndex = 0;

        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z);
    }

    private void StartPlayersTurn()
    {
        playersTurn = true;
    }
}
