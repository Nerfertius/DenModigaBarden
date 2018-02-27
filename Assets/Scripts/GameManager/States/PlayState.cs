using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayState : GameState
{

    public StateController player;
    public PlayerData playerData;
    public GameObject playCanvas;
    public Image[] itemIcons;
    public GameObject iconPrefab;

    public Image notesBg;
    public Image[] notes;

    private int[] lastValues;
    private int iconOffset = 0;

    private HP hp;

    public PlayState(GameManager gm)
    {
        playerControl = true;
        this.gm = gm;
    }

    public override void enter()
    {
        playerData = PlayerData.player;
        if (playerData) {
            lastValues = new int[playerData.items.Length];
            itemIcons = new Image[lastValues.Length];
        }
        getPlayCanvas();
        hp = HP.GetInstance;
    }

    public override void update()
    {
        if (playerData != null)
        {
            if (playerData.health <= 0)
            {
                //gm.switchState(new GameOverState(gm));
            }
            if (playCanvas == null)
            {
                getPlayCanvas();
            }
            else
            {
                //****************HEALTH****************
                if (hp != null)
                {
                    hp.Update(playerData.health);
                }
                else {
                    hp = HP.GetInstance;
                }
                //****************ITEMS****************
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
                            if (sprite != null)
                                itemIcons[i].sprite = sprite;
                            itemIcons[i].rectTransform.Translate(new Vector3(iconOffset, 0));
                            iconOffset -= 60;
                        }
                        lastValues[i] = playerData.items[i];
                        itemIcons[i].GetComponentInChildren<Text>().text = "x" + playerData.items[i];
                    }
                }
                //****************NOTES****************
                if (notesBg == null)
                    notesBg = playCanvas.transform.Find("UINotes").GetComponent<Image>();
                if (Input.GetButton("PlayMelody"))
                {
                    CanvasGroup cg = notesBg.GetComponent<CanvasGroup>();
                    cg.alpha = 1;
                    if (notes == null)
                    {
                        notes = new Image[5];
                        if (notesBg.transform.childCount == 0)
                        {
                            //TODO: Generate images
                        }
                        else
                        {
                            for (int i = 1; i <= notes.Length; i++)
                            {
                                notes[i - 1] = notesBg.transform.Find("Note" + i).GetComponent<Image>();
                            }
                        }
                    }
                    if (playerData.melodyData.PlayedNotes.Count > 0)
                    {
                        int i = 0;
                        foreach (Note n in playerData.melodyData.PlayedNotes)
                        {
                            Color c = notes[i].color;
                            //TODO: Change image sprite
                            Sprite toSprite = notes[i].sprite;
                            switch (n.noteID)
                            {
                                case Note.NoteID.G:
                                    if (gm.notes[0] != null)
                                        toSprite = gm.notes[0];
                                    break;
                                case Note.NoteID.A:
                                    if (gm.notes[1] != null)
                                        toSprite = gm.notes[1];
                                    break;
                                case Note.NoteID.B:
                                    if (gm.notes[2] != null)
                                        toSprite = gm.notes[2];
                                    break;
                                case Note.NoteID.C:
                                    if (gm.notes[3] != null)
                                        toSprite = gm.notes[3];
                                    break;
                                case Note.NoteID.D:
                                    if (gm.notes[4] != null)
                                        toSprite = gm.notes[4];
                                    break;
                                default:
                                    break;

                            }
                            notes[i].sprite = toSprite;
                            c.a = 1;
                            notes[i].color = c;
                            i++;
                        }
                    }
                    else
                    {
                        for (int i = 0; i < notes.Length; i++)
                        {
                            Color c = notes[i].color;
                            c.a = 0;
                            notes[i].color = c;
                        }
                    }
                }
                else
                {
                    CanvasGroup cg = notesBg.GetComponent<CanvasGroup>();
                    cg.interactable = false;
                    cg.alpha = 0;
                }
            }
        }
        else
        {
            playerData = PlayerData.player;
            if(lastValues == null)
                lastValues = new int[playerData.items.Length];
            if(itemIcons == null)
                itemIcons = new Image[lastValues.Length];
        }

        if (Input.GetButtonDown("Cancel"))
            gm.switchState(new PauseState(gm));
    }

    private void getPlayCanvas() {
        if (gm.PlayCanvas)
        {
            playCanvas = gm.PlayCanvas.gameObject;
            gm.PlayCanvas.enabled = true;
        }
        else
        {
            playCanvas = GameObject.Find("PlayCanvas");
            if (playCanvas == null)
                Debug.LogWarning("Can't find PlayCanvas");
        }

        if (playCanvas) {
            notesBg = playCanvas.transform.Find("UINotes").GetComponent<Image>();
        }

    }

    public override void exit()
    {
        gm.PlayCanvas.enabled = false;
    }
}
