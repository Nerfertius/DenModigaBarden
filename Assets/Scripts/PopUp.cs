using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUp : MonoBehaviour
{
    private bool hasShowed;
    private SpriteRenderer rend;
    private StateController npcState;
    
    public Sprite sprite;
    public bool repeatable;
    public Vector2 offset;

    [Header("Application")]
    public bool npcTalk;
    public State state;
    public bool melodyPlayed;
    public Melody.MelodyID melody;
    public bool actionPerformed;
    public string buttonName;


    private void OnDrawGizmosSelected()
    {
        Vector2 pos = (Vector2)transform.position + offset;
        Gizmos.DrawCube(pos, Vector2.one / 2);
    }

    void Start ()
    {
        if (npcTalk)
        {
            npcState = transform.parent.GetComponent<StateController>();
        }
        GameObject empty = new GameObject();
        empty.AddComponent<SpriteRenderer>();
        GameObject prompt = Instantiate(empty, (Vector2)transform.position + offset, Quaternion.identity);
        rend = prompt.GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (repeatable)
            {
                rend.sprite = sprite;
            }

            else if (!repeatable && !hasShowed)
            {
                rend.sprite = sprite;
                hasShowed = true;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerData data = collision.GetComponent<PlayerData>();
            PlayerData.MelodyData mData = data.melodyData;

            if (npcTalk)
            {
                //print("START..." + npcState.currentState.ToString() + "... END");
                //print(npcState.currentState.ToString() == "NPCIdle");
                if (npcState.currentState == state)
                {
                    rend.sprite = null;
                }
                else if (npcState.currentState.ToString() == "NPCIdle (State)")
                {
                    rend.sprite = sprite;
                }
            }

            else if (melodyPlayed && mData.currentMelody == melody)
            {
                rend.sprite = null;
            }

            else if (actionPerformed && Input.GetButtonDown(buttonName))
            {
                rend.sprite = null;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            rend.sprite = null;
        }
    }
}
