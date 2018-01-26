using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayState : GameState {

    public StateController player;
    public PlayerData playerData;
    public GameObject playCanvas;
    public Image hp;
    public Image[] itemIcons;
    public GameObject iconPrefab;

    private int[] lastValues;
    private int iconOffset = 0;

    public PlayState(GameManager gm)
    {
        playerControl = true;
        this.gm = gm;
    }

    public override void enter() {
        findPlayer();
        playCanvas = GameObject.Find("/PlayCanvas");
        hp = playCanvas.transform.Find("HPBar").GetComponent<Image>();
    }

    public override void update() {
        if (playerData != null)
        {
            if (playerData.health <= 0)
            {
                gm.switchState(new GameOverState(gm));
            }
            if (playCanvas == null)
            {
                playCanvas = GameObject.Find("/PlayCanvas");
                if (playCanvas == null)
                    Debug.LogWarning("Can't find /PlayCanvas");
            }
            else {
                if (hp == null) {
                    hp = playCanvas.transform.Find("HPBar").GetComponent<Image>();
                }
                else {
                    RectTransform rect = hp.rectTransform;
                    rect.sizeDelta = new Vector2((100 * playerData.health / 10), 10);
                }
                for (int i = 0; i < lastValues.Length; i++)
                {
                    if (lastValues[i] != playerData.items[i])
                    {
                        if (itemIcons[i] == null)
                        {
                            if (iconPrefab == null)
                                iconPrefab = Resources.Load<GameObject>("Icons/ItemImage");
                            itemIcons[i] = GameObject.Instantiate(iconPrefab, playCanvas.transform).GetComponent<Image>();
                            Sprite sprite = Resources.Load<Sprite>("Icons/" + System.Enum.GetName(typeof(ItemType), i));
                            if(sprite != null)
                                itemIcons[i].sprite = sprite;
                            itemIcons[i].rectTransform.Translate(new Vector3(iconOffset, 0));
                            iconOffset -= 60;
                        }
                        lastValues[i] = playerData.items[i];
                        itemIcons[i].GetComponentInChildren<Text>().text = "x" + playerData.items[i];
                    }
                }
            }

        }
        else
        {
            findPlayer();
        }

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
            gm.switchState(new PauseState(gm));
    }

    public void findPlayer() {
        player = GameObject.FindWithTag("Player").GetComponent<StateController>();
        if (player != null && player.data.GetType() == typeof(PlayerData)) {
            playerData = (PlayerData)player.data;
            lastValues = new int[playerData.items.Length];
            itemIcons = new Image[lastValues.Length];
        }
    }
}
